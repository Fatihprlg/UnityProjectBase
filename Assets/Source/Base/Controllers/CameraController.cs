using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class CameraController : ControllerBase
{
    public static Camera MainCamera { get; private set; }
    public CinemachineVirtualCamera ActiveCamera;
    [SerializeField] CinemachineVirtualCamera[] cameras;

    private void Awake()
    {
        MainCamera = Camera.main;
    }

    public void ChangeCamera(int index)
    {
        ActiveCamera.SetActiveGameObject(false);
        ActiveCamera = cameras[index];
        ActiveCamera.SetActiveGameObject(true);
    }

    private void Reset()
    {
        ActiveCamera = FindObjectOfType<CinemachineVirtualCamera>();
        if (ActiveCamera != null)
        {
            if (cameras.Length == 0)
            {
                cameras = new CinemachineVirtualCamera[]
                {
                    ActiveCamera
                };
            }
        }
    }
}
