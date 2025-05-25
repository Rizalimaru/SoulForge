using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrimatyWeaponChoseManager : MonoBehaviour
{
    public GameObject primaryWeaponSelectionUI;
    public GameObject sword;
    public GameObject staff;


    void Start()
    {
        primaryWeaponSelectionUI.SetActive(true);
        Time.timeScale = 0; // Pause the game
    }

    public void SelectSword()
    {
        primaryWeaponSelectionUI.SetActive(false);
        sword.SetActive(true);
        staff.SetActive(false);
        Time.timeScale = 1; // Resume the game
    }

    public void SelectStaff()
    {
        primaryWeaponSelectionUI.SetActive(false);
        sword.SetActive(false);
        staff.SetActive(true);
        Time.timeScale = 1; // Resume the game
    }
}
