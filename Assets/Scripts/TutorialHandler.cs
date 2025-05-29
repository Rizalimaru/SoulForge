using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialHandler : MonoBehaviour
{
    public PlayerData playerData; // Reference to the PlayerData scriptable object
    public GameObject tutorialPanel; // Reference to the tutorial panel UI
    public GameObject[] langkahTutorial; // Array of tutorial steps



    public IEnumerator mulaiTutorial()
    {   

        if(playerData.isTutorialCompleted)
        {
            yield break; // Exit if the tutorial is already done
        }
        tutorialPanel.SetActive(true); // Show the tutorial panel

        foreach (GameObject langkah in langkahTutorial)
        {
            langkah.SetActive(true); // Activate each tutorial step
            yield return new WaitForSeconds(5f); // Wait for 2 seconds before showing the next step
            langkah.SetActive(false); // Deactivate the current step
        }

        tutorialPanel.SetActive(false); // Hide the tutorial panel after all steps are shown
    }

}
