using UnityEditor;
using UnityEngine;

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
                Rect rect = GUILayoutUtility.GetLastRect();
                rect.width = EditorStyles.linkLabel.CalcSize(name).x;
                EditorGUIUtility.AddCursorRect(rect, MouseCursor.Link);
            }

        EditorUtils.DrawUILine(Color.white);
        EditorGUILayout.EndVertical();
    }
}