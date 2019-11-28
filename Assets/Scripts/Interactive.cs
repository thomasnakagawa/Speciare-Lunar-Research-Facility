using System.Collections;
using System.Collections.Generic;
using OatsUtil;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// player can interact with these
/// </summary>
public class Interactive : MonoBehaviour
{
    [SerializeField] private UnityEvent OnInteract = default;
    public string ObjectName = "InteractiveObject";
    public string TakeName = "ItemName";
    public string Verb = "use";

    [SerializeField] private DialogLine[] InspectLines = default;

    [Header("Item use")]
    [SerializeField] private UnityEvent OnCorrectUse = default;
    [SerializeField] private string CorrectItemName = default;
    [SerializeField] private DialogLine[] CorrectUseLines = default;

    [SerializeField] private DialogLine[] DoneUsingLines = default;
    private bool isDoneUsing = false;

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
            if (isDoneUsing)
            {
                FindObjectOfType<DialogBox>().ShowDialog(DoneUsingLines);
            }
            else
            {
                FindObjectOfType<DialogBox>().ShowDialog(InspectLines);
            }
        }
    }

    public virtual bool UseItemOn(string item)
    {
        Debug.Log("Used " + item + " on " + ObjectName);
        if (item.ToLower().Equals(CorrectItemName.ToLower()) && !isDoneUsing)
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

    public void AddToInventory(bool take)
    {
        var item = new InventoryItem();
        item.ItemName = (TakeName != null && TakeName.Length > 0) ? TakeName : this.ObjectName;
        SceneUtils.FindComponentInScene<Inventory>().AddToInventory(item);
        if (take)
        {
            Destroy(gameObject);
        }
    }

    public void DoneUsing()
    {
        isDoneUsing = true;
    }
}
