using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OatsUtil;

public class Cursor : MonoBehaviour
{
    private RectTransform descriptor;
    private RectTransform background;
    private TMPro.TMP_Text objectText;
    private TMPro.TMP_Text actionText;

    private bool isShowing = false;

    private void Start()
    {
        descriptor = this.RequireDescendantGameObject("CursorDescriptor").RequireComponent<RectTransform>();
        background = descriptor.RequireChildGameObject("Background").RequireComponent<RectTransform>();
        objectText = background.RequireChildGameObject("ObjectLabel").RequireComponent<TMPro.TMP_Text>();
        actionText = background.RequireChildGameObject("ActionLabel").RequireComponent<TMPro.TMP_Text>();
    }
    private void Update()
    {
        if (isShowing)
        {
            descriptor.position = Input.mousePosition;
            if (Input.mousePosition.x > (Screen.width - background.sizeDelta.x))
            {
                background.pivot = Vector2.one;
            }
            else
            {
                background.pivot = Vector2.up;
            }
        }
    }

    public void SetText(string objectName, string action)
    {
        descriptor.gameObject.SetActive(true);
        objectText.text = objectName;
        actionText.text = "Click to " + action;
        isShowing = true;
    }

    public void HideText()
    {
        descriptor.gameObject.SetActive(false);
        isShowing = false;
    }
}
