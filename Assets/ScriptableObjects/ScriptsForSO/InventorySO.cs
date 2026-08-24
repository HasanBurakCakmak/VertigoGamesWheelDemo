using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "InventorySO", menuName = "Items/Inventory")]
public class InventorySO : ScriptableObject, ISerializationCallbackReceiver
{
    [System.Serializable]
    public class InventorySlotClass
    {
        public Sprite invSprite;
        public int quantity=0;
    };

    public InventorySlotClass cashSlot;
    public InventorySlotClass goldSlot;
    public Dictionary<Sprite, int> nonCurrencySlots;

    [HideInInspector] [SerializeField] List<Sprite> savedSprites = new List<Sprite>();
    [HideInInspector] [SerializeField] List<int> savedQuantity = new List<int>();

    public void OnBeforeSerialize()
    {
        savedSprites.Clear();
        savedQuantity.Clear();
        foreach(var kvp in nonCurrencySlots)
        {
            savedSprites.Add(kvp.Key);
            savedQuantity.Add(kvp.Value);
        }
    }

    public void OnAfterDeserialize()
    {
        nonCurrencySlots=new Dictionary<Sprite, int>();
        for (int i= 0; i < savedSprites.Count; i++)
        {
            nonCurrencySlots.Add(savedSprites[i], savedQuantity[i]);
        }
    }

}