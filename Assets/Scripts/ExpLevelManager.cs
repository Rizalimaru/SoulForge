using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExpLevelManager : MonoBehaviour
{
    public Image expBar;
    public PlayerData playerData;
    public GameObject UIBuffSelection;
    public BuffSelectionUI buffSelectionUI;

    void Start()
    {
        playerData.level = 1;
        playerData.maxXP = 100 * (playerData.level * playerData.level);
        playerData.currnetXP = 0;
    }
    void Update()
    {   
        
        if(Input.GetKeyDown(KeyCode.K))
        {
            playerData.currnetXP += playerData.maxXP; // Tambah XP untuk testing
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerData.currnetXP += 10;
        }
        if(playerData.currnetXP >= playerData.maxXP)
        {
            LevelUp();
        }
        expBar.fillAmount = playerData.currnetXP / playerData.maxXP;
    }

    public void LevelUp()
    {   
        expBar.fillAmount = 0;
        playerData.currnetXP = 0;
        playerData.level += 1;
        playerData.maxXP = 100 * (playerData.level * playerData.level);
        Time.timeScale = 0;
        buffSelectionUI.DisplayRandomBuffPair(); // Tampilkan buff baru
        UIBuffSelection.SetActive(true);

    }


}
