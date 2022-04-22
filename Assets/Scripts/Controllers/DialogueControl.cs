using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class DialogueControl : MonoBehaviour
{
    private TMP_Text m_Text;
    private string m_CurrText;
    private float m_TimerShowDialogue =0;
    private float m_MaxTimeShowDialogue=10; //dependiendo de la longitud de la frase.
    private bool m_DialogueActive;

    private AudioSource m_AudioSource;

    public delegate void SoundDelegate();
    public static SoundDelegate soundSFX;

    [SerializeField]private List<Interactables> m_ListInteract = new List<Interactables>();
    
    private float m_Timer=-100;

    private void SetTimer()
    {
        m_Timer = 0;
        m_TimerShowDialogue = 0;
    }

    void Awake()
    {
        m_Text = GetComponent<TMP_Text>();
        m_AudioSource = GetComponentInChildren<AudioSource>();
    }

    void Start()
    {
        GameManager.GetManager().Dialogue = this;

        m_ListInteract.Add(GameManager.GetManager().Bed);
        m_ListInteract.Add(GameManager.GetManager().Book);
        m_ListInteract.Add(GameManager.GetManager().VR);
        m_ListInteract.Add(GameManager.GetManager().Window);
    }

    private void Update()
    {
        if (m_DialogueActive)
        {
            //m_TimerShowDialogue += Time.deltaTime;
            if (!m_AudioSource.isPlaying && m_AudioSource.clip!=null)
            {
                m_DialogueActive = false;
                m_Text.text = "";
               // m_TimerShowDialogue = 0;
            }else
                m_Text.text = m_CurrText;
        }
        m_Timer += Time.deltaTime;
        //if (m_Timer > 12 && !m_DialogueActive && GameManager.GetManager().m_CurrentStateGame==GameManager.StateGame.GamePlay)
        //    HelpDialogue();
    }


    /// <summary>
    /// If it doesnt has voiceOff var.
    /// </summary>
    /// <param name="dialogue"></param>
    /// <param name="voice"></param>
    public void SetDialogue(string dialogue, AudioClip voice=null)
    {
        StopDialogue();
        SetTimer();
        m_DialogueActive = true;
        m_CurrText = "[Elle] " + dialogue;
        m_AudioSource.clip = voice;

        soundSFX?.Invoke();

        if (m_AudioSource.clip != null)
            m_AudioSource.Play();
    }

    /// <summary>
    /// If it has VoiceOff var.
    /// </summary>
    /// <param name="voiceOff"></param>
    public void SetDialogue(VoiceOff voiceOff)
    {
        StopDialogue();
        SetTimer();
        m_DialogueActive = true;
        m_CurrText = voiceOff.text;
        m_AudioSource.clip = voiceOff.voice;

        if (m_AudioSource.clip != null)
            m_AudioSource.Play();
    }

    public void StopDialogue()
    {
        SetTimer();
        m_DialogueActive = false;
        m_Text.text = "";
        m_AudioSource.clip = null;
    }

    private void HelpDialogue()
    {
        for (int i = 0; i < m_ListInteract.Count; i++)
        {
            Interactables l_interactable = m_ListInteract[i].GetComponent<Interactables>();
            
            if (!l_interactable.GetDone() && l_interactable.GetPhrasesVoiceOff().Length !=0)
            {
                int random = Random.Range(0, l_interactable.GetPhrasesVoiceOff().Length-1);

                VoiceOff l_VoiceOff = l_interactable.GetPhrasesVoiceOff()[random];
               
                SetDialogue(l_VoiceOff);
            }
        }
    }

    public bool CheckDialogueIsPlaying()
    {
        return m_AudioSource.isPlaying;
    }
}
