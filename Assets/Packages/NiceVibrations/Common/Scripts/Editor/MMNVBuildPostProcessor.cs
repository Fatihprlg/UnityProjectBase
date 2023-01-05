#if UNITY_EDITOR
using System.IO;
using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.Build;
#if UNITY_2018_1_OR_NEWER
using UnityEditor.Build.Reporting;
#endif

#if UNITY_IOS
using UnityEditor.iOS.Xcode;
#endif

namespace MoreMountains.NiceVibrations
{
    /// <summary>
    /// This class takes care of post processing after a build, adding properties to the XCode project or framework depending on the Unity version.
    /// Note that this class tries to guess paths to certain files, and while this will work in most cases, you may be in a configuration where it doesn't.
    /// If needed, you can override paths manually by editing the scriptable object located in NiceVibrations/Common/Resources, and called
    /// MMNVPathDefinition. Don't change its name! Just select it, and from the editor, specify your paths overrides.
    /// </summary>    
    public class MMNVBuildPostProcessor : IPostprocessBuildWithReport
    {
        /// the order at which this build post processor should run
        public int callbackOrder => 1;
        
        /// <summary>
        /// On post process build, we run our treatment, and throw an error if necessary
        /// </summary>
        /// <param name="report"></param>
        public void OnPostprocessBuild(BuildReport report)
        {
            try
            {
                if (report.summary.platform == BuildTarget.iOS)
                {
                    string path = report.summary.outputPath;
                    #if UNITY_IOS && UNITY_EDITOR
                        ConfigureXCodeProjectForNativePlugin(path);
                    #endif
                }
            }
            catch (System.Exception e)
            {
                throw new BuildFailedException(e); // make sure the build fails
            }
        }

        /// <summary>
        /// This method tries to guess the path to the plugin
        /// </summary>
        /// <returns></returns>
        public static string GetPluginPath()
        {
            #if UNITY_IOS && UNITY_EDITOR
                string[] res = System.IO.Directory.GetFiles(Application.dataPath, "MMNVBuildPostProcessor.cs", SearchOption.AllDirectories);
                if (res.Length == 0)
                {
                    return "error";
                }
                string path = res[0].Replace("MMNVBuildPostProcessor.cs", "").Replace("\\", "/");

                string[] explodedPath = path.Split('/');

                string finalPath = "Libraries/";

                bool assetsFound = false;
                for (int i = 0; i < explodedPath.Length; i++)
                {
                    if (!assetsFound)
                    {
                        if (explodedPath[i] == "Assets")
                        {
                            assetsFound = true;
                        }
                    }
                    else
                    {
                        finalPath += explodedPath[i] + "/";
                    }
                }
                finalPath = finalPath.Replace("//", "/");
                finalPath = finalPath.Replace("Common/Scripts/Editor", "Common/Plugins/iOS/Swift/");

                if (!finalPath.Contains("Common/Plugins/iOS/Swift"))
                {
                    return "path not found";
                }

                return finalPath;
            #else
                return "not iOS";
            #endif
        }

    #if UNITY_IOS && UNITY_EDITOR
        public static void ConfigureXCodeProjectForNativePlugin(string xcodeProjectPath)
        {
            string pluginPath = GetPluginPath();

            // we check if we can find a manual path override
            MMNVPath pathDefinition = Resources.Load<MMNVPath>("MMNVPathDefinition");
            bool forceAlwaysEmbedSwiftStandardLibraries = false;
            if (pathDefinition != null)
            {
                if (pathDefinition.PluginPath != "")
                {
                    pluginPath = pathDefinition.PluginPath;
                }
                forceAlwaysEmbedSwiftStandardLibraries = pathDefinition.ForceAlwaysEmbedSwiftStandardLibraries;
            }

            string pbxProjectPath = PBXProject.GetPBXProjectPath(xcodeProjectPath);
            PBXProject pbxProject = new PBXProject();
            pbxProject.ReadFromString(File.ReadAllText(pbxProjectPath));
            #if UNITY_2019_3_OR_NEWER
                string targetGUID = pbxProject.GetUnityFrameworkTargetGuid();
                WritePropertiesToFramework(pbxProject, targetGUID, pbxProjectPath, pluginPath, forceAlwaysEmbedSwiftStandardLibraries);
#else
                string unityTargetName = PBXProject.GetUnityTargetName();
                string targetGUID = pbxProject.TargetGuidByName(unityTargetName);
                WritePropertiesToProject(pbxProject, targetGUID, pbxProjectPath, pluginPath, forceAlwaysEmbedSwiftStandardLibraries);
#endif
            File.WriteAllText(pbxProjectPath, pbxProject.WriteToString());
            Debug.Log("[MMNVBuildPostProcessor] Post process complete.");
        }
        /// <summary>
        /// Writes properties to the XCode project
        /// </summary>
        /// <param name="pbxProject"></param>
        /// <param name="targetGUID"></param>
        /// <param name="pbxProjectPath"></param>
        /// <param name="pluginPath"></param>
        private static void WritePropertiesToProject(PBXProject pbxProject, string targetGUID, string pbxProjectPath, string pluginPath, bool forceAlwaysEmbedSwiftStandardLibraries)
        {
            pbxProject.AddFrameworkToProject(targetGUID, "CoreHaptics.framework", false);
            pbxProject.AddBuildProperty(targetGUID, "SWIFT_VERSION", "5.1");
            pbxProject.SetBuildProperty(targetGUID, "ENABLE_BITCODE", "NO");
            pbxProject.SetBuildProperty(targetGUID, "SWIFT_OBJC_BRIDGING_HEADER", pluginPath + "UnitySwift-Bridging-Header.h");
            pbxProject.SetBuildProperty(targetGUID, "SWIFT_OBJC_INTERFACE_HEADER_NAME", "unityswift-Swift.h");
            pbxProject.AddBuildProperty(targetGUID, "LD_RUNPATH_SEARCH_PATHS", "@executable_path/Frameworks");
            if (forceAlwaysEmbedSwiftStandardLibraries)
            {
                pbxProject.AddBuildProperty(targetGUID, "ALWAYS_EMBED_SWIFT_STANDARD_LIBRARIES", "YES");
            }
            File.WriteAllText(pbxProjectPath, pbxProject.WriteToString());

            Debug.Log("[MMNVBuildPostProcessor] Adding properties to XCode project, plugin path : " + pluginPath);
        }

        const string _privateModuleFilename = "module.modulemap";

        /// <summary>
        /// Writes properties to the XCode framework
        /// </summary>
        /// <param name="pbxProject"></param>
        /// <param name="targetGUID"></param>
        /// <param name="pbxProjectPath"></param>
        /// <param name="pluginPath"></param>
        private static void WritePropertiesToFramework(PBXProject pbxProject, string targetGUID, string pbxProjectPath, string pluginPath, bool forceAlwaysEmbedSwiftStandardLibraries)
        {
            string privateModuleFilename = _privateModuleFilename;
            string pluginRelativePath = pluginPath.Substring(10, pluginPath.Length - 10); // remove 'Libraries/'

            // we look for manual path overrides
            MMNVPath pathDefinition = Resources.Load<MMNVPath>("MMNVPathDefinition");
            if (pathDefinition != null)
            {
                if (pathDefinition.ModuleFileName != "")
                {
                    privateModuleFilename = pathDefinition.ModuleFileName;
                }
                if (pathDefinition.PluginRelativePath != "")
                {
                    pluginRelativePath = pathDefinition.PluginRelativePath;
                }
            }
            Debug.Log("[MMNVBuildPostProcessor] module relative path in Unity project: " + pluginRelativePath);

            // Full Path to copy from
            string module_map_filepath = pluginPath + privateModuleFilename;
            Debug.Log("[MMNVBuildPostProcessor] Adding properties to XCode framework, module path : " + module_map_filepath);

            pbxProject.AddFrameworkToProject(targetGUID, "CoreHaptics.framework", false);
            pbxProject.AddBuildProperty(targetGUID, "SWIFT_VERSION", "5.1");
            pbxProject.SetBuildProperty(targetGUID, "ENABLE_BITCODE", "NO");
            if (forceAlwaysEmbedSwiftStandardLibraries)
            {
                pbxProject.AddBuildProperty(targetGUID, "ALWAYS_EMBED_SWIFT_STANDARD_LIBRARIES", "YES");
            }
            pbxProject.AddBuildProperty(targetGUID, "CLANG_ENABLE_MODULES", "YES");
            pbxProject.AddBuildProperty(targetGUID, "SWIFT_INCLUDE_PATHS", pluginPath);
            pbxProject.AddBuildProperty(targetGUID, "LD_RUNPATH_SEARCH_PATHS", "@executable_path/Frameworks");


            // we add a module reference to the pbx project
            string file_guid = pbxProject.AddFile(module_map_filepath, module_map_filepath, PBXSourceTree.Source);
            pbxProject.AddFileToBuild(targetGUID, file_guid);
            File.WriteAllText(pbxProjectPath, pbxProject.WriteToString());

            // we copy the module file to the project
            string privateModuleFilepath = Application.dataPath + "/" + pluginRelativePath + "/" + privateModuleFilename;
            string projFileDir = System.IO.Path.GetDirectoryName(pbxProjectPath);
            string destination = projFileDir + "/../" + module_map_filepath;
            Debug.Log("[MMNVBuildPostProcessor] Copy module file to project : " + privateModuleFilepath + " -> " + destination);
            System.IO.File.Copy(privateModuleFilepath, destination);
        }
#endif
    }
}
#endif
