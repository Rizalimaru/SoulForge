using UnityEngine;

[CreateAssetMenu(fileName = "NewBuffPair", menuName = "Buff System/Buff Pair Data")]
public class BuffPairData : ScriptableObject
{
    public BuffCardData buffA;
    public BuffCardData buffB;
}

[System.Serializable]
public class BuffCardData
{
    public string buffName;
    [TextArea(2, 5)]
    public string description; // Dilema/narasi
    public Sprite icon;
    [TextArea(2, 5)]
    public string statSummary; // Ringkasan stat yang diubah (untuk UI)
    public BuffEffect[] effects; // List efek stat
    public TraitEffect[] traitEffects; // List efek trait
}

[System.Serializable]
public class BuffEffect
{
    public BuffStatType statType;
    public float value;
}

public enum BuffStatType
{
    Attack,
    Defense,
    AttackSpeed,
    Regen,
    HP,
    PickRadius,
    // Tambahkan stat lain jika perlu
}

[System.Serializable]
public class TraitEffect
{
    public TraitType traitType;
    public int value;
}

public enum TraitType
{
    Extraversion,
    Conscientiousness,
    Agreeableness,
    Neuroticism,
    Openness
}