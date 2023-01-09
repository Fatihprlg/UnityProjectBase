using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class DataHandler : MonoSingleton<DataHandler>, ICrossSceneObject
{
    public SettingsDataModel Setting;
    public PlayerDataModel Player;
    public TutorialDataModel Tutorial;
    public MapUpgradeDataModel Upgrade;
    private bool isInitialized;
    public override void Initialize()
    {
        destroyGameObjectOnDuplicate = true;
        base.Initialize();
        if(destroyed) return;
        HandleDontDestroy();
        Setting = new SettingsDataModel().Load();
        Player = new PlayerDataModel().Load();
        Tutorial = new TutorialDataModel().Load();
        Upgrade = new MapUpgradeDataModel().Load();
        isInitialized = true;
    }

    [EditorButton()]
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
    public void HandleDontDestroy()
    {
        transform.SetParent(null);
        DontDestroyOnLoad(gameObject);
    }

#if UNITY_EDITOR
    [EditorButton()]
    public void E_CreateNewDataModel(string DataName)
    {
        var regexItem = new Regex("^[a-zA-Z0-9 ]*$");

        if (DataName != null && !char.IsNumber(DataName.ToCharArray().ElementAt(0)) && regexItem.IsMatch(DataName))
        {
            DataName = DataName.Replace(" ", "");
            string targetPath = Application.dataPath + "/Source/Base/Models/DataModels/" + DataName + ".cs";
            string sampleDataModelPath = Application.dataPath + "/Source/Base/Models/DataModels/SampleDataModel.cs";
            string sampleDataModelText = File.ReadAllText(sampleDataModelPath);
            sampleDataModelText = sampleDataModelText.Replace("SampleDataModel", DataName);

            if (File.Exists(targetPath) == false)
            {
                Debug.Log("Creating DataModel: " + targetPath);
                using StreamWriter outfile =
                    new StreamWriter(targetPath);
                outfile.Write(sampleDataModelText);
            }
            else
                Debug.LogError("There is a data model with the same name!");
            AssetDatabase.Refresh();
        }
        else
        {
            Debug.LogError("Check Data Name!");
        }

    }
    
    [EditorButton()]
    public void E_GetMoney(int amount = 1000000)
    {
        CurrencyManager.Instance.UpdateCurrencyInstant(amount);
    }
#endif
    
}