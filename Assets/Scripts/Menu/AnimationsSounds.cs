using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationsSounds : MonoBehaviour
{
    private AudioSource Source;
    public void Awake()
    {
        Source=GetComponentInChildren<AudioSource>();
    }

    public void OpenDoor()
    {
        Source.Play();
    }
}
