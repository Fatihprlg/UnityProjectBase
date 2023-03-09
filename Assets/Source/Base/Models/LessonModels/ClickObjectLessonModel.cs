using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickObjectLessonModel : LessonModel
{
    public Transform objectToClick;
    public Canvas canvas;
    private LayerMask objLayerMask;
    private Ray ray;
    private Camera mainCam;

    public override void PlayLesson()
    {
        objLayerMask = objectToClick.gameObject.layer;
        mainCam = CameraController.MainCamera;
        FingerTransform.anchoredPosition = objectToClick.position.WorldToScreenPoint(mainCam, canvas);
        base.PlayLesson();
    }

    private void Update()
    {
        if (!Input.GetMouseButtonDown(0)) return;
        ray = mainCam.ScreenPointToRay(Input.mousePosition);
        if(!Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, objLayerMask)) return;
        if(hit.collider.gameObject == objectToClick.gameObject) StopLesson();
    }
}
