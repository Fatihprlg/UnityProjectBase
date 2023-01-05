using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class SceneLoadingViewModel : ScreenModel
{
    public float MinLoadTime => minLoadTime;
    [SerializeField] private float minLoadTime;
    [SerializeField] private Slider loadingBar;

    public void UpdateLoadingBar(float progress)
    {
        loadingBar.value = progress;
    }

}