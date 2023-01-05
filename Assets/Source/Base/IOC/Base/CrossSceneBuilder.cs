using System.Collections.Generic;
using System.Linq;
using UnityEngine;
#if UNITY_EDITOR
using System.Reflection;
using UnityEditor;
#endif

public class CrossSceneBuilder : MonoSingleton<CrossSceneBuilder>, IBuilder, ICrossSceneObject
{
    [SerializeField , HideInInspector] protected List<ClassInfo> classes;
    public override void Initialize()
    {
        destroyGameObjectOnDuplicate = true;
        base.Initialize();
        if(destroyed) return;
        HandleDontDestroy();
    }

    public void HandleDontDestroy()
    {
        this.transform.SetParent(null);
        DontDestroyOnLoad(gameObject);
    }

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
        var crossSceneObjs = monos.Where(mono => mono.GetType().GetInterfaces().Contains(typeof(ICrossSceneObject)));
        foreach (var objs in crossSceneObjs)
        {
            if(objs == null) continue;
            ClassInfo info = new ()
            {
                implementation = objs,
                isSingleton = true
            };
            classes.Add(info);
        }
    }
#endif
}