using UnityEngine;

[CreateAssetMenu(fileName = "NewBuff", menuName = "Buff System/Buff Data")]
public class BuffData : ScriptableObject
{
    public string buffName;
    public string description;
    public Sprite icon;
    public BuffType buffType;
    public int value;

    // Tambahan: efek trait
    public TraitEffect[] traitEffects;
}

public enum BuffType
{
    Attack,
    Defense,
    MoveSpeed,
    Regen,
    HP,
    pickRadius,
    AtkSpeed,
}

// Tambahkan struct dan enum trait seperti di BuffPairData
[System.Serializable]
public class TraitEffect1
{
    public TraitType1 traitType;
    public int value;
}

public enum TraitType1
{
    Extraversion,
    Conscientiousness,
    Agreeableness,
    Neuroticism,
    Openness
}