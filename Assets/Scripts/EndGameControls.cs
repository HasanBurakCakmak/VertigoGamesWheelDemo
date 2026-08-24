using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGameControls : MonoBehaviour
{
    [SerializeField] private RewardSelection SCR_rewardSelection;
    [SerializeField] private Button collectButton;
    [SerializeField] private ZoneManagementSO _zoneManagementSO;
    [SerializeField] private GameObject deathScreen;
    [SerializeField] private ChildSerializationForDeathScreen deathScreenComponents;
    [SerializeField] private InventorySO _inventorySO;

    private int retryValue = 10;

    public event Action<bool> OnGameEnd; // bool value checks if the game ended with success or death
    public event Action<int> OnRetry;

    private void OnEnable()
    {
        collectButton.onClick.AddListener(CallGameEndWithCollect);
        SCR_rewardSelection.OnDeathRewardSelected += CallGameEndWithDeath;
        _zoneManagementSO.OnStageChange += DisableCollect;
        deathScreenComponents.getGiveUpButton().onClick.AddListener(GameLost);
        deathScreenComponents.getRetryButton().onClick.AddListener(PayForRetry);
    }

    private void OnDisable()
    {
        collectButton.onClick.RemoveListener(CallGameEndWithCollect);
        SCR_rewardSelection.OnDeathRewardSelected -= CallGameEndWithDeath;
        _zoneManagementSO.OnStageChange -= DisableCollect;
        deathScreenComponents.getGiveUpButton().onClick.RemoveListener(GameLost);
        deathScreenComponents.getRetryButton().onClick.RemoveListener(PayForRetry);
    }
    //Resets Stage At start so we start from 0
    private void Start()
    {
        _zoneManagementSO.ResetStage();
    }

    //Disables collectButton 
    private void DisableCollect()
    {
        bool enabled = (_zoneManagementSO.getCurrentZone() == ZoneManagementSO.GameZone.Bronze);
        collectButton.interactable=enabled;
    }
    // Ended Game With Collect
    private void CallGameEndWithCollect()
    {
        OnGameEnd?.Invoke(true);
        _zoneManagementSO.ResetStage();
    }
    //Death Card Pulled
    private void CallGameEndWithDeath()
    {
        retryValue = (_zoneManagementSO.getCurrentStage() / 5)+1; //retry value is calculated based off of the stage 
        retryValue = retryValue * 10;
        deathScreenComponents.SetRetryValue(retryValue.ToString());
        bool enableButton = (_inventorySO.goldSlot.quantity >= retryValue);

        deathScreenComponents.getRetryButton().interactable=enableButton;
        collectButton.interactable=false;
        deathScreen.SetActive(true);
    }
    private void PayForRetry()
    {
        OnRetry?.Invoke(retryValue);
        deathScreen.SetActive(false);
    }


    private void GameLost()
    {
        OnGameEnd?.Invoke(false);
        deathScreen.SetActive(false);
        _zoneManagementSO.ResetStage();
    }

}
