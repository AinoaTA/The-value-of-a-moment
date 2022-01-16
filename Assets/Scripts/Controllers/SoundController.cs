using UnityEngine;

public class SoundController : MonoBehaviour
{
    private AudioSource m_AudioSource;
    
    public AudioClip m_Message;
    public AudioClip m_Alarm;
    public AudioClip m_Book;

    private void Awake()
    {
        m_AudioSource = GetComponent<AudioSource>();
        GameManager.GetManager().SetSoundController(this);
    }


    private void OnEnable()
    {
        NotificationController.m_MessageDelegate += StartMessage;
        Alarm.m_DelegateSFX += StartAlarm;
        Book.m_DelegateSFXBook += StartBook; 
    }

    private void OnDisable()
    {
        NotificationController.m_MessageDelegate -= StartMessage;
        Alarm.m_DelegateSFX -= StartAlarm;
        Book.m_DelegateSFXBook -= StartBook;
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
        m_AudioSource.Stop();
    }


}

