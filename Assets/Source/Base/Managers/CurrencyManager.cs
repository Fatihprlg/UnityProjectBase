using System.Collections.Generic;
using DG.Tweening;
using UnityEngine.Events;

public class CurrencyManager : MonoSingleton<CurrencyManager>, ICrossSceneObject, IInitializable, ISubject<int>
{
    public UnityAction OnCurrencyUpdate;
    public int CurrencyAmount
    {
        get => data.Currency;
        set => data.Currency = value;
    }
    private PlayerDataModel data;
    
    public void Initialize()
    {
        destroyGameObjectOnDuplicate = true;
        base.Init();
        if(destroyed) return;
        HandleDontDestroy();
        data = PlayerDataModel.Data;
        
        Observers ??= new List<IObserver<int>>();
        NotifyObservers(CurrencyAmount);
    }

    public void OnSceneUnload(SceneModel sceneModel)
    {
        var observersToRemove = new List<IObserver<int>>();
        DOTween.Complete(this, true);
        foreach (var observer in Observers)
        {
            if (!observer.GetType().IsSubclassOf(typeof(ICrossSceneObject)))
                observersToRemove.Add(observer);
        }

        foreach (var observer in observersToRemove)
        {
            Observers.Remove(observer);
        }
    }
    
    public void UpdateCurrencyInstant(int amount)
    {
        CurrencyAmount += amount;
        OnCurrencyUpdate?.Invoke();

        NotifyObservers(CurrencyAmount);
    }
    public void DecreaseCurrencyInstant(int amount)
    {
        CurrencyAmount -= amount;
        OnCurrencyUpdate?.Invoke();

        NotifyObservers(CurrencyAmount);
    }

    public void UpdateCurrencySmooth(int amount)
    {
        int currencyTemp = CurrencyAmount;
        int finalNum = CurrencyAmount += amount;
        CurrencyAmount = finalNum;

        DOTween.To(() => currencyTemp, x => currencyTemp = x, finalNum, .3f)
            .OnUpdate(()=>NotifyObservers(currencyTemp))
            .SetId(this).Play();

        OnCurrencyUpdate?.Invoke();
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

    public List<IObserver<int>> Observers { get; set; }
    public void NotifyObservers(int value)
    {
        foreach (var observer in Observers)
        {
            observer.Notify(value);
        }
    }

    public void RegisterObserver(IObserver<int> observer)
    {
        Observers ??= new List<IObserver<int>>();
        if(!Observers.Contains(observer))Observers.Add(observer);
        NotifyObservers(CurrencyAmount);
    }

    public void UnRegisterObserver(IObserver<int> observer)
    {
        Observers ??= new List<IObserver<int>>();
        if(Observers.Contains(observer))Observers.Remove(observer);
    }
}