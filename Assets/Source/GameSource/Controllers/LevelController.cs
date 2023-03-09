using UnityEngine;
using Object = UnityEngine.Object;
using System;
using UnityEngine.Events;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class LevelController : MonoBehaviour, IInitializable
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
    private LevelList levelModels;

    private void Awake()
    {
        if (!initializeOnAwake) return;
        Init();
    }

    public void Initialize()
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
                MapUpgradeData upgradeData = new MapUpgradeData(i);
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
    private void DeserializeLevels()
    {
        levelModels = JsonHelper.LoadJson<LevelList>(levels.ToString());
    }

    private void LoadLevelHelper(int levelIndex, bool isEditorMode = false)
    {
        if (levelModels.list.Count == 0) return;
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

    private void ClearScene()
    {
        LevelAdapter.ClearScene();
        activeLevel = null;
    }
    #endregion
    
#if UNITY_EDITOR
    private void GetLevels()
    {
        Object asset = AssetDatabase.LoadAssetAtPath<Object>(Constants.Strings.LEVELS_PATH);
        levels = asset;
    }
#endif

    
}