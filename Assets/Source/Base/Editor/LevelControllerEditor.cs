using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(LevelController))]
public class LevelControllerEditor : Editor
{
    private LevelController levelController;
    private int editorLevelIndex;
    private GUIContent saveContent, loadContent, clearSceneContent, overrideContent, resetDataContext;

    private void OnEnable()
    {
        saveContent = new GUIContent
        {
            text = "Save Level"
        };

        loadContent = new GUIContent
        {
            text = "Load Level"
        };

        overrideContent = new GUIContent
        {
            text = "Override Level"
        };

        clearSceneContent = new GUIContent
        {
            text = "Clear Scene"
        };

        resetDataContext = new GUIContent
        {
            text = "Reset Levels"
        };
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        EditorUtils.DrawUILine(Color.white);

        levelController = target as LevelController;

        EditorGUILayout.BeginVertical();

        if (GUILayout.Button(saveContent))
        {
            levelController.SaveLevel();
        }
        if (GUILayout.Button(overrideContent))
        {
            levelController.OverrideLevel();
        }
        editorLevelIndex = EditorGUILayout.IntField("Loaded Level Index", editorLevelIndex);
        if (GUILayout.Button(loadContent))
        {
            levelController.E_LoadLevel(editorLevelIndex);
        }

        if (GUILayout.Button(clearSceneContent))
        {
            levelController.ClearScene();
        }
        EditorGUILayout.EndVertical();
    }
}