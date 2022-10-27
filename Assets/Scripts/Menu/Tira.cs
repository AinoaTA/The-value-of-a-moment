using System.Collections;
using FMODUnity;
using UnityEngine;
using UnityEngine.UI;

public class Tira : MonoBehaviour
{
    [SerializeField] bool played;
    //[SerializeField] string nameAudio;
    [SerializeField] bool finalRecord;
    public GameObject tutorial;
    public string soundEvent = null;

    bool block;
    public delegate void EndButton();
    public static EndButton buttonEnd;

    private IEnumerator Start()
    {
        if (tutorial == null) yield break;
        yield return new WaitForSeconds(2.5f);
        tutorial.gameObject.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ComicAudio"))
        {
            if (tutorial != null)
                tutorial.gameObject.SetActive(false);
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
