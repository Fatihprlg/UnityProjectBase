using System.Collections.Generic;
using UnityEngine;

public class UpgradeController : ControllerBase
{
    public int GetUpgradeTypesCount => upgradeModels.Count;
    [SerializeField] List<UpgradeBaseModel> upgradeModels;
    [Dependency] UpgradeViewModel upgradeView;
    [Dependency] private CurrencyManager _currencyManager;

    public override void Initialize()
    {
        base.Initialize();
        this.Inject();
        _currencyManager.OnCurrencyUpdate += CheckButtonsDisabledState;
    }

    public void InitializeLevelUpgrades(int lvIndex)
    {
        foreach (var item in upgradeModels)
        {
            item.Init();
        }
    }
    private void UpdateUIButton(int upgradeIndex)
    {
        upgradeView.UpdateButtonUI(upgradeIndex, upgradeModels[upgradeIndex].GetCurrentPrice(),
            upgradeModels[upgradeIndex].GetCurrentUpgradeLevel());
        RecalculateIncome();
    }

    // ReSharper disable Unity.PerformanceAnalysis
    private void CheckButtonsDisabledState()
    {
        for (int i = 0; i < upgradeModels.Count; i++)
        {
            upgradeView.SetButtonInteractable(i, _currencyManager.CanAfford(upgradeModels[i].GetCurrentPrice()));
        }
    }
    
    private void UpdateUIButtons()
    {
        for (int i = 0; i < upgradeModels.Count; i++)
        {
            upgradeView.UpdateButtonUI(upgradeModels[i].id, upgradeModels[i].GetCurrentPrice(),
                upgradeModels[i].GetCurrentUpgradeLevel());
        }

        RecalculateIncome();
    }

    private void SetMaxUpgradeUI(int upgradeID)
    {
        upgradeView.SetMaxUpgradeUI(upgradeID);
    }

    private void RecalculateIncome()
    {
    }

}