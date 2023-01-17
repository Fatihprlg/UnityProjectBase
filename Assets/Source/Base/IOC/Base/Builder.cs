using System;
using System.Collections.Generic;
using System.Linq;
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

#if UNITY_EDITOR
[CustomEditor(typeof(Builder))]
public class BuilderEditor : Editor
{
    private Builder builder;
    private GUIContent mapClassesContent;

    private void OnEnable()
    {
        builder = target as Builder;
        mapClassesContent = new GUIContent("Map Classes On Scene");
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        EditorUtils.DrawUILine(Color.white);
        EditorGUILayout.BeginVertical();
        if (GUILayout.Button(mapClassesContent))
        {
            builder.MapClasses();
        }

        EditorUtils.DrawUILine(Color.white);
        GUILayout.Label("Classes on Scene");
        GUILayout.Space(5);
        if (builder.classes != null)
            for (int index = 0; index < builder.classes.Count; index++)
            {
                ClassInfo @class = builder.classes[index];
                GUIContent name = new ((index + 1) + ". " + @class.implementation.GetType().Name);
                if (GUILayout.Button(name,
                        EditorStyles.linkLabel))
                {
                    Selection.SetActiveObjectWithContext(@class.implementation, null);
                }
                var rect = GUILayoutUtility.GetLastRect();
                rect.width = EditorStyles.linkLabel.CalcSize(name).x;
                EditorGUIUtility.AddCursorRect(rect, MouseCursor.Link);
            }

        EditorUtils.DrawUILine(Color.white);
        EditorGUILayout.EndVertical();
    }
}
#endif