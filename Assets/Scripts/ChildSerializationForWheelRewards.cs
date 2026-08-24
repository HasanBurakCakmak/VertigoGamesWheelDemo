using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChildSerializationForWheelRewards : MonoBehaviour
{
    [SerializeField] private Image wheelAssetImage;
    [SerializeField] private TextMeshProUGUI quantityHolder;

    public void SetSprite(Sprite sprite)
    {
        wheelAssetImage.sprite = sprite;
    }
    public void SetQuantity(int quantity)
    {
        quantityHolder.text = quantity.ToString();
        if (quantity == 0)
        {
            quantityHolder.gameObject.SetActive(false);
        }
    }
}
