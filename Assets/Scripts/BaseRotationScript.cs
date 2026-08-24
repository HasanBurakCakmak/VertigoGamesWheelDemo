using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseRotationScript : MonoBehaviour
{
    [SerializeField] private Button spinButton;
    [SerializeField] private Button collectButton;
    [SerializeField] private int fullspin = 2;
    private float offsetAngle = 360 / 8;
    [SerializeField] private float duration = 3.0f;
    public event Action<int> OnSpinComplete;
    private int lastOffset = 0;
    private void OnValidate()
    {
        if (spinButton == null)
        {
            spinButton = GetComponent<Button>();
        }
    }
    private void OnEnable()
    {
        spinButton.onClick.AddListener(SpinLogic);
    }

    private void OnDisable()
    {
        spinButton.onClick.RemoveListener(SpinLogic);
    }


    private void SpinLogic()
    {
        collectButton.interactable = false;
        spinButton.interactable=false;
        int offset = (int)(UnityEngine.Random.Range(0,8));
        float spinDegree = 360 * fullspin + offset*offsetAngle;
            transform.DORotate(Vector3.forward * -spinDegree, duration, RotateMode.FastBeyond360).SetEase(Ease.OutCubic).SetRelative(true).OnComplete(
        () =>
        {
            spinButton.interactable = true;
            collectButton.interactable = true;
            offset = (lastOffset + offset) % 8;
            lastOffset= offset;
            OnSpinComplete?.Invoke(offset);
        });
        Debug.Log($"offset:{offset}");
    }
}
