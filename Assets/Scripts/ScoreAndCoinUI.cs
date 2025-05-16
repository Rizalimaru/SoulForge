using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreAndCoinUI : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI coinText;
    public PlayerData playerData;


    void Start()
    {
        playerData.scoreInStage = 0;
        playerData.coinInStage = 0;
    }

    void Update()
    {
        scoreText.text = playerData.scoreInStage.ToString();
        coinText.text = playerData.coinInStage.ToString();
    }




}
