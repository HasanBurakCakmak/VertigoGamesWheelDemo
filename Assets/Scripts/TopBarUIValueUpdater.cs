using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TopBarUIValueUpdater : MonoBehaviour
{
    [SerializeField] private InventorySO _inventorySO;
    [SerializeField] private TextMeshProUGUI cashValue;
    [SerializeField] private TextMeshProUGUI goldValue;
    [SerializeField] private InventoryManager _inventoryManager;

    private void OnEnable()
    {
        _inventoryManager.OnInventoryChanged += SetTopBarValues;
    }
    private void OnDisable()
    {
        _inventoryManager.OnInventoryChanged -= SetTopBarValues;
    }
    private void Start()
    {
        SetTopBarValues();
    }

    private void SetTopBarValues()
    {
        if (_inventorySO != null)
        {
            if (cashValue == null) { Debug.Log("cashMissing!"); }
            if (goldValue == null) { Debug.Log("goldMissing!"); }
            cashValue.SetText(_inventorySO.cashSlot.quantity.ToString());
            goldValue.SetText(_inventorySO.goldSlot.quantity.ToString());
        }
    }
}
