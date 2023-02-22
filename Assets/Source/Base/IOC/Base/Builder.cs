using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
#if UNITY_EDITOR
using System.Reflection;
using UnityEditor;
using UnityEngine.Rendering;

#endif

public class Builder : MonoBehaviour, IBuilder
{
    public List<ClassInfo> classes { get; private set; }

    public void Build(Container container)
    {
        if (classes == null || classes.Count < 1) MapClasses();
        foreach (var item in classes)
        {
            if (item.implementation == null) continue;
            var type = item.implementation.GetType();
            container.Register(type.BaseType, type, item);
        }
    }

#if UNITY_EDITOR
    public void OnValidate()
    {
        AssemblyReloadEvents.afterAssemblyReload -= MapClasses;
        AssemblyReloadEvents.afterAssemblyReload += MapClasses;
    }
#endif

    public void MapClasses()
    {
        classes = new List<ClassInfo>();
        classes.Clear();
        var monos = FindObjectsOfType<MonoBehaviour>(true);
        foreach (var mono in monos)
        {
            if (mono == null) continue;
            if(mono.GetType().Assembly != Assembly.GetAssembly(GetType())) continue;
            ClassInfo info = new()
            {
                implementation = mono,
                isSingleton = mono.GetType().IsSubclassOf(typeof(MonoSingleton<>))
            };
            classes.Add(info);
        }
    }
}