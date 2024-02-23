using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : BaseManager<CameraManager>
{
    public CinemachineVirtualCameraBase killCamera;

    public void EnableKillCam()
    {
        killCamera.Priority = 20;
    }
}
