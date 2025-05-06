using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SecondaryWeaponData", menuName = "WeaponData/SecondaryWeaponData")]
public class SecondaryWeaponData : ScriptableObject
{   
    public string weaponName;
    public string description;
    public Sprite imageIcon;
    public float damage;
    public float time;
    public int level = 1; // Level awal senjata
    public int maxLevel = 3; // Level maksimal senjata
}
