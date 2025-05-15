using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPUI_Manager : MonoBehaviour
{
    public PlayerData playerData; // Referensi ke PlayerData
    public Image hpBar; // Referensi ke UI HP bar

    void Start()
    {
        StartCoroutine(RegenHP());
    }

    void Update()
    {
        hpBar.fillAmount = playerData.currentHP / playerData.maxHP; // Update HP bar berdasarkan HP saat ini
    }

    IEnumerator RegenHP()
    {
        while (true)
        {
            if (playerData.currentHP < playerData.maxHP)
            {
                playerData.currentHP += playerData.regen;
                if (playerData.currentHP > playerData.maxHP)
                    playerData.currentHP = playerData.maxHP;
            }
            yield return new WaitForSeconds(1f);
        }
    }
}
