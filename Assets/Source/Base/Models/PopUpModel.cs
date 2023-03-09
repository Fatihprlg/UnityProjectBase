using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class PopUpModel : MonoBehaviour
{
    public UnityAction onHide;
    public UnityAction onShow;
    [SerializeField] private PopUpDataModel Data;
    public PopUpDataModel GetData()
    {
        return Data;
    }

    public void SetData(PopUpDataModel data)
    {
        Data.SetData(data);
    }

    public void Show()
    {
        gameObject.SetActive(true);
        transform.DOScale(1, .5f)
            .OnComplete(()=> onShow?.Invoke());
    }

    public void Hide()
    {
        transform.DOScale(0, .5f).OnComplete(() =>
        {
            gameObject.SetActive(false);
            onHide?.Invoke();
        });
    }
}