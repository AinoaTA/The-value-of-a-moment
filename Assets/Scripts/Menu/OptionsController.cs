using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Audio;

public class OptionsController : MonoBehaviour
{
    public AudioMixer TheMixer;
    public Slider musicSlider, sfxSlider;
    public TMP_Text musicLabel, sfxLabel;

    private void Start()
    {
        SetMusicVolume();
        SetSFXVolume();
        SetMasterVolume();
    }
    public void SetMasterVolume()
    {
        TheMixer.SetFloat("MasterVol", 0);
    }
    public void SetMusicVolume()
    {
        musicLabel.text = (musicSlider.value + 25).ToString();
        TheMixer.SetFloat("MusicVol", musicSlider.value);
    }

    public void SetSFXVolume()
    {
        sfxLabel.text = (sfxSlider.value + 25).ToString();
        TheMixer.SetFloat("SFXVol", (sfxSlider.value));
    }
}
