using UnityEngine;
using Object = UnityEngine.Object;
using System;
using UnityEngine.Events;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class LevelController : MonoBase
{
    public bool initializeOnAwake;
    public int forceLevelIndex = -1;
    public LevelModel activeLevel;
    public int maxLevelCount => levelModels.list.Count;
    [SerializeField] private Object levels;
    [SerializeField] private UnityEvent onLevelLoaded;
    [SerializeField] private LevelAdapter levelAdapter;
    [Dependency] private GameController _gameController;
    private SceneController _sceneController;
    private readonly string levelsPath = $"Assets/GameAssets/Levels/Levels.json";
    private LevelList levelModels;

    private void Awake()
    {
        if (!initializeOnAwake) return;
        Init();
    }

    public override void Initialize()
    {
        if (initializeOnAwake) return;
        Init();
    }

    private void Init()
    {
        this.Inject();
        levelAdapter.Initialize();
#if UNITY_EDITOR
        GetLevels();
#endif
        _sceneController = SceneController.Instance;
        DeserializeLevels();
        if (MapUpgradeDataModel.Data.upgradeDatas.Count < levelModels.list.Count)
        {
            for (int i = MapUpgradeDataModel.Data.upgradeDatas.Count; i < levelModels.list.Count; i++)
            {
                var upgradeData = new MapUpgradeData(i);
                MapUpgradeDataModel.Data.upgradeDatas.Add(upgradeData);
            }
        }
        MapUpgradeDataModel.Data.InitUpgradeLevels(levelAdapter.GetUpgradeTypeCount);
        activeLevel = null;
        LoadLevel(forceLevelIndex >= 0 ? forceLevelIndex : PlayerDataModel.Data.LevelIndex);
    }
    
    private void LoadLevel(int levelIndex)
    {
        if (levelIndex < 0 || levelIndex >= levelModels.list.Count) levelIndex = 0;
        PlayerDataModel.Data.Level = levelIndex + 1;
        PlayerDataModel.Data.LevelIndex = levelIndex;
        
        LoadLevelHelper(levelIndex);
    }

    public void NextLevel()
    {
        var levelIndex = PlayerDataModel.Data.LevelIndex + 1;
        if (levelIndex >= levelModels.list.Count) levelIndex = 0;
        PlayerDataModel.Data.Level = levelIndex + 1;
        PlayerDataModel.Data.LevelIndex = levelIndex;
        _sceneController.RestartScene();
    }
 
    public void ReplayLevel()
    {
        _sceneController.RestartScene();
    }
    
    #region UTILS

#if UNITY_EDITOR
    private void SaveWorldItems(LevelModel level, string path, bool _override = false)
    {
        levelAdapter.SaveAll(level);
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
#endif
    private string GetRightPartOfPath(string path, string after)
    {
        var parts = path.Split(System.IO.Path.DirectorySeparatorChar);
        int afterIndex = Array.IndexOf(parts, after);

        if (afterIndex == -1) return null;

        return string.Join(System.IO.Path.DirectorySeparatorChar.ToString(),
        parts, afterIndex, parts.Length - afterIndex);
    }

    private void DeserializeLevels()
    {
        levelModels = JsonHelper.LoadJson<LevelList>(levels.ToString());
    }

    private void LoadLevelHelper(int levelIndex, bool isEditorMode = false)
    {
        if (levelModels.list.Count <= levelIndex)
        {
            levelIndex = 0;
        }
        else if (levelIndex < 0)
        {
            levelIndex = levelModels.list.Count - 1;
        }
        
        ClearScene();
        activeLevel = levelModels.list[levelIndex];
        levelAdapter.LoadLevel(activeLevel, isEditorMode);
        onLevelLoaded?.Invoke();
    }

    #endregion
    #region EDITOR
    public void ClearScene()
    {
        levelAdapter.ClearScene();
        activeLevel = null;
    }
#if UNITY_EDITOR

    public void E_LoadLevel(int levelIndex)
    {
        if(levelModels == null || levelModels.list.Count <= 0) DeserializeLevels();
        LoadLevelHelper(levelIndex, true);
    }

    public void SaveLevel()
    {
        DeserializeLevels();
        LevelModel level = new LevelModel();

        level.name = "Level " + levelModels.list.Count.ToString();
        level.index = levelModels.list.Count;

        SaveWorldItems(level, levelsPath);

        var asset = AssetDatabase.LoadAssetAtPath(levelsPath, typeof(Object));
        levels = asset;
        ClearScene();
    }
    public void OverrideLevel()
    {
        if(levelModels == null) DeserializeLevels();
        SaveWorldItems(activeLevel, levelsPath, true);
    }
    

    /*private void OnValidate()
    {
        DeserializeLevels();
    }*/

    public List<Object> GetLevels()
    {
        var path = $"/GameAssets/Levels/";
        var files = System.IO.Directory.GetFiles(Application.dataPath + path, "*.json");
        List<Object> res = new List<Object>();
        foreach (var item in files)
        {
            string p = GetRightPartOfPath(item, "Assets");
            var asset = AssetDatabase.LoadAssetAtPath<Object>(p);
            res.Add(asset);
        }
        return res;
    }
#endif
    #endregion


}


#if UNITY_EDITOR
[CustomEditor(typeof(LevelController))]
public class LevelControllerEditor : Editor
{
    private LevelController levelController;
    private int editorLevelIndex;
    private GUIContent saveContent, loadContent, clearSceneContent, overrideContent, resetDataContext;

    private void OnEnable()
    {
        saveContent = new GUIContent();
        saveContent.text = "Save Level";

        loadContent = new GUIContent();
        loadContent.text = "Load Level";

        overrideContent = new GUIContent();
        overrideContent.text = "Override Level";

        clearSceneContent = new GUIContent();
        clearSceneContent.text = "Clear Scene";

        resetDataContext = new GUIContent();
        resetDataContext.text = "Reset Levels";
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

#endif


