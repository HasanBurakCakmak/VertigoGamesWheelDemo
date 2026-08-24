using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class RewardNotification : MonoBehaviour
{
    [SerializeField] private RewardSelection rewardSelection;
    [SerializeField] private GameObject rewardNotification;
    [SerializeField] private Image rewardAsset;
    [SerializeField] private TextMeshProUGUI rewardQuantityTMP;

    [SerializeField] private CanvasGroup rewardGroup;
    private void OnEnable()
    {
        rewardSelection.OnNonDeathRewardSelected += RunRewardNotification;
    }
    private void OnDisable()
    {
        rewardSelection.OnNonDeathRewardSelected -= RunRewardNotification;
    }

    private void RunRewardNotification((ItemSO item, int quantity) itemInfo)
    {
        rewardAsset.sprite = itemInfo.item.ItemSprite;
        rewardQuantityTMP.text = itemInfo.quantity.ToString();
        DOTween.Kill(rewardGroup);
        rewardNotification.SetActive(true);
        rewardGroup.alpha = 0f;
        Sequence notifySequence = DOTween.Sequence();
        notifySequence.Append(rewardGroup.DOFade(1f, 0.3f));

        notifySequence.AppendInterval(1f);

        notifySequence.Append(rewardGroup.DOFade(0f, 0.5f));

        notifySequence.OnComplete(() =>rewardNotification.SetActive(false));
        
    }

}
