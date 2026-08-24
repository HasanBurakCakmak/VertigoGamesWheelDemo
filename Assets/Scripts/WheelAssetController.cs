using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class WheelAssetController : MonoBehaviour
{
    [SerializeField] WheelAssets _wheelAssets;
    [SerializeField] ZoneManagementSO _zoneManagementSO;
    [SerializeField] RewardRandomSelection _rewardRandomSelection;
    private ZoneManagementSO.GameZone currentGameZone;
    private ZoneManagementSO.GameZone newZone;
    [SerializeField] private Transform wheelContainer;
    [SerializeField] private Image BaseImage;
    [SerializeField] private Image IndImage;
    [SerializeField] private GameObject RewardAssetPrefab;
    [SerializeField] private float radius;


    private void OnEnable()
    {
        if (_zoneManagementSO != null)
        {
            _zoneManagementSO.OnStageChange += HandleWheelZoneAssets;
           
        }
        if (_rewardRandomSelection != null)
        {
            _rewardRandomSelection.OnRandomRewardSelection += HandleRewardAssetPopulation;
        }
    }
    private void OnDisable()
    {
        if (_zoneManagementSO != null)
        {
            _zoneManagementSO.OnStageChange -= HandleWheelZoneAssets;
        }
        if (_rewardRandomSelection != null)
        {
            _rewardRandomSelection.OnRandomRewardSelection -= HandleRewardAssetPopulation;
        }
    }

    private void HandleWheelZoneAssets()
    {
        if (_zoneManagementSO != null)
        {
            newZone =_zoneManagementSO.getCurrentZone();
        }
        else
        {
            Debug.Log("Zone ManagementSO is missing!!!");
        }
        if (newZone == currentGameZone)
        {
            return;
        }
        Sprite baseName= _wheelAssets.BronzeBase;
        Sprite indName = _wheelAssets.BronzeInd;
        currentGameZone= newZone;
        switch (currentGameZone) {
            case ZoneManagementSO.GameZone.Bronze: { baseName = _wheelAssets.BronzeBase;indName = _wheelAssets.BronzeInd;break; }
            case ZoneManagementSO.GameZone.Silver: { baseName = _wheelAssets.SilverBase; indName = _wheelAssets.SilverInd; break; }
            case ZoneManagementSO.GameZone.Gold: { baseName = _wheelAssets.GoldBase; indName = _wheelAssets.GoldInd; break; }
            default: { Debug.Log("Error in switch Case"); break; }
        }
        BaseImage.sprite = baseName;
        IndImage.sprite = indName;
    }

    private void HandleRewardAssetPopulation(List<(ItemSO item,int quantity)> selectedList)
    {
        foreach(Transform child in wheelContainer)
        {
            Destroy(child.gameObject);
        }
        if(selectedList.Count != 8)
            {
            Debug.Log("ListProblem");
            return; 
        }

        for (int i = 0; i < selectedList.Count; i++) 
        {
            GameObject newWheelRewardItem = Instantiate<GameObject>(RewardAssetPrefab,wheelContainer,false);
            RectTransform rect = newWheelRewardItem.GetComponent<RectTransform>();
           
            float offsetAngle = -45f * i;
            float offsetRadian = offsetAngle*Mathf.Deg2Rad;

            float posX= radius* Mathf.Sin(offsetRadian);
            float posY= radius* Mathf.Cos(offsetRadian);

            rect.anchoredPosition= new Vector2(posX, posY);

            rect.localRotation= Quaternion.Euler(0f,0f,-offsetAngle);
            rect.localScale= Vector3.one/10;

            newWheelRewardItem.GetComponent<ChildSerializationForWheelRewards>().SetSprite(selectedList[i].item.ItemSprite);
            newWheelRewardItem.GetComponent<ChildSerializationForWheelRewards>().SetQuantity(selectedList[i].quantity);
        }
    }
}



