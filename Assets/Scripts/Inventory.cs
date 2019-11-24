using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using OatsUtil;

public class Inventory : MonoBehaviour
{
    private Button OpenInventoryButton;
    private RectTransform InventoryBox;

    // Start is called before the first frame update
    void Start()
    {
        OpenInventoryButton = this.RequireChildGameObject("OpenButton").RequireComponent<Button>();
        InventoryBox = this.RequireDescendantGameObject("Background").RequireComponent<RectTransform>();

        CloseInventory();
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
