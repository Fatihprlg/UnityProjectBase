using System.Linq;
using UnityEditor;
using UnityEngine;

public class LevelEditor : EditorWindow
{
    [SerializeField] private MultiplePoolModel[] worldItemPools;
    [SerializeField] private LevelModel activeLevel;
    private int editorLevelIndex;
    private Object levels;
    private SerializedObject serializedObject;
    private LevelAdapter levelAdapter;
    private LevelList levelModels;
    private GUIContent saveContent, loadContent, clearSceneContent, overrideContent, resetDataContext;
    private void OnEnable()
    {
        serializedObject = new SerializedObject(this);
        GetLevels();
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

    [MenuItem("My Game Lib/Level Editor")]
    public static void ShowWindow()
    {
        GetWindow(typeof(LevelEditor));
    }

    private void OnGUI()
    {
        EditorGUILayout.BeginVertical();
        EditorGUILayout.Space(10);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("worldItemPools"));
        EditorGUILayout.Space(5);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("activeLevel"));
        EditorGUILayout.Space(10);
        serializedObject.ApplyModifiedProperties();

        EditorUtils.DrawUILine(Color.white);
        
        if (GUILayout.Button(saveContent))
        {
            SaveLevel();
        }
        EditorUtils.DrawUILine(Color.white);
        if (GUILayout.Button(overrideContent))
        {
            OverrideLevel();
        }
        EditorUtils.DrawUILine(Color.white);
        editorLevelIndex = EditorGUILayout.IntField("Level Index to Load", editorLevelIndex);
        if (GUILayout.Button(loadContent))
        {
            LoadLevel(editorLevelIndex);
        }
        EditorUtils.DrawUILine(Color.white);
        if (GUILayout.Button(clearSceneContent))
        {
            ClearScene();
        }
        EditorUtils.DrawUILine(Color.white);
        if (GUILayout.Button(resetDataContext))
        {
            ResetLevelsData();
        }
        EditorUtils.DrawUILine(Color.white);
        EditorGUILayout.EndVertical();
    }

    private void ResetLevelsData()
    {
        levelModels.list.Clear();
        JsonHelper.SaveJson(levelModels, Constants.Strings.LEVELS_PATH);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
    private void ActivatePoolObjects(PoolItemDataModel[] items)
    {
        for (int i = 0; i < items.Length; i++)
        {
            PoolItemDataModel itemData = items[i];
            PoolItemModel item = worldItemPools[items[i].multiplePoolIndex].GetDeactiveItem<PoolItemModel>(items[i].poolIndex);
            item.SetValues(itemData);
            item.SetActiveGameObject(true);
        }
    }

    private static void ActivateWorldObjects(WorldItemDataModel[] items)
    {
        var worldItems = FindObjectsOfType<WorldItemModel>(true);
        for (int i = 0; i < items.Length; i++)
        {
            WorldItemDataModel itemData = items[i];
            WorldItemModel item = worldItems.FirstOrDefault(a => a.id == items[i].Id);
            item.SetValues(itemData);
            item.SetActiveGameObject(true);
        }
    }
    
    private static void SaveAll(LevelModel lvlModel)
    {
        var poolItems = FindObjectsOfType<PoolItemModel>();
        var worldItems = FindObjectsOfType<WorldItemModel>();

        var poolItemDatas = poolItems.Select(poolItemModel => poolItemModel.GetData()).ToList();
        var worldItemDatas = worldItems.Select(worldItemModel => worldItemModel.GetData()).ToList();
        lvlModel.poolItems = poolItemDatas;
        lvlModel.worldItems = worldItemDatas;
    }
    
    private void SaveWorldItems(LevelModel level, string path, bool _override = false)
    {
        Undo.RecordObjects(new []{levels, levelModels}, "SaveWorldItems");
        SaveAll(level);
        if (_override)
        {
            levelModels.list.Insert(activeLevel.index, level);
            levelModels.list.Remove(activeLevel);
        }
        else levelModels.list.Add(level);

        JsonHelper.SaveJson(levelModels, path);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
    private void DeserializeLevels()
    {
        levelModels = JsonHelper.LoadJson<LevelList>(levels.ToString());
    }
    private static void ClearScene()
    {
        var poolItems = FindObjectsOfType<PoolItemModel>();
        var worldItems = FindObjectsOfType<WorldItemModel>();
        foreach (PoolItemModel poolItemModel in poolItems)
        {
            poolItemModel.SetActiveGameObject(false);
        }
        foreach (WorldItemModel worldItemModel in worldItems)
        {
            worldItemModel.SetActiveGameObject(false);
        }
    }
    private void LoadLevel(int levelIndex)
    {
        if (levelModels == null || levelModels.list.Count == 0) DeserializeLevels();
        if (levelModels.list.Count == 0) return;
        activeLevel = levelModels.list.FirstOrDefault(lv => lv.index == levelIndex);
        if (activeLevel is null)
        {
            Debug.LogError($"There is no level with given index {levelIndex}");
            return;
        }
        ClearScene();
        ActivatePoolObjects(activeLevel.poolItems.ToArray());
        ActivateWorldObjects(activeLevel.worldItems.ToArray());
    }

    private void SaveLevel()
    {
        DeserializeLevels();
        LevelModel level = new()
        {
            name = $"Level {levelModels.list.Count}",
            index = levelModels.list.Count
        };

        SaveWorldItems(level, Constants.Strings.LEVELS_PATH);
        GetLevels();
        ClearScene();
    }

    private void GetLevels()
    {
        Object asset = AssetDatabase.LoadAssetAtPath<Object>(Constants.Strings.LEVELS_PATH);
        levels = asset;
        DeserializeLevels();
    }
    private void OverrideLevel()
    {
        if (levelModels == null) DeserializeLevels();
        SaveWorldItems(activeLevel, Constants.Strings.LEVELS_PATH, true);
    }

}