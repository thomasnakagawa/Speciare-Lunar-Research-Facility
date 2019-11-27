using UnityEngine;
using System.Collections;

public class MovingCamera : MonoBehaviour
{
    public CameraController TargetController;

    private Transform Target;

    private void Start()
    {
        Target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (TargetController != null)
        {
            transform.position = TargetController.transform.position;
            switch (TargetController.CameraMode)
            {
                case CameraController.CameraModes.STATIONARY:
                    transform.rotation = TargetController.transform.rotation;
                    break;
                case CameraController.CameraModes.FOLLOW_PLAYER:
                    transform.LookAt(Target);
                    break;
            }
        }
    }
}
