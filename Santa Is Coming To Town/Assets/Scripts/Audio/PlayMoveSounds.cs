using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMoveSounds : MonoBehaviour
{
    [SerializeField] private AudioSource aS;
    [SerializeField] private AudioClip walk;
    [SerializeField] private AudioClip run;

    public void WalkSound()
    {
        aS.clip = walk;
        aS.Play();
    }

    public void RunSound()
    {
        aS.clip =run;
        aS.Play();
    }
}
