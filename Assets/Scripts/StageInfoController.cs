using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StageInfoController : MonoBehaviour
{
    [SerializeField] ZoneManagementSO zoneManagementSO;
    [SerializeField] TextMeshProUGUI stageValue;

    private void OnEnable()
    {
        zoneManagementSO.OnStageChange += SetStageCounter;
    }
    private void OnDisable()
    {
        zoneManagementSO.OnStageChange -= SetStageCounter;
    }
    private void SetStageCounter()
    {
        stageValue.text=zoneManagementSO.getCurrentStage().ToString();
    }
}
