using System.Collections;
using System.Collections.Generic;
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

    public void StartDialogue(string dialogue)
    {
        if (nextLineCoroutine != null) StopCoroutine(nextLineCoroutine);
        eventAudio.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);

        currentDialogue = dialogues.GetDialogue(dialogue);
        //this conver was played.
        print(currentDialogue.lines[0].played);
        if (currentDialogue.lines[0].played)
            return;
        print("?");
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
        int lenght=0;
        string path = "event:/Dialogue/" + LanguageGame.lang + "/" + line.ID;

        print(path);

        try
        {
            eventAudio = FMODUnity.RuntimeManager.CreateInstance(path);
            FMODUnity.RuntimeManager.GetEventDescription(FMODUnity.EventReference.Find(path)).getLength(out lenght);
            eventAudio.start();
            print("patat"+ lenght/3600);
            waitTime = lenght + aditionalVoiceTime;
        }
        catch (System.Exception e)
        {
            print(e);
        }

        line.played = true;
        nextLineCoroutine = NextLine(waitTime);
        StartCoroutine(nextLineCoroutine);
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
        subtitle.enabled = false;
    }
}
