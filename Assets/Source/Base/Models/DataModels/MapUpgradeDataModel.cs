using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MapUpgradeDataModel : DataModel
{
    public static MapUpgradeDataModel Data;
    public int upgradeTypeCount;
    public List<MapUpgradeData> upgradeDatas = new List<MapUpgradeData>();


    public MapUpgradeDataModel Load()
    {
        if (Data == null)
        {
            Data = this;
            object data = LoadData();

            if (data != null)
            {
                Data = (MapUpgradeDataModel)data;
            }
        }
        return Data;
    }


    public void Save()
    {
        Save(Data);
    }

    public void InitUpgradeLevels(int upgradeTypesCount)
    {
        upgradeTypeCount = upgradeTypesCount;
        foreach (var item in Data.upgradeDatas)
        {
            if (item.upgradeLevels == null) item.upgradeLevels = new();
            if (item.upgradeLevels.Count < upgradeTypeCount)
            {
                for (int i = item.upgradeLevels.Count; i < upgradeTypeCount; i++)
                {
                    item.upgradeLevels.Add(1);
                }
            }
        }
    }
}

[System.Serializable]
public class MapUpgradeData
{
    public int levelIndex;
    public List<int> upgradeLevels;

    public MapUpgradeData()
    {
        upgradeLevels = new();

    }
    public MapUpgradeData(int lvlIndex)
    {
        levelIndex = lvlIndex;
        upgradeLevels = new();
    }
}