using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class Builder : MonoBehaviour, IBuilder
{
    public List<ClassInfo> classes { get; private set; }
    [SerializeField, HideInInspector] private IOCIncludedAssemblies assemblies;
    
    public void Build(Container container)
    {
        if (classes == null || classes.Count < 1) MapClasses();
        foreach (ClassInfo item in classes)
        {
            if (item.implementation == null) continue;
            Type type = item.implementation.GetType();
            container.Register(type.BaseType, type, item);
        }
    }

#if UNITY_EDITOR
    public void OnValidate()
    {
        AssemblyReloadEvents.afterAssemblyReload -= MapClasses;
        AssemblyReloadEvents.afterAssemblyReload += MapClasses;
        if(assemblies is null)
            assemblies = AssetDatabase.LoadAssetAtPath<IOCIncludedAssemblies>(Constants.Strings.IOC_INCLUDED_ASM_PATH);
    }
#endif

    public void MapClasses()
    {
        classes = new List<ClassInfo>();
        classes.Clear();
        var monos = FindObjectsOfType<MonoBehaviour>(true);
        foreach (MonoBehaviour mono in monos)
        {
            if (mono == null) continue;
            if(!assemblies.Assemblies.Exists(asm => mono.GetType().Assembly.GetName().Name ==  asm)) continue;
            ClassInfo info = new()
            {
                implementation = mono,
                isSingleton = mono.GetType().IsSubclassOf(typeof(MonoSingleton<>))
            };
            classes.Add(info);
        }
    }
}