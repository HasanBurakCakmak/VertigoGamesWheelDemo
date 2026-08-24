using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChildSerializationForRewardInvSlot : MonoBehaviour
{
    [SerializeField] private Image rewardAssetImage;
    [SerializeField] private TextMeshProUGUI rewardQuantity;

    public void SetupSprite(Sprite rewardSprite, int rewardAmount)
    {
        rewardAssetImage.sprite = rewardSprite;
        rewardQuantity.text = rewardAmount.ToString();
        rewardAssetImage.preserveAspect = true;
    }
    public void UpdateQuantity(int rewardAmount) 
    {
        rewardQuantity.text = rewardAmount.ToString();
    }
    public Sprite getRewardSprite()
    {
        return rewardAssetImage.sprite;
    }
}
