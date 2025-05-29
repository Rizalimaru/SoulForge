using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings : MonoBehaviour
{
    [Header("Graphics Settings")]
    [Tooltip("Set target frame rate. 0 = unlimited.")]
    public int targetFPS = 60;

    void Awake()
    {
        Application.targetFrameRate = targetFPS;
        QualitySettings.vSyncCount = 0; // Pastikan vSync dimatikan agar limit FPS efektif
    }
}
