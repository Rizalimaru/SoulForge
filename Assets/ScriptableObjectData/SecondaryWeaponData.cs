using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SecondaryWeaponData", menuName = "WeaponData/SecondaryWeaponData")]
public class SecondaryWeaponData : ScriptableObject
{
    public string weaponName;

    [TextArea(3, 5)]
    public string description;

    public Sprite imageIcon;
    public float baseDamage; // Damage dasar senjata
    public float baseCooldown; // Cooldown dasar senjata
    public List<WeaponLevelData> levels; // Daftar level senjata
}
