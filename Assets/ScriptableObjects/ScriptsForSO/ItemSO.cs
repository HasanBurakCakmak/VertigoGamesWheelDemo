using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName ="NewItemSO",menuName ="WheelItems/ItemSO")]
public class ItemSO : ScriptableObject
{
    public Sprite ItemSprite;
    public string ItemName;
    public int ItemQuantity;//item quantity is 0 for only bombs
}