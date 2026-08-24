using System.Collections;
using System.Collections.Generic;
using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static InventorySO;


public class InventoryManager : MonoBehaviour
{
    [SerializeField] private RewardSelection SCR_rewardSelection;
    [SerializeField] private InventorySO _inventorySO;
    [SerializeField] private ZoneManagementSO _zoneManagementSO;
    [SerializeField] private TemporaryInventory rewardsInventory;
    [SerializeField] private EndGameControls endGameCall;


    public event Action<Sprite,int> OnRewardsListChanged;
    public event Action OnInventoryChanged;
    
    [System.Serializable]
    public class TemporaryInventory
    {
        public InventorySlotClass cashSlot;
        public InventorySlotClass goldSlot;
        public Dictionary<Sprite,int> nonCurrencySlots=new Dictionary<Sprite, int>();
    }

    private void OnEnable()
    {
        if (SCR_rewardSelection != null)
        {
            SCR_rewardSelection.OnNonDeathRewardSelected += AddRewardToRewardsInventory;
        }
        endGameCall.OnGameEnd += MoveItemsToPermanentInventory;
        endGameCall.OnRetry += PayForRetry;
    }

    private void OnDisable()
    {
        if (SCR_rewardSelection != null) 
        {
            SCR_rewardSelection.OnNonDeathRewardSelected -= AddRewardToRewardsInventory;
        }
        endGameCall.OnGameEnd -= MoveItemsToPermanentInventory;
        endGameCall.OnRetry -= PayForRetry;
    }


    private void AddRewardToRewardsInventory((ItemSO item,int quantity) itemToAdd)
    {
        int finalRewardQuantity;
        if (itemToAdd.item == null)
        {
            Debug.Log("No Reward Sent To Inventory Manager!!");
        }
        if (itemToAdd.item.ItemSprite == rewardsInventory.cashSlot.invSprite)
        {
            finalRewardQuantity=rewardsInventory.cashSlot.quantity + itemToAdd.quantity;
            rewardsInventory.cashSlot.quantity = finalRewardQuantity;

        } else if (itemToAdd.item.ItemSprite == rewardsInventory.goldSlot.invSprite)
        {
            finalRewardQuantity=rewardsInventory.goldSlot.quantity + itemToAdd.quantity;
            rewardsInventory.goldSlot.quantity = finalRewardQuantity;
        }
        else
        {
            if (rewardsInventory.nonCurrencySlots.ContainsKey(itemToAdd.item.ItemSprite))
            {
                finalRewardQuantity= rewardsInventory.nonCurrencySlots[itemToAdd.item.ItemSprite] + itemToAdd.quantity;
                rewardsInventory.nonCurrencySlots[itemToAdd.item.ItemSprite] = finalRewardQuantity;
            }
            else
            {
                rewardsInventory.nonCurrencySlots.Add(itemToAdd.item.ItemSprite, itemToAdd.quantity);
                finalRewardQuantity= itemToAdd.quantity;
            }
        }
        OnRewardsListChanged?.Invoke(itemToAdd.item.ItemSprite,finalRewardQuantity);
    }

    private void PayForRetry(int payValue)
    {
        _inventorySO.goldSlot.quantity -= payValue;
        OnInventoryChanged?.Invoke();
    }
    
    public TemporaryInventory getPrivateInv()
    {
        return rewardsInventory;
    }

    private void ResetRewardsInv()
    {
        rewardsInventory.cashSlot.quantity=0;
        rewardsInventory.goldSlot.quantity=0;
        rewardsInventory.nonCurrencySlots.Clear();
    }

    private void MoveItemsToPermanentInventory(bool isCollected)
    {
        if (!isCollected)
        {
            ResetRewardsInv();
            return;
        }
        _inventorySO.cashSlot.quantity += rewardsInventory.cashSlot.quantity;
        _inventorySO.goldSlot.quantity += rewardsInventory.goldSlot.quantity;

        foreach (  Sprite rewardedSlot in rewardsInventory.nonCurrencySlots.Keys)
        {
            if (_inventorySO.nonCurrencySlots.ContainsKey(rewardedSlot))
            {
                _inventorySO.nonCurrencySlots[rewardedSlot] += rewardsInventory.nonCurrencySlots[rewardedSlot];
            }
            else
            {
                _inventorySO.nonCurrencySlots.Add(rewardedSlot,rewardsInventory.nonCurrencySlots[rewardedSlot]);
            }
        }
        ResetRewardsInv();
        OnInventoryChanged?.Invoke();

        Debug.Log("Transfer Complete!!");
    }
}
