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
    public TritsData traitsData;


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
        traitsData.Extraversion += 2; traitsData.Neuroticism -= 1; traitsData.Conscientiousness += 1;
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
        traitsData.Openness += 2; traitsData.Neuroticism += 1; traitsData.Extraversion -= 1;
        Time.timeScale = 1; // Resume the game
        pauseUiManager.canPause = true; // Re-enable pause functionality if needed
        if (tutorialHandler != null)
        {
            StartCoroutine(tutorialHandler.mulaiTutorial()); // Start the tutorial if applicable
        }
    }

    // public void SelectSword()
    // {
    //     traitsData.Extraversion += 2; traitsData.Neuroticism -= 1;
    //     // Extraversion (+2): Pemain menunjukkan keberanian, inisiatif, dan gaya bermain aktif.
    //     // Neuroticism (-1): Pemain siap menghadapi tekanan atau bahaya secara langsung.
    // }
    // public void SelectStaff()
    // {
    //     traitsData.Openness += 2; traitsData.Conscientiousness += 1;
    //     // Openness (+2): Mewakili imajinasi, fleksibilitas, dan pendekatan alternatif dalam permainan.
    //     // Conscientiousness (+1): Pemain mempertimbangkan strategi dan kontrol situasi.
    // }
}
