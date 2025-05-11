using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponLevelData", menuName = "WeaponData/WeaponLevelData")]
public class WeaponLevelData : ScriptableObject
{
    public int level; // Level senjata
    public float damageMultiplier; // Pengali damage berdasarkan level
    public float cooldownReduction; // Pengurangan cooldown berdasarkan level
    [TextArea(3, 5)]
    public string description; // Deskripsi khusus untuk level ini
}