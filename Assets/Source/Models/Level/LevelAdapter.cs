using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using UnityEngine;
using static Cinemachine.DocumentationSortingAttribute;


[DisallowMultipleComponent]
public class LevelAdapter : MonoBehaviour, IInitializable
{
    public int GetUpgradeTypeCount => upgradeController.GetUpgradeTypesCount;
    [SerializeField] private MultiplePoolModel[] worldItemPools;
    [Dependency] private UpgradeController upgradeController;
    public void Initialize()
    {
        this.Inject();
    }

    public void LoadLevel(LevelModel level, bool isEditorMode = false)
    {
        ClearScene();
        
        ActivatePoolObjects(level.poolItems.ToArray());
        ActivateWorldObjects(level.worldItems.ToArray());

        if (!isEditorMode)
        {
            upgradeController.InitializeLevelUpgrades(level.index);
        }
    }

    public void SaveAll(LevelModel lvlModel)
    {
        var poolItems = FindObjectsOfType<PoolItemModel>();
        var worldItems = FindObjectsOfType<WorldItemModel>();
        List<PoolItemDataModel> poolItemDatas = new List<PoolItemDataModel>();
        List<WorldItemDataModel> worldItemDatas = new List<WorldItemDataModel>();

        foreach (PoolItemModel poolItemModel in poolItems)
        {
            poolItemDatas.Add(poolItemModel.GetData());
        }
        foreach (WorldItemModel worldItemModel in worldItems)
        {
            worldItemDatas.Add(worldItemModel.GetData());
        }
        lvlModel.poolItems = poolItemDatas;
        lvlModel.worldItems = worldItemDatas;
    }
    public void ActivatePoolObjects(PoolItemDataModel[] items)
    {
        for (int i = 0; i < items.Length; i++)
        {
            var itemData = items[i];
            var item = worldItemPools[items[i].multiplePoolIndex].GetDeactiveItem<PoolItemModel>(items[i].poolIndex);
            item.SetValues(itemData);
            item.SetActiveGameObject(true);
        }
    }

    public void ActivateWorldObjects(WorldItemDataModel[] items)
    {
        var worldItems = FindObjectsOfType<WorldItemModel>(true);
        for (int i = 0; i < items.Length; i++)
        {
            var itemData = items[i];
            var item = worldItems.FirstOrDefault(a => a.id == items[i].Id);
            item.SetValues(itemData);
            item.SetActiveGameObject(true);
        }
    }
    
    public void ClearScene()
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
}
