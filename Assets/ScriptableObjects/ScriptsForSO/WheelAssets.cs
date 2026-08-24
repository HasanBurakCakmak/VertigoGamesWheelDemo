using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName ="WheelTierAssets",menuName = "Wheel/WheelTierAssets")]
public class WheelAssets : ScriptableObject
{
    public Sprite BronzeBase;
    public Sprite BronzeInd;
    public List<ItemSO> BronzeItems;
    public Sprite SilverBase;
    public Sprite SilverInd;
    public List <ItemSO> SilverItems;
    public Sprite GoldBase;
    public Sprite GoldInd;
    public List<ItemSO> GoldItems;
}
