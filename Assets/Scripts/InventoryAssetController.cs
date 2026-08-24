using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InventoryAssetController : MonoBehaviour
{
    [SerializeField] private InventorySO _inventorySO;
    [SerializeField] private Transform invContent;
    [SerializeField] private GameObject inventoryAssetPrefab;

    private Dictionary<Sprite, ChildSerializationForInventoryAssets> populatedRewards= new Dictionary<Sprite,ChildSerializationForInventoryAssets>();


    private void Start()
    {
        PopulateInventory();
    }

    private void OnEnable()
    {
        PopulateInventory();
    }

    private void PopulateInventory() 
    {
        foreach(var invItem in _inventorySO.nonCurrencySlots)
        {
            if (populatedRewards.ContainsKey(invItem.Key))
            {
                ChildSerializationForInventoryAssets slotComponents = populatedRewards[invItem.Key];
                slotComponents.UpdateQuantity(invItem.Value);
            }
            else
            {
                GameObject newInvSlot= Instantiate(inventoryAssetPrefab, invContent);
                ChildSerializationForInventoryAssets slotComponents = newInvSlot.GetComponent<ChildSerializationForInventoryAssets>();
                slotComponents.SetupSlot(invItem.Key, invItem.Value);
                populatedRewards.Add(invItem.Key, slotComponents);
            }
        }
    }
}
