using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private float UpdateInterval = 1f;
    private GameObject Player;
    private GameObject[] Cameras;

    private Camera currentCamera;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        Cameras = GameObject.FindGameObjectsWithTag("Camera");

        StartCoroutine(UpdateCameraAtInterval());
    }

    public Camera currentCam => currentCamera;

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
        GameObject closestCamObj;
        try
        {
            // find the closest camera that can see the player
            closestCamObj = Cameras.Where(cam =>
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
            Camera closestCam = closestCamObj.GetComponent<Camera>();

            if (currentCamera != null)
            {
                currentCamera.enabled = false;
            }
            closestCam.enabled = true;
            currentCamera = closestCam;
        }
    }
}
