using UnityEngine;
using System.Collections.Generic;
using System.IO;

[CreateAssetMenu(fileName = "GameSessionResult", menuName = "Game/Session Result")]
public class GameSessionResult : ScriptableObject
{
    [System.Serializable]
    public class SessionRecord
    {
        public float timeElapsed;
        public int score;
        public int extraversion;
        public int openness;
        public int conscientiousness;
        public int agreeableness;
        public int neuroticism;
    }

    [Header("Riwayat Semua Session")]
    public List<SessionRecord> sessionRecords = new List<SessionRecord>();

    private static string SavePath => Path.Combine(Application.persistentDataPath, "session_records.json");

    [System.Serializable]
    private class SessionRecordListWrapper
    {
        public List<SessionRecord> sessionRecords;
    }

    public void SaveResult(float time, int score, TritsData traits)
    {
        SessionRecord record = new SessionRecord
        {
            timeElapsed = time,
            score = score,
            extraversion = traits.Extraversion,
            openness = traits.Openness,
            conscientiousness = traits.Conscientiousness,
            agreeableness = traits.Agreeableness,
            neuroticism = traits.Neuroticism
        };
        sessionRecords.Add(record);
        SaveToJson();
    }

    public void SaveToJson()
    {
        SessionRecordListWrapper wrapper = new SessionRecordListWrapper { sessionRecords = sessionRecords };
        string json = JsonUtility.ToJson(wrapper, true);
        File.WriteAllText(SavePath, json);
    }

    public void LoadFromJson()
    {
        if (File.Exists(SavePath))
        {
            string json = File.ReadAllText(SavePath);
            SessionRecordListWrapper wrapper = JsonUtility.FromJson<SessionRecordListWrapper>(json);
            if (wrapper != null && wrapper.sessionRecords != null)
                sessionRecords = wrapper.sessionRecords;
        }
    }
}
