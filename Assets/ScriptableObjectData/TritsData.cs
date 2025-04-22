using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Trits Data", menuName = "ScriptableObject/Trits Data")]
public class TritsData : ScriptableObject
{
    public float Extraversion;
    public float Agreeableness;
    public float Conscientiousness;
    public float Neuroticism;
    public float Openness;
}

