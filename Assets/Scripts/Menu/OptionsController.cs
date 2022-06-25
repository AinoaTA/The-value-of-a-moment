using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class OptionsController : MonoBehaviour
{
    public AudioMixer TheMixer;
    public OptionsData optionsData;
    public Slider musicSlider, sfxSlider, voiceSlider;

    private void Start()
    {
        musicSlider.value = optionsData.music;
        sfxSlider.value = optionsData.sfx;
        voiceSlider.value = optionsData.voices;

        SetMusicVolume();
        SetSFXVolume();
        SetMasterVolume();
        SetVoiceVolume();
    }
    public void SetMasterVolume()
    {
        TheMixer.SetFloat("MasterVol", 0);
    }
    public void SetMusicVolume()
    {
        TheMixer.SetFloat("MusicVol", Mathf.Log10(musicSlider.value) * 20);
     
        optionsData.music = musicSlider.value;
    }

    public void SetSFXVolume()
    {
        TheMixer.SetFloat("SFXVol", Mathf.Log10(sfxSlider.value) * 20);
        optionsData.sfx =sfxSlider.value;
    }

    public void SetVoiceVolume()
    {
        TheMixer.SetFloat("VoiceVol", Mathf.Log10(voiceSlider.value) * 20);
        optionsData.voices = voiceSlider.value;
    }
}
