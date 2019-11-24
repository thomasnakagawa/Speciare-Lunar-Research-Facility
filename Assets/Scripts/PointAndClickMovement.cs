using System.Collections;
using System.Collections.Generic;
using OatsUtil;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PointAndClickMovement : MonoBehaviour
{
    [SerializeField] private float InteractionDistanceThresh = 0.2f;

    private Animator animator;
    private NavMeshAgent navMeshAgent;
    private CameraManager camManager;

    private Interactive pendingInteractive;

    private DialogBox dialogBox;

    private int IgnorePlayerLayerMask;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        camManager = FindObjectOfType<CameraManager>();
        animator = GetComponentInChildren<Animator>();
        animator.SetInteger("AnimationPar", 0);

        dialogBox = SceneUtils.FindComponentInScene<DialogBox>();
        IgnorePlayerLayerMask = LayerMask.GetMask("Player");

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(0);
        }

        // mouse click
        if (dialogBox.IsShowingDialog == false)
        {
            if (Physics.Raycast(camManager.currentCam.ScreenPointToRay(Input.mousePosition), out var hit, IgnorePlayerLayerMask))
            {
                Interactive interactive = hit.collider.GetComponent<Interactive>();

                if (interactive != null)
                {
                    FindObjectOfType<Cursor>().SetText(interactive.ObjectName, interactive.Verb);
                }
                else
                {
                    FindObjectOfType<Cursor>().HideText();
                }
                if (Input.GetMouseButtonDown(0))
                {
                    if (interactive != null)
                    {
                        Debug.Log("GOING TO " + interactive.ObjectName);
                        pendingInteractive = interactive;
                        navMeshAgent.destination = pendingInteractive.InteractPoint.position;
                    }
                    else
                    {
                        pendingInteractive = null;
                        navMeshAgent.destination = hit.point;
                    }
                }
            }
        }

        // interaction
        if (pendingInteractive != null)
        {
            Vector3 playerPosOnPlane = new Vector3(transform.position.x, 0f, transform.position.z);
            Vector3 interactivePosOnPlane = new Vector3(pendingInteractive.InteractPoint.position.x, 0f, pendingInteractive.InteractPoint.position.z);
            if (Vector3.Distance(playerPosOnPlane, interactivePosOnPlane) < InteractionDistanceThresh)
            {
                Debug.Log("GOT TO " + pendingInteractive.ObjectName);
                navMeshAgent.destination = transform.position;
                pendingInteractive.Interact();
                pendingInteractive = null;
            }
        }

        // animation
        if (navMeshAgent.velocity.magnitude > 0f)
        {
            animator.speed = navMeshAgent.velocity.magnitude / 2f;
            animator.SetInteger("AnimationPar", 1);
        }
        else
        {
            animator.SetInteger("AnimationPar", 0);

        }
    }
}
