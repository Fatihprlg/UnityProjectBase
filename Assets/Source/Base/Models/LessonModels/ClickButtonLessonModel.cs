using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Object = UnityEngine.Object;

public class ClickButtonLessonModel : LessonModel
{
    public float FingerScaleUpDownDuration;  
    [HideInInspector] public Button ButtonToClick;
    
    public override void PlayLesson()
    {
        RectTransform buttonPos = ButtonToClick.gameObject.transform as RectTransform;
        FingerTransform.position = buttonPos!.position;
        Sequence lessonSequence = DOTween.Sequence();
        lessonSequence.Append(FingerTransform.DOScale(.7f, FingerScaleUpDownDuration));
        lessonSequence.Append(FingerTransform.DOScale(1f, FingerScaleUpDownDuration));
        lessonSequence.SetLoops(-1);
        lessonSequence.SetId(this);
        ButtonToClick.onClick.AddListener(StopLesson);
        base.PlayLesson();
        lessonSequence.Play();
    }

    public override void StopLesson()
    {
        DOTween.Kill(this);
        ButtonToClick.onClick.RemoveListener(StopLesson);
        base.StopLesson();
    }
}
