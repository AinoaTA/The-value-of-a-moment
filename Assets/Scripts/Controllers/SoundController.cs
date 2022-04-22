using System.Collections;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    private AudioSource m_AudioSource;
    public AudioSource m_ExtraSFX; 

    public AudioSource m_GlobalSource;
    public AudioClip m_Message;
    public AudioClip m_Alarm;
    public AudioClip m_Book;
    public AudioClip dialogueBlip;
    private bool Active;

    private void Awake()
    {
        m_AudioSource = GetComponent<AudioSource>();    
    }
    void Start()
    {
        GameManager.GetManager().SoundController = this;
        m_GlobalSource.volume = 0;
        m_GlobalSource.gameObject.SetActive(false);
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
        m_AudioSource.PlayOneShot(m_Message);
    }

    public void StartAlarm()
    {
        m_AudioSource.PlayOneShot(m_Alarm);
    }

    public void StartBook()
    {
        m_AudioSource.PlayOneShot(m_Book);
    }
    public void StopSound()
    {
        m_AudioSource.Stop();// = null;
    }
    public void SetMusic()
    {
        StartCoroutine(IcreaseAudioCo());
    }

    public void QuitMusic()
    {
        StartCoroutine(DecreaseAudioCo());
    }

    public void DialogueSound()
    {
        m_ExtraSFX.PlayOneShot(dialogueBlip);
    }

    private IEnumerator DecreaseAudioCo()
    {
        float counter = 0f;
        while (counter < 0.5f)
        {
            counter += Time.deltaTime;

            m_GlobalSource.volume = Mathf.Lerp(1f, 0f, counter / 0.5f);

            yield return null;
        }
        m_GlobalSource.gameObject.SetActive(false);
    }
    private IEnumerator IcreaseAudioCo()
    {
        float counter = 0f;
        m_GlobalSource.gameObject.SetActive(true);

        m_GlobalSource.Play();
        while (counter < 5f)
        {
            counter += Time.deltaTime;

            m_GlobalSource.volume = Mathf.Lerp(0f, 1f, counter / 1.5f);

            yield return null;
        }
    }

}

