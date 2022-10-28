using FMODUnity;
using UnityEngine;

public class FMOD_OneShot : MonoBehaviour
{
    public string soundEvent = null;

    public void PlaySoundEvent()
    {
        if (soundEvent != null)
        {
            RuntimeManager.PlayOneShot(soundEvent);
        }
    }
}
