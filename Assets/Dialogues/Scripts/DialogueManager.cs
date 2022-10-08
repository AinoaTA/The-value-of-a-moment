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

    private void Awake()
    {
        GameManager.GetManager().dialogueManager = this;
    }

    public void StartDialogue(string dialogue)
    {
        if (nextLineCoroutine!=null) StopCoroutine(nextLineCoroutine);
        //audioSource.Stop();

       
        currentDialogue = dialogues.GetDialogue(dialogue);
        //this conver was played.
        if (currentDialogue.lines[0].played)
            return;
        currentLine = 0;
        ShowLine();
    }

    void ShowLine() {
        DialogueLineJSON line = currentDialogue.lines[currentLine];
        subtitle.enabled = true;

        bool langESP = LanguageGame.lang == LanguageGame.Languages.ESP;

        subtitle.text = langESP?line.es:line.en;

        float waitTime = defaultvoiceTime;
        AudioClip voice = langESP ? line.voice_es : line.voice_en;
        print(voice);
        if (voice != null)
        {
            FMODUnity.RuntimeManager.CreateInstance("event:/Dialogue/"+LanguageGame.lang+"/"+line.ID);
            waitTime = voice.length + aditionalVoiceTime;
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

    void EndDialog() {
        subtitle.enabled = false;
    }
}
