using System.Collections;
using FMODUnity;
using UnityEngine;
using UnityEngine.UI;

public class Tira : MonoBehaviour
{
    [SerializeField] bool played;
    //[SerializeField] string nameAudio;
    [SerializeField] bool finalRecord;

    public string soundEvent = null;

    bool block;
    public delegate void EndButton();
    public static EndButton buttonEnd;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ComicAudio"))
        {
            GameManager.GetManager().dialogueManager.SetDialogue(soundEvent, canRepeat: true, onlyVoice:true);
            
                if (finalRecord && !block)
                {
                    block = true;
                    StartCoroutine(FinalRecord());
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
