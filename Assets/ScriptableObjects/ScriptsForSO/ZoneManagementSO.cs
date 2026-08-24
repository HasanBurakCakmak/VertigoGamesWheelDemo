using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[CreateAssetMenu(fileName = "NewZone", menuName = "Wheel/Zone")]
public class ZoneManagementSO : ScriptableObject
{
    public enum GameZone
    {
        Bronze,Silver,Gold
    }

    [SerializeField] private GameZone currentZone = GameZone.Bronze;
    [SerializeField] private int currentStage = 1;
    private float stageMultiplier=1f;
    private int initialStage = 1;
    public event Action OnStageChange;

    public int getCurrentStage()
    {
        return currentStage;
    }

    public void ResetStage()
    {
        currentStage = initialStage;
        setCurrentZone();
    }

    public void AdvanceStage()
    {
        currentStage++;
        setCurrentZone();

    }

    private void setCurrentZone()
    {
        if (currentStage % 30 == 0)
        { currentZone = GameZone.Gold; }
        else if (currentStage % 5 == 0)
        { currentZone = GameZone.Silver; }
        else  currentZone = GameZone.Bronze;
        SetStageMultiplier();
        OnStageChange?.Invoke();
    }
    public GameZone getCurrentZone()
    {
        return currentZone;
    }
    private void SetStageMultiplier()
    {
        stageMultiplier = (float)(Math.Pow((currentStage - 1),2) / 75) + 1;
    }
    public float GetStageMultiplier()
    {
        return stageMultiplier;
    }
}
