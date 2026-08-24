using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TopBarInventoryToogler : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Button inventoryButton;
    [SerializeField] private GameObject inventoryScreen;
    private void OnEnable()
    {
        inventoryButton.onClick.AddListener(ToggleInventory);
    }

    private void OnDisable()
    {
        inventoryButton.onClick.RemoveListener(ToggleInventory);
    }

    private void ToggleInventory()
    {
        bool isOpen = inventoryScreen.activeSelf;
        inventoryScreen.SetActive(!isOpen);
    }
}
