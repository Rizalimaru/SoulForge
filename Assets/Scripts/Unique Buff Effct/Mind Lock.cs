using System.Collections;
using UnityEngine;

public class MindLock : MonoBehaviour
{
    public PlayerController playerController;
    public PlayerData playerData;
    public float bonusPercent = 15f; // 15% attack speed
    public float idleTimeRequired = 2f;

    private float idleTimer = 0f;
    private bool bonusActive = false;
    private float originalAttackSpeed;

    void Start()
    {
        originalAttackSpeed = playerData.attackSpeed;
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
        playerData.attackSpeed -= playerData.baseAttackSpeed * (bonusPercent / 100f);
        if (playerData.attackSpeed < 0.05f) playerData.attackSpeed = 0.05f;
    }

    void DeactivateBonus()
    {
        bonusActive = false;
        playerData.attackSpeed = originalAttackSpeed;
    }
}