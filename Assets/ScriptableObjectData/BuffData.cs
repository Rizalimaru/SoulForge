using UnityEngine;

[CreateAssetMenu(fileName = "NewBuff", menuName = "Buff System/Buff Data")]
public class BuffData : ScriptableObject
{
    public string buffName;
    public string description;
    public Sprite icon;
    public BuffType buffType;
    public int value;
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