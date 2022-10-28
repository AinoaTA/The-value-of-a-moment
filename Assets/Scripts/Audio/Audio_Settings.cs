using UnityEngine;

public class Audio_Settings : MonoBehaviour
{
    FMOD.Studio.EventInstance SFXVolumeTestEvent;
    FMOD.Studio.EventInstance DialogueVolumeTestEvent;

    FMOD.Studio.Bus Music;
    FMOD.Studio.Bus SFX;
    FMOD.Studio.Bus Dialogue;
    FMOD.Studio.Bus Master;
    float MusicVolume = 0.8f;
    float DialogueVolume = 0.8f;
    float SFXVolume = 0.8f;
    float MasterVolume = 1f;

    public void Awake()
    {
        Music = FMODUnity.RuntimeManager.GetBus("bus:/Master/Music");
        Dialogue = FMODUnity.RuntimeManager.GetBus("bus:/Master/Dialogue");
        SFX = FMODUnity.RuntimeManager.GetBus("bus:/Master/SFX");
        Master = FMODUnity.RuntimeManager.GetBus("bus:/Master");
        SFXVolumeTestEvent = FMODUnity.RuntimeManager.CreateInstance("event:/SFX_LevelTest");
        DialogueVolumeTestEvent = FMODUnity.RuntimeManager.CreateInstance("event:/Dialogue_LevelTest");
    }

    public void Update()
    {
        Music.setVolume(MusicVolume);
        Dialogue.setVolume(DialogueVolume);
        SFX.setVolume(SFXVolume);
        Master.setVolume(MasterVolume);
    }

    public void MasterVolumeLevel(float newMasterVolume)
    {
        MasterVolume = newMasterVolume;
    }

    public void MusicVolumeLevel(float newMusicVolume)
    {
        MusicVolume = newMusicVolume;
    }

    public void DialogueVolumeLevel(float newDialogueVolume)
    {
        DialogueVolume = newDialogueVolume;

        FMOD.Studio.PLAYBACK_STATE PbState;
        DialogueVolumeTestEvent.getPlaybackState(out PbState);
        if (PbState != FMOD.Studio.PLAYBACK_STATE.PLAYING)
        {
            DialogueVolumeTestEvent.start();
        }
    }

    public void SFXVolumeLevel(float newSFXVolume)
    {
        SFXVolume = newSFXVolume;

        FMOD.Studio.PLAYBACK_STATE PbState;
        SFXVolumeTestEvent.getPlaybackState(out PbState);
        if (PbState != FMOD.Studio.PLAYBACK_STATE.PLAYING)
        {
            SFXVolumeTestEvent.start();
        }
    }

    public void MuteToggle(bool muted)
    {
        if (muted)
        {
            MasterVolume = 0f;
        }

        else
        {
            MasterVolume = 1f;
        }
    }

    /*public void ActualizarMasterSlide ()
    {
        if ()
        {

        }
    }*/

}
