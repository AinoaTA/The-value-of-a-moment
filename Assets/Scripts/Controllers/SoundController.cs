using System.Collections;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    public AudioSource ExtraSFX1;
    public AudioSource ExtraSFX;

    public AudioSource introLoop, loop;
    public AudioClip Message;
    public AudioClip Alarm;
    public AudioClip Book;
    public AudioClip dialogueBlip;
    private bool Active;

    public AudioClip[] introMusic;
    public AudioClip[] loopMusic;
    private int currIndex = 0;

    void Awake()
    {
        GameManager.GetManager().SoundController = this;
        introLoop.volume = 0;
    }

    private void OnEnable()
    {
        NotificationController.MessageDelegate += StartMessage;
        Alarm.DelegateSFX += StartAlarm;
        Book.DelegateSFXBook += StartBook;
        DialogueControl.soundSFX += DialogueSound;
    }

    private void OnDisable()
    {
        NotificationController.MessageDelegate -= StartMessage;
        Alarm.DelegateSFX -= StartAlarm;
        Book.DelegateSFXBook -= StartBook;
        DialogueControl.soundSFX -= DialogueSound;
    }

    public void StartMessage()
    {
        ExtraSFX1.PlayOneShot(Message);
    }

    public void StartAlarm()
    {
        ExtraSFX1.PlayOneShot(Alarm);
    }

    public void StartBook()
    {
        ExtraSFX1.PlayOneShot(Book);
    }
    public void StopSound()
    {
        ExtraSFX1.Stop();
    }
    public void SetMusic()
    {
        StartCoroutine(SetMusicDelay());
        
    }

    IEnumerator SetMusicDelay()
    {
        yield return null;
        if (currIndex > 0)
        {
            currIndex = 5;
            GameManager.GetManager().Autocontrol.UpdateAutcontrol();
        }
        else
            StartCoroutine(StartSaddest());
    }

    public void QuitMusic(AudioSource source)
    {
        StartCoroutine(DecreaseAudioCo(source));
    }
    public void QuitAllMusic()
    {
        StopAllCoroutines();
        
        StartCoroutine(DecreaseAudioCo(introLoop));
        StartCoroutine(DecreaseAudioCo(loop));
    }

    public void DialogueSound()
    {
        ExtraSFX.PlayOneShot(dialogueBlip);
    }

    private IEnumerator DecreaseAudioCo(AudioSource source)
    {
        float counter = 0f;
        while (counter < 0.5)
        {
            counter += Time.deltaTime;
            source.volume = Mathf.Lerp(1f, 0f, counter / 0.5f);
            yield return null;
        }
        source.Stop();
    }
    private IEnumerator IcreaseAudioCo(int index)
    { 
        // introLoop.gameObject.SetActive(true);
        float counter = 0f;
        introLoop.clip = introMusic[index];
        loop.clip = loopMusic[index];

        introLoop.Play();
        while (counter < 4f)
        {
            counter += Time.deltaTime;
            introLoop.volume = Mathf.Lerp(0f, 1f, counter / 4);
            yield return null;
        }
        yield return new WaitUntil(() => !introLoop.isPlaying);
        loop.volume = 1;
        loop.Play();
    }

    public void ChangeMusicMood(int index)
    {
        StartCoroutine(ChangeMusic(index));
    }
    private IEnumerator ChangeMusic(int index)
    {
        if (currIndex != index)
        {
            currIndex = index;
            StartCoroutine(DecreaseAudioCo(loop));
            yield return new WaitUntil(() => !loop.isPlaying);
            if (index == 0)
                StartCoroutine(StartSaddest());
            else
            {
                StartCoroutine(IcreaseAudioCo(index));
            }
        }
    }

    IEnumerator StartSaddest()
    {
        currIndex = 0;
        float counter = 0;
        loop.volume = 0;
        loop.clip = loopMusic[0];
        loop.Play();
        while (counter < 3)
        {
            counter += Time.deltaTime;
            loop.volume = Mathf.Lerp(0f, 1f, counter / 3);
            yield return null;
        }
    }
}

