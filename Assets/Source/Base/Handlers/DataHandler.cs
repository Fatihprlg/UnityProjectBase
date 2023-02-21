using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class DataHandler : MonoBehaviour, IInitializable
{
    public SettingsDataModel Setting;
    public PlayerDataModel Player;
    public TutorialDataModel Tutorial;
    public MapUpgradeDataModel Upgrade;
    private bool isInitialized;
    public void Initialize()
    {
        Setting = new SettingsDataModel().Load();
        Player = new PlayerDataModel().Load();
        Tutorial = new TutorialDataModel().Load();
        Upgrade = new MapUpgradeDataModel().Load();
        isInitialized = true;
    }

    public void ClearAllData()
    {
        string[] files = Directory.GetFiles(Application.persistentDataPath, "*.dat");
        for (int i = 0; i < files.Length; i++)
        {
            File.Delete(files[i]);
        }

        PlayerPrefs.DeleteAll();

        if (Directory.GetFiles(Application.persistentDataPath, "*.dat").Length == 0)
        {
            Debug.Log("Data Clear Succeed");
        }
    }

    private void SaveDatas()
    {
        if(!isInitialized) return;
        PlayerDataModel.Data.Save();
        TutorialDataModel.Data.Save();
        SettingsDataModel.Data.Save();
        MapUpgradeDataModel.Data.Save();
    }

    private void OnApplicationFocus(bool focus)
    {
        if (!focus)
        {
            SaveDatas();
        }
    }

}