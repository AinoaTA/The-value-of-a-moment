using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class DialogueControl : MonoBehaviour
{
    private TMP_Text Text;
    private string CurrText;
    private float TimerShowDialogue =0;
    private float MaxTimeShowDialogue=10; //dependiendo de la longitud de la frase.
    private bool DialogueActive;

    private AudioSource AudioSource;

    public delegate void SoundDelegate();
    public static SoundDelegate soundSFX;

    [SerializeField]private List<Interactables> ListInteract = new List<Interactables>();
    
    private float Timer=-100;

    private void SetTimer()
    {
        Timer = 0;
        TimerShowDialogue = 0;
    }

    void Awake()
    {
        Text = GetComponent<TMP_Text>();
        AudioSource = GetComponentInChildren<AudioSource>();
    }

    void Start()
    {
        //GameManager.GetManager().Dialogue = this;

        //ListInteract.Add(GameManager.GetManager().Bed);
        //ListInteract.Add(GameManager.GetManager().Book);
        //ListInteract.Add(GameManager.GetManager().VR);
        //ListInteract.Add(GameManager.GetManager().Window);
    }

    private void Update()
    {
        if (DialogueActive)
        {
            //TimerShowDialogue += Time.deltaTime;
            if (!AudioSource.isPlaying && AudioSource.clip!=null)
            {
                DialogueActive = false;
                Text.text = "";
               // TimerShowDialogue = 0;
            }else
                Text.text = CurrText;
        }
        Timer += Time.deltaTime;
        //if (Timer > 12 && !DialogueActive && GameManager.GetManager().CurrentStateGame==GameManager.StateGame.GamePlay)
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
        DialogueActive = true;
        CurrText = "(Elle) " + dialogue;
        AudioSource.clip = voice;

        soundSFX?.Invoke();

        if (AudioSource.clip != null)
            AudioSource.Play();
    }

    /// <summary>
    /// If it has VoiceOff var.
    /// </summary>
    /// <param name="voiceOff"></param>
    public void SetDialogue(VoiceOff voiceOff)
    {
        StopDialogue();
        SetTimer();
        DialogueActive = true;
        CurrText = voiceOff.text;
        AudioSource.clip = voiceOff.voice;

        if (AudioSource.clip != null)
            AudioSource.Play();
    }

    public void StopDialogue()
    {
        SetTimer();
        StopAllCoroutines();
        DialogueActive = false;
        Text.text = "";
        AudioSource.clip = null;
    }

    private void HelpDialogue()
    {
        for (int i = 0; i < ListInteract.Count; i++)
        {
            Interactables l_interactable = ListInteract[i].GetComponent<Interactables>();

            //if (!l_interactable.GetDone() && l_interactable.GetPhrasesVoiceOff().Length != 0)
            //{
            //    int random = Random.Range(0, l_interactable.GetPhrasesVoiceOff().Length - 1);
            //    VoiceOff l_VoiceOff = l_interactable.GetPhrasesVoiceOff()[random];
            //    SetDialogue(l_VoiceOff);
            //}
        }
    }

    public bool CheckDialogueIsPlaying()
    {
        return AudioSource.isPlaying;
    }
}
