using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FMODMusic : MonoBehaviour
{
    private static FMOD.Studio.EventInstance Music;
    void Start()
    {
        Music = FMODUnity.RuntimeManager.CreateInstance("event:/Music/Gameplay");
        Music.start();
        Music.release();
    }

    public void Mood (float MoodLevel)
    {
        Music.setParameterByName("Mood", MoodLevel);
    }

    public void Location (float Loc)
    {
        Music.setParameterByName("Location", Loc);
    }

    public void Yoga (float YogaValue)
    {
        Music.setParameterByName("Yoga", YogaValue);
    }

    public void Drums (float DrumsValue)
    {
        Music.setParameterByName("Drums", DrumsValue);
    }

    public void Headphones(float HeadphoneMode)
    {
        Music.setParameterByName("Headphones", HeadphoneMode);
    }
    public void Pause(float PauseMode)
    {
        Music.setParameterByName("Pause", PauseMode);
    }

    private void OnDestroy()
    {
        Music.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }

}
