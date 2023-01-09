using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
#if UNITY_EDITOR
using System.Reflection;
using UnityEditor;
#endif
public class Builder : MonoBase, IBuilder
{
    [SerializeField ] protected List<ClassInfo> classes;

    public void Build(Container container)
    {
        foreach (var item in classes)
        {
            if(item.implementation == null) continue;
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

    public void MapClasses()
    {
        classes.Clear();
        var monos = FindObjectsOfType<MonoBase>(true);
        foreach (var mono in monos)
        {
            if(mono == null) continue;
            ClassInfo info = new ClassInfo()
            {
                implementation = mono,
                isSingleton = mono.GetType().IsSubclassOf(typeof(MonoSingleton<>))
            };
            classes.Add(info);
        }
    }
#endif
}