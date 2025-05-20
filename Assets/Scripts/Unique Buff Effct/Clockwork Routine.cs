using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockworkRoutine : MonoBehaviour
{
    public PlayerController playerController;
    public PlayerData playerData;
    public float bonusPercent = 10f; // 15% attack speed
    public float idleTimeRequired = 3f;

    private float idleTimer = 0f;
    private bool bonusActive = false;
    private float originalAttack;

    void Start()
    {
        originalAttack = playerData.attackDamage;
    }

    void Update()
    {
        if (playerController.movementInput.magnitude == 0)
        {
            idleTimer += Time.deltaTime;
            if (!bonusActive && idleTimer >= idleTimeRequired)
            {
                ActivateBonus();
            }
        }
        else
        {
            idleTimer = 0f;
            if (bonusActive)
            {
                DeactivateBonus();
            }
        }
    }

    void ActivateBonus()
    {
        bonusActive = true;
        playerData.attackDamage += Mathf.RoundToInt(playerData.baseAttackDamage * bonusPercent / 100f);
        if (playerData.attackDamage < 0.05f) playerData.attackDamage = 0.05f;
    }

    void DeactivateBonus()
    {
        bonusActive = false;
        playerData.attackDamage = originalAttack;
    }
}
