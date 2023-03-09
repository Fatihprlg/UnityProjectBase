using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class IOCWindow : EditorWindow
{
    [MenuItem("My Game Lib/Setup IOC on Scene")]
    public static void SetupIOC()
    {
        Context context = FindObjectOfType<Context>();
        if(context) DestroyImmediate(context);
        Builder builder = FindObjectOfType<Builder>();
        if(builder) DestroyImmediate(builder);
        
        Object prefab = Resources.Load(Constants.Strings.IOC_PREFAB_PATH);
        GameObject instantiatedObj = PrefabUtility.InstantiatePrefab(prefab) as GameObject;
        Initializer initializer = FindObjectOfType<Initializer>();
        if (initializer)
            initializer.InsertInitializableObject(instantiatedObj);
    }
}
