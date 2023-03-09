using DG.Tweening;
using TMPro;
using UnityEngine;

public class CurrencyViewModel : ScreenElement
{
    [SerializeField] private TextMeshProUGUI currencyText;
    [SerializeField] private Transform currencyIcon;
    public void UpdateCurrencyText(string txt)
    {
        currencyText.text = txt;
    }
    
    public void CurrencyIconAnim()
    {
        DOTween.Complete(this);
        Sequence sequence = DOTween.Sequence().SetId(this);

        var scaleUp = currencyIcon.DOScale(1.35f, .1f);
        var scaleDown = currencyIcon.DOScale(1f, .1f);
        
        sequence.Append(scaleUp);
        sequence.Append(scaleDown);
        sequence.Play();
    }
}