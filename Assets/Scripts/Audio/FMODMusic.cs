using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FMODMusic : MonoBehaviour
{
    //remains consitent amongst all copies of themselves
    private static FMOD.Studio.EventInstance Music;
    void Start()
    {
        Music = FMODUnity.RuntimeManager.CreateInstance("event:/Music/Gameplay");
        Music.start();
        Music.release();
    }

    void Update()
    {
        
    }

    public void Mood (float MoodLevel)
    {
        Music.setParameterByName("Mood", MoodLevel);
    }

    public void Headphones(float HeadphoneMode)
    {
        Music.setParameterByName("Headphones", HeadphoneMode);
    }

    private void OnDestroy()
    {
        Music.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }

}
