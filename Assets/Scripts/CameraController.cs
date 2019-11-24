using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	private Camera camera;
	private Transform player;
    // Start is called before the first frame update
    void Start()
    {
		camera = GetComponent<Camera>();
		player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
		if (camera.enabled)
		{
			transform.LookAt(player);
		}
	}
}
