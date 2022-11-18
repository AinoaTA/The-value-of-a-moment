using System;
using System.Collections;
using TMPro;
using UnityEngine;
using FMODUnity;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public DialoguesList dialogues;
    public TMP_Text subtitle;
    public float defaultvoiceTime = 2;
    public float aditionalVoiceTime = 0.2f;
    public Image panel;
    bool justVoice;
    DialogueJSON currentDialogue;
    int currentLine;

    IEnumerator nextLineCoroutine;
    private static FMOD.Studio.EventInstance eventAudio;
    private void Awake()
    {
        GameManager.GetManager().dialogueManager = this;
    }

    public void Start()
    {
        if (panel != null)
            panel.gameObject.SetActive(false);
        for (int i = 0; i < dialogues.dialogues.Count; i++)
        {
            for (int e = 0; e < dialogues.dialogues[i].lines.Count; e++)
            {
                dialogues.dialogues[i].lines[e].played = false;
            }
        }
    }
    Action saveAct;
    bool canRepeat;
    string previusDialogue;
    public bool waitDialogue = true;
    bool noInterrumpt;
    public void SetDialogue(string dialogue, Action act = null, bool forceInvoke = false, bool canRepeat = false, bool onlyVoice = false, bool canInterrumpt=true)
    {
        if (dialogue == previusDialogue) return;
        if (noInterrumpt) return;
        if (!canInterrumpt) 
        {
            noInterrumpt = true;
        }

        previusDialogue = dialogue;
        waitDialogue = true;
        if (nextLineCoroutine != null) StopDialogue();
        eventAudio.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        justVoice = onlyVoice;
        currentDialogue = dialogues.GetDialogue(dialogue);
        //this conver was played.
        saveAct = act;

        if (forceInvoke)
        {
            saveAct?.Invoke();
            return;
        }

        if (currentDialogue.lines[0].played)
        {
            saveAct = null;
            return;
        }
        this.canRepeat = canRepeat;
        currentLine = 0;
        ShowLine();
    }

    void ShowLine()
    {
        DialogueLineJSON line = currentDialogue.lines[currentLine];
        if (!justVoice)
            subtitle.enabled = true;

        bool langESP = LanguageGame.lang == LanguageGame.Languages.ESP;
        if (!justVoice)
            subtitle.text = langESP ? line.es : line.en;

        float waitTime = defaultvoiceTime;
        string path = "event:/Dialogue/" + LanguageGame.lang + "/" + line.ID;

        try
        {
            RuntimeManager.GetEventDescription(path).getLength(out int lenght);
            eventAudio = FMODUnity.RuntimeManager.CreateInstance(path); //FMODUnity.RuntimeManager.PlayOneShot(path);
            eventAudio.start();
            float time = (float)lenght / 1000;
            waitTime = time + aditionalVoiceTime;
        }
        catch (System.Exception e)
        {
            Debug.Log(e);
        }

        if (!canRepeat)
            line.played = true;
        StartCoroutine(nextLineCoroutine = NextLine(waitTime));
    }

    IEnumerator NextLine(float waitTime)
    {
        if (panel != null)
            panel.gameObject.SetActive(true);
        yield return new WaitForSeconds(waitTime);
        currentLine++;
        if (currentLine < currentDialogue.lines.Count) ShowLine();
        else EndDialog();
    }

    void EndDialog()
    {
        if (canRepeat)
        {
            for (int i = 0; i < currentDialogue.lines.Count; i++)
                currentDialogue.lines[i].played = false;
        }
        noInterrumpt = false;
        if (saveAct != null)
            saveAct?.Invoke();

        if (panel != null)
            panel.gameObject.SetActive(false);
        subtitle.text = "";
        subtitle.enabled = false;
        previusDialogue = "";
        waitDialogue = false;
    }

    void StopDialogue()
    {
        if (panel != null)
            panel.gameObject.SetActive(false);
        StopCoroutine(nextLineCoroutine);
        subtitle.text = "";
        subtitle.enabled = false;
        previusDialogue = "";
    }
}
