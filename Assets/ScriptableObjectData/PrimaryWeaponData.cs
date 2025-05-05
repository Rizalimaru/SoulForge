using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PrimaryWeaponData", menuName = "WeaponData/PrimaryWeaponData")]
public class PrimaryWeaponData : ScriptableObject
{
    public string weaponName;
    public string weaponDescription;
    public Sprite image;
    public float damage;


}
