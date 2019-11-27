﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// player can interact with these
/// </summary>
public class Interactive : MonoBehaviour
{
    [SerializeField] private UnityEvent OnInteract = default;
    public string ObjectName = "InteractiveObject";
    public string Verb = "use";

    [SerializeField] private DialogLine[] InspectLines = default;

    [Header("Item use")]
    [SerializeField] private UnityEvent OnCorrectUse = default;
    [SerializeField] private string CorrectItemName = default;
    [SerializeField] private DialogLine[] CorrectUseLines = default;

    public Transform InteractPoint => InteractPosition == null ? transform : InteractPosition;

    private Transform InteractPosition;

    private void Start()
    {
        InteractPosition = transform.Find("InteractPoint");
    }

    public virtual void Interact()
    {
        OnInteract.Invoke();
        if (InspectLines != null && InspectLines.Length > 0)
        {
            FindObjectOfType<DialogBox>().ShowDialog(InspectLines);
        }
    }

    public virtual bool UseItemOn(string item)
    {
        Debug.Log("Used " + item + " on " + ObjectName);
        if (item.ToLower().Equals(CorrectItemName.ToLower()))
        {
            OnCorrectUse.Invoke();
            FindObjectOfType<DialogBox>().ShowDialog(CorrectUseLines);
            return true;
        }
        else
        {
            return false;
        }
    }
}
