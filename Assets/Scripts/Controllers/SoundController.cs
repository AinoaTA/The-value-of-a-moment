using System.Collections;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    public AudioSource m_ExtraSFX1;
    public AudioSource m_ExtraSFX;

    public AudioSource introLoop, loop;
    public AudioClip m_Message;
    public AudioClip m_Alarm;
    public AudioClip m_Book;
    public AudioClip dialogueBlip;
    private bool Active;

    public AudioClip[] introMusic;
    public AudioClip[] loopMusic;
    public float[] timingsIntroLoop;
    private int currIndex;

    void Start()
    {
        GameManager.GetManager().SoundController = this;
        introLoop.volume = 0;
    }

    private void OnEnable()
    {
        NotificationController.m_MessageDelegate += StartMessage;
        Alarm.m_DelegateSFX += StartAlarm;
        Book.m_DelegateSFXBook += StartBook;
        DialogueControl.soundSFX += DialogueSound;
    }

    private void OnDisable()
    {
        NotificationController.m_MessageDelegate -= StartMessage;
        Alarm.m_DelegateSFX -= StartAlarm;
        Book.m_DelegateSFXBook -= StartBook;
        DialogueControl.soundSFX -= DialogueSound;
    }

    public void StartMessage()
    {
        m_ExtraSFX1.PlayOneShot(m_Message);
    }

    public void StartAlarm()
    {
        m_ExtraSFX1.PlayOneShot(m_Alarm);
    }

    public void StartBook()
    {
        m_ExtraSFX1.PlayOneShot(m_Book);
    }
    public void StopSound()
    {
        m_ExtraSFX1.Stop();
    }
    public void SetMusic()
    {
        StartCoroutine(IcreaseAudioCo(0));
    }

    public void QuitMusic()
    {
        StartCoroutine(DecreaseAudioCo(introLoop));
    }

    public void DialogueSound()
    {
        m_ExtraSFX.PlayOneShot(dialogueBlip);
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
       
           currIndex = index;
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
            StartCoroutine(DecreaseAudioCo(loop));
            yield return new WaitUntil(() => !loop.isPlaying);
            StartCoroutine(IcreaseAudioCo(index));
        }
    }
}

