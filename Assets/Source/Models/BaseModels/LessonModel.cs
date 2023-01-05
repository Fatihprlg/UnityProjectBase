using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.Events;

public class LessonModel : MonoBase
{
    public bool selfManaged;
    public bool playByTrigger;
    public float playDelay;
    public UnityEvent OnLessonPlay;
    public UnityEvent OnLessonStop;
    [SerializeField] private string plainText;
    [SerializeField] private TextMeshProUGUI plainTxtObj;
    
    public virtual void PlayLesson()
    {
        transform.localScale = Vector3.zero;
        plainTxtObj.text = plainText;
        gameObject.SetActive(true);
        transform.DOScale(1, .3f).OnComplete(() =>
        {
            OnLessonPlay?.Invoke();
        });
    }

    public virtual void StopLesson()
    {
        transform.DOScale(0, .3f).OnComplete(() =>
        {
            gameObject.SetActive(false);
            OnLessonStop?.Invoke();
        });
    }
}
