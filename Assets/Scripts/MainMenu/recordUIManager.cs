using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class recordUIManager : MonoBehaviour
{
    public GameSessionResult sessionResult; // Drag ScriptableObject ke sini
    public GameObject recordTextPrefab;     // Drag prefab TMP UI ke sini
    public Transform contentParent;         // Drag panel/empty GameObject (parent TMP) ke sini

    void Start()
    {
        ShowAllRecords();
    }

    public void ShowAllRecords()
    {
        // Hapus semua child lama
        foreach (Transform child in contentParent)
        {
            Destroy(child.gameObject);
        }

        // Tampilkan semua record
        foreach (var record in sessionResult.sessionRecords)
        {
            GameObject go = Instantiate(recordTextPrefab, contentParent);
            TextMeshProUGUI tmp = go.GetComponent<TextMeshProUGUI>();
            tmp.text = $"Score: {record.score} | Time: {record.timeElapsed:F1}s\n" +
                       $"Traits: E:{record.extraversion} O:{record.openness} C:{record.conscientiousness} A:{record.agreeableness} N:{record.neuroticism}";
        }
    }
}
