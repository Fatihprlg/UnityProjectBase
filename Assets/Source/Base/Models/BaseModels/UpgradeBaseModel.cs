using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[System.Serializable]
public class UpgradeBaseModel
{
    public string name;
    public int id;
    [SerializeField] protected int defPrice;
    [SerializeField] protected float pricePerLevel;
    [SerializeField] protected float pricePerMap;
    [SerializeField] protected float increaseRate;
    protected int currentUpgradeLevel;
    protected int CurrentMapIndex => PlayerDataModel.Data.LevelIndex;
    protected int currentPrice;
    public int GetCurrentUpgradeLevel()
    {
        return currentUpgradeLevel;
    }

    public virtual int GetCurrentPrice()
    {
        currentPrice = Convert.ToInt32(defPrice + (pricePerMap * CurrentMapIndex) + Mathf.Pow(currentUpgradeLevel, ( pricePerLevel) + (CurrentMapIndex + 1)));
        return currentPrice;
    } 
    public virtual ulong GetIncreaseAmount()
    {
        var incrAmount = Mathf.RoundToInt((increaseRate * currentUpgradeLevel * (CurrentMapIndex + 1)) * Mathf.Log10(increaseRate * currentUpgradeLevel* (CurrentMapIndex + 1)));
        return Convert.ToUInt64(incrAmount);
    }

    public virtual float GetIncreaseRate()
    {
        return increaseRate;
    }

    public void Init()
    {
        if(MapUpgradeDataModel.Data.upgradeDatas[CurrentMapIndex].upgradeLevels.Count <= id )
        {
            MapUpgradeDataModel.Data.upgradeDatas[CurrentMapIndex].upgradeLevels.Insert(id, 1);
        }
        
        currentUpgradeLevel = MapUpgradeDataModel.Data.upgradeDatas[CurrentMapIndex].upgradeLevels[id];
    }

    public void Upgrade()
    {
        currentUpgradeLevel = ++MapUpgradeDataModel.Data.upgradeDatas[CurrentMapIndex].upgradeLevels[id];
    }
}
