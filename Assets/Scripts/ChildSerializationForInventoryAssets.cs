using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChildSerializationForInventoryAssets : MonoBehaviour
{
    [SerializeField] private Image itemImage;
    [SerializeField] private TextMeshProUGUI quantity;

    private int savedQuantity=0;

    public void SetupSlot(Sprite itemSprite ,int quantityToShow)
    {
        itemImage.sprite=itemSprite;
        quantity.text=quantityToShow.ToString();
        savedQuantity = quantityToShow;
    }

    public void UpdateQuantity(int quantityToShow)
    {
        savedQuantity += quantityToShow;
        quantity.text = savedQuantity.ToString();
        
    }
}
