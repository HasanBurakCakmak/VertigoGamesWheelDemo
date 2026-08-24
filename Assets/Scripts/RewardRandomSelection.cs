using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardRandomSelection : MonoBehaviour
{
    [SerializeField] private WheelAssets _wheelAssets;
    [SerializeField] private ZoneManagementSO _zoneManagementSO;
    [SerializeField] private ItemSO _deathItemSO;
    public event  Action<List<(ItemSO item, int quantity)>> OnRandomRewardSelection;
    private void OnEnable()
    {
        _zoneManagementSO.OnStageChange += RandomRewardsSelection;
    }
    private void OnDisable()
    {
        _zoneManagementSO.OnStageChange -= RandomRewardsSelection;
    }

    private void RandomRewardsSelection()
    {
        ZoneManagementSO.GameZone currentGameZone=ZoneManagementSO.GameZone.Bronze;
        if (_zoneManagementSO != null)
        {
            currentGameZone = _zoneManagementSO.getCurrentZone();
        }

        List<ItemSO> currentZoneList=_wheelAssets.BronzeItems;
        if (_wheelAssets != null)
        {
            switch (currentGameZone)
            {
                case ZoneManagementSO.GameZone.Bronze: { currentZoneList = _wheelAssets.BronzeItems; break; }
                case ZoneManagementSO.GameZone.Silver: { currentZoneList = _wheelAssets.SilverItems; break; }
                case ZoneManagementSO.GameZone.Gold: { currentZoneList = _wheelAssets.GoldItems; break; }
                default: { Debug.Log("Error in switch Case"); break; }
            }
        }
        int listLen = currentZoneList.Count;
        List<(ItemSO item,int quantity)> selectedList = new List<(ItemSO item, int quantity)>();
        int deathPlacement= UnityEngine.Random.Range(0, 8);
        float currMultiplier = _zoneManagementSO.GetStageMultiplier();
        for (int i = 0; i < 8; i++)
        {
            ItemSO itemToAdd = currentZoneList[UnityEngine.Random.Range(0, listLen)];
            int quantityToAdd = RewardQuantityCalculation(itemToAdd.ItemQuantity, currMultiplier);
            if (i != deathPlacement || currentGameZone!=ZoneManagementSO.GameZone.Bronze) selectedList.Add((itemToAdd,quantityToAdd));
            else selectedList.Add((_deathItemSO,0));
        }
        OnRandomRewardSelection?.Invoke(selectedList);
    }

    private int RewardQuantityCalculation(int baseQuantity, float stageMultiplier)
    {
        int mult = Mathf.RoundToInt( baseQuantity * stageMultiplier);
        if (mult > 100)
        {
            return (mult / 25) * 25;
        }
        if (mult > 10)
        {
            return (mult / 10) * 10;
        }
        else
        {
            return mult;
        }
    }
}
