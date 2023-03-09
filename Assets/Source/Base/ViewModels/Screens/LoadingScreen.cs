using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class LoadingScreen : ScreenModel
{
    [SerializeField] bool showOnAwake;
    [SerializeField] Image loadingBar;
    [SerializeField] Image[] visualsOnScreen; 
    [SerializeField] UnityEvent onProgressBarFilled;
    
    private void Awake()
    {
        if (showOnAwake)
            Show();
    }
    public override void Show()
    {
        base.Show();
        loadingBar.fillAmount = 0;
        float rnd = UnityEngine.Random.Range(1, 3);
        FillLoadingBar(rnd, onProgressBarFilled);
    }

    private void FillLoadingBar(float time, UnityEvent onComplete)
    {
        loadingBar.DOFillAmount(1, time)
            .SetId(this)
            .OnComplete(()=>onComplete?.Invoke());
    }
    public override void Hide()
    {
        foreach (var visual in visualsOnScreen)
        {
            visual.DOFade(0, .2f).SetId(this);
        }
        DOVirtual.DelayedCall(.21f,() =>
        {
            base.Hide();
            foreach (var visual in visualsOnScreen)
            {
                visual.color = new Color(visual.color.r, visual.color.g, visual.color.b, 1);
            }
        });
        
    }
}
