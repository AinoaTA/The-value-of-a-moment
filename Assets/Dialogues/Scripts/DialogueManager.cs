using System.Collections;
using System;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public DialoguesList dialogues;
    public TMP_Text subtitle;
    public float defaultvoiceTime = 2;
    public float aditionalVoiceTime = 0.2f;

    DialogueJSON currentDialogue;
    int currentLine;

    IEnumerator nextLineCoroutine;
    private static FMOD.Studio.EventInstance eventAudio;

    private void Awake()
    {
        GameManager.GetManager().dialogueManager = this;
    }

    Action saveAct;
    bool canRepeat;
    string previusDialogue;
    public void SetDialogue(string dialogue, Action act = null, bool forceInvoke = false, bool canRepeat = false)
    {
        if (dialogue == previusDialogue) return;
        previusDialogue = dialogue;
        if (nextLineCoroutine != null) StopDialogue();
        eventAudio.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);

        currentDialogue = dialogues.GetDialogue(dialogue);
        //this conver was played.
        saveAct = act;

        if (currentDialogue.lines[0].played)
        {
            if (forceInvoke)
                saveAct?.Invoke();

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
        subtitle.enabled = true;

        bool langESP = LanguageGame.lang == LanguageGame.Languages.ESP;

        subtitle.text = langESP ? line.es : line.en;

        float waitTime = defaultvoiceTime;
        int lenght;
        string path = "event:/Dialogue/" + LanguageGame.lang + "/" + line.ID;

        try
        {
            eventAudio = FMODUnity.RuntimeManager.CreateInstance(path);
            FMODUnity.RuntimeManager.GetEventDescription(FMODUnity.EventReference.Find(path)).getLength(out lenght);
            eventAudio.start();

            float time = (float)lenght / 1000;
        //    print("AUDIO LENGHT Mili: " + (float)lenght + "| in seconds: " + time);
            waitTime = time + aditionalVoiceTime;
        }
        catch (System.Exception e)
        {
           //print(e);
        }

        if (!canRepeat)
            line.played = true;
        StartCoroutine(nextLineCoroutine = NextLine(waitTime));
    }

    IEnumerator NextLine(float waitTime)
    {
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

        if (saveAct != null)
            saveAct?.Invoke();
        subtitle.text = "";
        subtitle.enabled = false;
        previusDialogue = "";
    }

    void StopDialogue() 
    {
        StopCoroutine(nextLineCoroutine);
        subtitle.text = "";
        subtitle.enabled = false;
        previusDialogue = "";
    }
}
