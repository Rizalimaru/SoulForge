using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEnter : StateMachineBehaviour
{
    [SerializeField] private SoundType sound;
    [SerializeField, Range(0,1)] private float volume = 1f;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        SoundManager.PlaySound(sound, volume);
    }
}
