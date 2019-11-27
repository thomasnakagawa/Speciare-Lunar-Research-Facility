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
    private string pendingItem;

    public string HeldInventoryItem;

    private DialogBox dialogBox;

    private int IgnorePlayerLayerMask;

    private EventSystem eventSystem;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        camManager = FindObjectOfType<CameraManager>();
        animator = GetComponentInChildren<Animator>();
        animator.SetInteger("AnimationPar", 0);

        dialogBox = SceneUtils.FindComponentInScene<DialogBox>();
        IgnorePlayerLayerMask = LayerMask.GetMask("Player");

        eventSystem = EventSystem.current;
    }

    public void MoveTo(Vector3 position)
    {
        pendingItem = null;
        pendingInteractive = null;
        navMeshAgent.destination = position;
    }

    public void InteractWith(Interactive interactive)
    {
        pendingItem = null;
        pendingInteractive = interactive;
        navMeshAgent.destination = pendingInteractive.InteractPoint.position;
    }

    public void UseItemOn(string item, Interactive interactive)
    {
        pendingItem = item;
        pendingInteractive = interactive;
        navMeshAgent.destination = pendingInteractive.InteractPoint.position;
    }

    // Update is called once per frame
    void Update()
    {
        // interaction
        if (pendingInteractive != null)
        {
            Vector3 playerPosOnPlane = new Vector3(transform.position.x, 0f, transform.position.z);
            Vector3 interactivePosOnPlane = new Vector3(pendingInteractive.InteractPoint.position.x, 0f, pendingInteractive.InteractPoint.position.z);
            if (Vector3.Distance(playerPosOnPlane, interactivePosOnPlane) < InteractionDistanceThresh)
            {
                Debug.Log("GOT TO " + pendingInteractive.ObjectName);
                navMeshAgent.destination = transform.position;
                if (pendingItem == null)
                {
                    pendingInteractive.Interact();
                    pendingInteractive = null;
                }
                else
                {
                    pendingInteractive.UseItemOn(pendingItem);
                    pendingInteractive = null;
                    pendingItem = null;
                }
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
