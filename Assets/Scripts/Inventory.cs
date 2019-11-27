using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using OatsUtil;
using System.Linq;

public class Inventory : MonoBehaviour
{
    [SerializeField] private GameObject InventoryItemPrefab = default;

    private Button OpenInventoryButton;
    private RectTransform InventoryBox;
    private RectTransform ContentBox;

    private List<InventoryItem> Items;
    private List<Button> UIItems;

    private MouseInteractionHandler interactionHandler;

    // Start is called before the first frame update
    void Start()
    {
        Items = new List<InventoryItem>();
        UIItems = new List<Button>();

        interactionHandler = SceneUtils.FindComponentInScene<MouseInteractionHandler>();

        OpenInventoryButton = this.RequireChildGameObject("OpenButton").RequireComponent<Button>();
        InventoryBox = this.RequireDescendantGameObject("Background").RequireComponent<RectTransform>();
        ContentBox = this.RequireDescendantGameObject("Content").RequireComponent<RectTransform>();

        CloseInventory();

        var newItem = new InventoryItem();
        newItem.ItemName = "Keycard";
        AddToInventory(newItem);
    }

    private void RefreshView()
    {
        foreach (Button button in UIItems)
        {
            Destroy(button.gameObject);
        }
        UIItems.Clear();

        foreach (InventoryItem item in Items)
        {
            GameObject newUIItem = Instantiate(InventoryItemPrefab, ContentBox);
            Button newUIButton = newUIItem.transform.RequireComponent<Button>();
            newUIButton.GetComponentInChildren<TMPro.TMP_Text>().text = item.ItemName;

            newUIButton.onClick.AddListener(() =>
            {
                CloseInventory();
                interactionHandler.OnSelectInventoryItem(item.ItemName);
            });

            UIItems.Add(newUIButton);
        }
    }

    public void OpenInventory()
    {
        OpenInventoryButton.gameObject.SetActive(false);
        InventoryBox.gameObject.SetActive(true);
    }

    public void CloseInventory()
    {
        OpenInventoryButton.gameObject.SetActive(true);
        InventoryBox.gameObject.SetActive(false);
    }

    public void AddToInventory(InventoryItem item)
    {
        if (!Contains(item))
        {
            Items.Add(item);
            RefreshView();
        }
    }

    public void RemoveFromInventory(InventoryItem item)
    {
        Items = Items.Where(itm => itm.ItemName.Equals(item.ItemName) == false).ToList();
        RefreshView();
    }

    private bool Contains(InventoryItem item)
    {
        return Items.Count(itm => itm.ItemName.Equals(item.ItemName)) > 0;
    }
}
