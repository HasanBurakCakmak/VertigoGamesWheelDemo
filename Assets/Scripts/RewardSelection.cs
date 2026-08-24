using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardSelection : MonoBehaviour
{
    [SerializeField] private BaseRotationScript _wheelBaseRotation;
    [SerializeField] private ZoneManagementSO _zoneManagementSO;
    [SerializeField] private List<(ItemSO item, int quantity)> currentWheelItems;
    [SerializeField] private RewardRandomSelection _rewardRandomSelection;

   
    public event Action<(ItemSO item, int quantity)> OnNonDeathRewardSelected;
    public event Action OnDeathRewardSelected;
    private void OnEnable()
    {
        if (_rewardRandomSelection != null)
        {
            _rewardRandomSelection.OnRandomRewardSelection += SetCurrentWheelItems;
        }
        if (_wheelBaseRotation != null)
        {
            _wheelBaseRotation.OnSpinComplete += HandleSelectedReward;
        }
        
    }
    private void OnDisable()
    {
        if (_rewardRandomSelection != null)
        {
            _rewardRandomSelection.OnRandomRewardSelection -= SetCurrentWheelItems;
        }
        if (_wheelBaseRotation != null)
        {
            _wheelBaseRotation.OnSpinComplete -= HandleSelectedReward;
        }
    }

    private void SetCurrentWheelItems(List<(ItemSO item, int quantity)> items)
    {
        currentWheelItems = items;
        Debug.Log("Wheel Items Set for Selection!");
    }

  

    private void HandleSelectedReward(int offset)
    {
        (ItemSO item, int quantity) selectedItem;
        if (currentWheelItems != null)
        {
            selectedItem = currentWheelItems[offset];
            Debug.Log(selectedItem);
            if (selectedItem.quantity == 0)
            {
                OnDeathRewardSelected?.Invoke();
            }
            else
            {
                OnNonDeathRewardSelected?.Invoke(selectedItem);
                _zoneManagementSO.AdvanceStage();
            }

        }
    }

}
