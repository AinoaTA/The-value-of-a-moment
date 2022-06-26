using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Tira : MonoBehaviour
{
    [SerializeField] bool played;
    [SerializeField] AudioClip audioToPlay;
    [SerializeField] AudioSource Source;
    [SerializeField] bool finalRecord;


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

                if (finalRecord)
                {
                    StartCoroutine(FinalRecord());
                    GetComponent<BoxCollider>().enabled = false;
                }
            }
        }   
    }

    IEnumerator FinalRecord()
    {
        ScrollRect a = FindObjectOfType<ScrollRect>();
        yield return null;
        a.enabled = false;
        yield return null;
        buttonEnd?.Invoke();
        //yield return new WaitUntil(() => !Source.isPlaying);
        //GameManager.GetManager().sceneLoader.LoadWithLoadingScene(1, false);

    }
}
