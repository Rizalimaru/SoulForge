using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeElapse : MonoBehaviour
{
    public TextMeshProUGUI timeText;
    public float timeElapsed;

    void Update()
    {
        timeElapsed += Time.deltaTime;
        int minutes = Mathf.FloorToInt(timeElapsed / 60F);
        int seconds = Mathf.FloorToInt(timeElapsed - minutes * 60);

        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
