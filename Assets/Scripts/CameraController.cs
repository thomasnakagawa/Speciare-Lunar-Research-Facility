using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public enum CameraModes
    {
        STATIONARY,
        FOLLOW_PLAYER
    }

    public CameraModes CameraMode;
}
