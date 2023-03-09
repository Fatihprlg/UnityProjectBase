using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeViewModel : ScreenElement
{
    [SerializeField] private Button[] upgradeButtons;
    [SerializeField] private TextMeshProUGUI[] priceTxts;
    [SerializeField] private TextMeshProUGUI[] levelTxts;

    public void UpdateButtonUI(int upgradeID, int newPrice, int newLevel)
    {
        priceTxts[upgradeID].text = "$" + newPrice.ToCoinValues();
        levelTxts[upgradeID].text = "Level " + newLevel;
    }
    public void SetMaxUpgradeUI(int upgradeID)
    {
        priceTxts[upgradeID].text = "MAX";
        levelTxts[upgradeID].text = "MAX";
        SetButtonInteractable(upgradeID, false);
    }
    public void SetButtonInteractable(int upgradeID, bool state)
    {
        upgradeButtons[upgradeID].interactable = state;
    }

    public void SetActiveButton(int upgradeID, bool state)
    {
        upgradeButtons[upgradeID].gameObject.SetActive(state);
    }
}
