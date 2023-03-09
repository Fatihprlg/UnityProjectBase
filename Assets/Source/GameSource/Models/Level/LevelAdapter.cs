using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using UnityEngine;


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
    
    public static void ClearScene()
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
