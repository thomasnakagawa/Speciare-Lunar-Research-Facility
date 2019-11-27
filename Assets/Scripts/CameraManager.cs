using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private float UpdateInterval = 1f;
    private GameObject Player;
    private CameraController[] CameraPositions;
    private MovingCamera CameraToMove;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        CameraPositions = FindObjectsOfType<CameraController>();
        CameraToMove = FindObjectOfType<MovingCamera>();

        foreach (Camera cam in FindObjectsOfType<Camera>())
        {
            if (cam != CameraToMove.GetComponent<Camera>())
            {
                Destroy(cam);
            }
        }
        StartCoroutine(UpdateCameraAtInterval());
    }

    public Camera currentCam => CameraToMove.GetComponent<Camera>();

    private IEnumerator UpdateCameraAtInterval()
    {
        while (true)
        {
            UpdateActiveCamera();
            yield return new WaitForSeconds(UpdateInterval);
        }
    }

    private void UpdateActiveCamera()
    {
        CameraController closestCamObj;
        try
        {
            // find the closest camera that can see the player
            closestCamObj = CameraPositions.Where(cam =>
            {
                if (Physics.Raycast(cam.transform.position, Player.transform.position - cam.transform.position, out var hit))
                {
                    if (hit.collider.CompareTag("Player"))
                    {
                        return true;
                    }
                }
                return false;
            }).OrderBy(cam => Vector3.Distance(cam.transform.position, Player.transform.position)).First();
        }
        catch (System.InvalidOperationException e)
        {
            return;
        }

        if (closestCamObj != null)
        {
            CameraToMove.TargetController = closestCamObj;
        }
    }
}
