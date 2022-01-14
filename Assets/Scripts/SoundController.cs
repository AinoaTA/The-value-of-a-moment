using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    private AudioSource m_AudioSource;
    
    public AudioClip m_Message;


    private void Awake()
    {
        m_AudioSource = GetComponent<AudioSource>();
    }


    private void OnEnable()
    {
        NotificationController.m_MessageDelegate += StartMessage;
    }

    private void OnDisable()
    {
        NotificationController.m_MessageDelegate -= StartMessage;
    }

    public void StartMessage()
    {
        m_AudioSource.PlayOneShot(m_Message);
    }
}

