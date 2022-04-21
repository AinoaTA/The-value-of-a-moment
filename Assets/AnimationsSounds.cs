using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationsSounds : MonoBehaviour
{
    private AudioSource Source;
    public void Start()
    {
        Source=GetComponentInChildren<AudioSource>();
        print(Source);
    }

    public void OpenDoor()
    {
        Source.Play();
    }
}
