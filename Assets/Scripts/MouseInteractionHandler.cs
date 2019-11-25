using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using OatsUtil;

public class MouseInteractionHandler : MonoBehaviour
{
    private LayerMask IgnorePlayerLayerMask;
    private EventSystem eventSystem;

    private PointAndClickMovement Player;

    private DialogBox dialogBox;
    private CameraManager cameraManager;
    private Cursor cursor;

    public string heldItem;

    // Use this for initialization
    void Start()
    {
        eventSystem = EventSystem.current;
        IgnorePlayerLayerMask = LayerMask.GetMask("Player");

        Player = SceneUtils.FindComponentInScene<PointAndClickMovement>();
        dialogBox = SceneUtils.FindComponentInScene<DialogBox>();
        cameraManager = SceneUtils.FindComponentInScene<CameraManager>();
        cursor = SceneUtils.FindComponentInScene<Cursor>();
    }

    // Update is called once per frame
    void Update()
    {
        // calculate values
        Interactive hoveredInteractive = null;
        Vector3 raycastPosition = Vector3.zero;
        if (Physics.Raycast(cameraManager.currentCam.ScreenPointToRay(Input.mousePosition), out var hit, IgnorePlayerLayerMask))
        {
            hoveredInteractive= hit.collider.GetComponent<Interactive>();
            raycastPosition = hit.point;
        }
        bool mouseClicked = Input.GetMouseButtonDown(0);

        bool dialogIsOpen = dialogBox.IsShowingDialog;
        bool mouseIsOverUI = eventSystem.IsPointerOverGameObject();

        // perform action based on calculated values
        if (dialogIsOpen == false)
        {
            if (mouseClicked && !mouseIsOverUI)
            {
                if (heldItem == null && hoveredInteractive == null)
                {
                    Player.MoveTo(raycastPosition);
                }
                else if (heldItem == null && hoveredInteractive != null)
                {
                    Player.InteractWith(hoveredInteractive);
                }
                else if (heldItem != null && hoveredInteractive == null)
                {
                    // do nothing?
                    heldItem = null;
                }
                else if (heldItem != null && hoveredInteractive != null)
                {
                    Player.UseItemOn(heldItem, hoveredInteractive);
                }
            }

            // set cursor
            if (hoveredInteractive == null)
            {
                cursor.HideText();
            }
            else
            {
                cursor.SetText(hoveredInteractive.ObjectName, hoveredInteractive.Verb);
            }
        }
        else
        {
            cursor.HideText();
        }
    }

    public void OnSelectInventoryItem(string item)
    {
        heldItem = item;
    }
}
