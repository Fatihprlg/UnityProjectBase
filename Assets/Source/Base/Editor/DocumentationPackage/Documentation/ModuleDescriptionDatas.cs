#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class ModuleDescriptionDatas
{
    List<ModuleDescription> moduleDescriptions = new List<ModuleDescription>();

    public List<ModuleDescription> GetDescriptions()
    {
        Refresh();
        return moduleDescriptions;
    }

    void Refresh()
    {
        moduleDescriptions.Clear();

        var path = "Assets/Source/Base/Editor/DocumentationPackage/Documents";
        string[] modules = Directory.GetFiles(path, "*.asset", SearchOption.TopDirectoryOnly);
        foreach (string matFile in modules)
        {
            string assetPath = matFile.Replace('\\', '/');
            ModuleDescription module =
                (ModuleDescription) AssetDatabase.LoadAssetAtPath(assetPath, typeof(ModuleDescription));
            moduleDescriptions.Add(module);
            // .. do whatever you like
        }
    }
}
#endif
