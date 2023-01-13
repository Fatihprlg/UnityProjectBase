using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
//#if UNITY_EDITOR
using System.Reflection;
using UnityEditor;

//#endif
public class Builder : MonoBase, IBuilder
{
    //[SerializeField] protected List<ClassInfo> classes;
    public List<ClassInfo> classes { get; private set; }
    public void Build(Container container)
    {
        if(classes == null || classes.Count <1) MapClasses();
        foreach (var item in classes)
        {
            if (item.implementation == null) continue;
            var type = item.implementation.GetType();
            container.Register(type.BaseType, type, item);
        }
    }

//#if UNITY_EDITOR
    public void OnValidate()
    {
        AssemblyReloadEvents.afterAssemblyReload -= MapClasses;
        AssemblyReloadEvents.afterAssemblyReload += MapClasses;
    }

    public void MapClasses()
    {
        classes = new List<ClassInfo>();
        classes.Clear();
        var monos = FindObjectsOfType<MonoBase>(true);
        foreach (var mono in monos)
        {
            if (mono == null) continue;
            ClassInfo info = new ()
            {
                implementation = mono,
                isSingleton = mono.GetType().IsSubclassOf(typeof(MonoSingleton<>))
            };
            classes.Add(info);
        }
    }
//#endif
}

//#if UNITY_EDITOR
[CustomEditor(typeof(Builder))]
public class BuilderEditor : Editor
{
    private Builder builder;
    private GUIContent mapClassesContent;
    private GUIStyle linkStyle;
    private void OnEnable()
    {
        builder = target as Builder;
        mapClassesContent = new GUIContent("Map Classes On Scene");
        linkStyle= new GUIStyle(EditorStyles.label);
        linkStyle.wordWrap = false;
        // Match selection color w$$anonymous$$ch works nicely for both light and dark skins
        linkStyle.normal.textColor = new Color(0x00 / 255f, 0x78 / 255f, 0xDA / 255f, 1f);
        linkStyle.stretchWidth = false;
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

        for (int index = 0; index < builder.classes.Count; index++)
        {
            ClassInfo @class = builder.classes[index];
            if (GUILayout.Button( (index + 1).ToString() + ". " + @class.implementation.GetType().Name, EditorStyles.linkLabel))
            {
                Selection.SetActiveObjectWithContext(@class.implementation, null);
            }
            //GUILayout.Label(@class.implementation.GetType().Name);
        }

        EditorUtils.DrawUILine(Color.white);
        EditorGUILayout.EndVertical();
    }

}
//#endif