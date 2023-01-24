using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.Events;

public class LessonModel : MonoBehaviour
{
    public float PlayDelay;
    public UnityEvent OnLessonPlay;
    public UnityEvent OnLessonStop;
    public RectTransform FingerTransform;
    public string PlainText;
    public TextMeshProUGUI PlainTxtObj;

    public virtual void PlayLesson()
    {
        transform.localScale = Vector3.zero;
        PlainTxtObj.text = PlainText;
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
