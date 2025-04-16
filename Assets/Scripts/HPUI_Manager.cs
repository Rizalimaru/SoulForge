using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPUI_Manager : MonoBehaviour
{
    public PlayerData playerData; // Referensi ke PlayerData
    public Image hpBar; // Referensi ke UI HP bar

    void Update()
    {
        hpBar.fillAmount = playerData.HP / 100; // Update HP bar berdasarkan HP saat ini
    }
}
