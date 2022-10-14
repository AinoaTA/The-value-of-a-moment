using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Tira : MonoBehaviour
{
    [SerializeField] bool played;
    [SerializeField] AudioClip audioToPlay;
    [SerializeField] AudioSource Source;
    [SerializeField] bool finalRecord;

    bool block;
    public delegate void EndButton();
    public static EndButton buttonEnd;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ComicAudio"))
        {
            if (Source.clip != audioToPlay)
            {
                Source.Stop();
                Source.clip = audioToPlay;
                Source.Play();

                if (finalRecord && !block)
                {
                    block = true;
                    StartCoroutine(FinalRecord());
                }
            }
        }
    }

    IEnumerator FinalRecord()
    {
        ScrollRect a = FindObjectOfType<ScrollRect>();
        yield return null;
        buttonEnd?.Invoke();
    }
}
