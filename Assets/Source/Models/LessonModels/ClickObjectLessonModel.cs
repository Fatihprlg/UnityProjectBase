using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickObjectLessonModel : LessonModel
{
    [SerializeField] Transform objectToClick;
    [SerializeField] RectTransform finger;
    [SerializeField] Canvas canvas;
    [SerializeField] Camera mainCam;

    public override void PlayLesson()
    {
        finger.anchoredPosition = WorldToScreenPoint(mainCam, canvas, objectToClick.position);
        
        base.PlayLesson();
    }
    private Vector2 WorldToScreenPoint(Camera camera, Canvas canvas, Vector3 targetPos)
    {
        Vector2 myPositionOnScreen = camera.WorldToScreenPoint(targetPos);
        float scaleFactor = canvas.scaleFactor;
        var result = new Vector2(myPositionOnScreen.x / scaleFactor, myPositionOnScreen.y / scaleFactor);
        return result;
    }
}
