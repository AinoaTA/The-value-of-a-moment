using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

namespace Menu
{
    public class OptionsController : MonoBehaviour
    {
        [SerializeField] private AudioMixer TheMixer;
        [SerializeField] private OptionsData optionsData;
        [SerializeField] private Slider musicSlider, sfxSlider, voiceSlider;

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
            optionsData.sfx = sfxSlider.value;
        }

        public void SetVoiceVolume()
        {
            TheMixer.SetFloat("VoiceVol", Mathf.Log10(voiceSlider.value) * 20);
            optionsData.voices = voiceSlider.value;
        }
    }
}
