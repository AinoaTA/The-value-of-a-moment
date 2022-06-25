using System.Collections;
using UnityEngine;

public class Tira : MonoBehaviour
{
    [SerializeField] bool played;
    [SerializeField] AudioClip audioToPlay;
    [SerializeField] AudioSource Source;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ComicAudio"))
        {
            if (Source.clip != audioToPlay)
            {
                Source.clip = audioToPlay;
                Source.Play();
            }
        }   
    }

    public void Click()
    {
      
    }
    public void UnClick()
    {
       

    }


}
