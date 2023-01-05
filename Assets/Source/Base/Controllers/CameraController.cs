using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class CameraController : ControllerBase
{
    [SerializeField] CinemachineVirtualCamera[] cameras;
    public CinemachineVirtualCamera ActiveCamera;

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
