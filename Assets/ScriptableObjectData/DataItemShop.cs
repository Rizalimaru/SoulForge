using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShopUpgradeData", menuName = "Shop/Upgrade Data")]
public class ShopUpgradeData : ScriptableObject
{
    public StatType statType;
    public Sprite icon;
    public UpgradeLevel[] levels; // 5 level upgrade

    [System.Serializable]
    public class UpgradeLevel
    {
        public int price;
        public string description;
        public float value; // Nilai upgrade (misal: +10 HP, +0.2 speed, dst)
    }
}

public enum StatType
{
    HP,
    Attack,
    PickRadius,
    MovementSpeed,
    HPRegen,
    AttackSpeed
}

public class DataItemShop : MonoBehaviour
{

}
