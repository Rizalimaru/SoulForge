using UnityEngine;
using System.Collections.Generic;

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
    }
}
