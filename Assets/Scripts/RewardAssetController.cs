using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class RewardAssetController : MonoBehaviour
{
    [SerializeField] private InventoryManager inventoryManager;
    [SerializeField] private Transform scrollContent;
    [SerializeField] private GameObject rewardInventorySlotPrefab;
    [SerializeField] private EndGameControls endGameControls;

    private void OnEnable()
    {
        if (inventoryManager != null) 
        {
            inventoryManager.OnRewardsListChanged += PopulateRewardsInventory;
        }
        endGameControls.OnGameEnd += ClearRewardsInventory;
    }
    private void OnDisable()
    {
        if (inventoryManager != null)
        {
            inventoryManager.OnRewardsListChanged -= PopulateRewardsInventory;
        }
        endGameControls.OnGameEnd -= ClearRewardsInventory;
        
    }

    private void PopulateRewardsInventory(Sprite itemSprite,int finalItemQuantity)
    {
        bool foundMatch=false;
        foreach (Transform child in scrollContent)
        {
            ChildSerializationForRewardInvSlot childInfo = child.GetComponentInChildren<ChildSerializationForRewardInvSlot>();
            if (childInfo != null)
            {
                if (childInfo.getRewardSprite() == itemSprite)
                {
                    foundMatch = true;
                    childInfo.UpdateQuantity(finalItemQuantity);
                    break;
                }
            }
        }

        if (!foundMatch) {
            GameObject item=Instantiate(rewardInventorySlotPrefab, scrollContent);
            ChildSerializationForRewardInvSlot childInfo = item.GetComponentInChildren<ChildSerializationForRewardInvSlot>();
            childInfo.SetupSprite(itemSprite, finalItemQuantity);

        }
    }

    private void ClearRewardsInventory(bool isCollected)
    {
        foreach (Transform child in scrollContent.GetComponentInChildren<Transform>())
        {
            Destroy(child.gameObject);
        }
        Debug.Log("Rewards Assets Cleared!");
    }
}
