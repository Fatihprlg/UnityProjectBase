using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine.Events;

public class CurrencyManager : MonoSingleton<CurrencyManager>, ICrossSceneObject
{
    public CurrencyViewModel currencyView;
    public UnityAction OnCurrencyUpdate;
    public int CurrencyAmount
    {
        get
        {
            return data.Currency;
        }
        set
        {
            data.Currency = value;
        }
    }
    private PlayerDataModel data;
    
    public override void Initialize()
    {
        base.Initialize();
        HandleDontDestroy();
        data = PlayerDataModel.Data;
    }
    
    public void UpdateCurrencyInstant(int amount)
    {
        CurrencyAmount += amount;
        OnCurrencyUpdate?.Invoke();

        UpdateCurrencyText();
    }
    public void DecreaseCurrencyInstant(int amount)
    {
        CurrencyAmount -= amount;
        OnCurrencyUpdate?.Invoke();

        UpdateCurrencyText();
    }

    public void UpdateCurrencySmooth(int amount)
    {
        int currencyTemp = CurrencyAmount;
        int finalNum = CurrencyAmount += amount;
        CurrencyAmount = finalNum;

        DOTween.To(() => currencyTemp, x => currencyTemp = x, finalNum, .3f)
            .OnUpdate(UpdateCurrencyText)
            .SetId(this).Play();

        currencyView.CurrencyIconAnim();
        OnCurrencyUpdate?.Invoke();
    }

    private void UpdateCurrencyText()
    {
        currencyView.UpdateCurrencyText( CurrencyAmount.ToCoinValues());
    }

    public bool CanAfford(int amount)
    {
        return CurrencyAmount >= amount;
    }
    public void HandleDontDestroy()
    {
        transform.SetParent(null);
        DontDestroyOnLoad(gameObject);
    }
}