using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Trits Data", menuName = "ScriptableObject/Trits Data")]
public class TritsData : ScriptableObject
{
    public int Extraversion;
    public int Agreeableness;
    public int Conscientiousness;
    public int Neuroticism;
    public int Openness;
}

