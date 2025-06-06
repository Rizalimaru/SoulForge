using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrimatyWeaponChoseManager : MonoBehaviour
{
    public GameObject primaryWeaponSelectionUI;
    public GameObject sword;
    public GameObject staff;
    public PauseUiManager pauseUiManager; // Optional: if you want to control pause state
    public TutorialHandler tutorialHandler; // Optional: if you want to control tutorial state


    void Start()
    {
        StartSelection();
    }

    public void StartSelection()
    {
        primaryWeaponSelectionUI.SetActive(true);
        Time.timeScale = 0; // Pause the game
        pauseUiManager.canPause = false; // Disable pause functionality if needed
    }

    public void SelectSword()
    {
        primaryWeaponSelectionUI.SetActive(false);
        sword.SetActive(true);
        staff.SetActive(false);
        Time.timeScale = 1; // Resume the game
        pauseUiManager.canPause = true; // Re-enable pause functionality if needed
        if (tutorialHandler != null)
        {
            StartCoroutine(tutorialHandler.mulaiTutorial()); // Start the tutorial if applicable
        }
    }

    public void SelectStaff()
    {
        primaryWeaponSelectionUI.SetActive(false);
        sword.SetActive(false);
        staff.SetActive(true);
        Time.timeScale = 1; // Resume the game
        pauseUiManager.canPause = true; // Re-enable pause functionality if needed
        if (tutorialHandler != null)
        {
            StartCoroutine(tutorialHandler.mulaiTutorial()); // Start the tutorial if applicable
        }
    }
}
