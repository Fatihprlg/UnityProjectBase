using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MoreMountains.NiceVibrations
{
    [CreateAssetMenu(fileName = "MMNVPathDefinition", menuName = "MoreMountains/NiceVibrations/MMNVPathDefinition")]
    public class MMNVPath : ScriptableObject
    {
        [Header("Paths")]
        /// the path to the plugin in XCode (usually Libraries/NiceVibrations/Common/Plugins/iOS/Swift/)
        public string PluginPath;
        /// the name of the module (module.modulemap by default)
        public string ModuleFileName;
        /// the path in Unity (without Assets/, so usually NiceVibrations/Common/Plugins/Swift/)
        public string PluginRelativePath;
        [Header("Swift")]
        /// whether or not the post processing build should force ALWAYS_EMBED_SWIFT_STANDARD_LIBRARIES to true
        /// it's usually not needed, but it's solved build errors for some people
        public bool ForceAlwaysEmbedSwiftStandardLibraries = false;
    }
}
