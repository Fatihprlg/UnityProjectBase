using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ClickButtonLessonModel : LessonModel
{
    [SerializeField] RectTransform buttonPos;
    [SerializeField] Button buttonToClick;
    [SerializeField] RectTransform finger;
    
    public override void PlayLesson()
    {
        selfManaged = true;
        finger.position = buttonPos.position;
        buttonToClick.onClick.AddListener(StopLesson);
        base.PlayLesson();
    }

    public override void StopLesson()
    {
        buttonToClick.onClick.RemoveListener(StopLesson);
        base.StopLesson();
    }
}
