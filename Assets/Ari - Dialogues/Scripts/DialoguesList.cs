using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialogues List", menuName = "Dialogues/List", order = 2)]
public class DialoguesList : ScriptableObject
{
    public TextAsset dialoguesFile;
    public string voicesPath = "";
    public string voicesPathEn = "";
    public List<DialogueJSON> dialogues;

    private Dictionary<string, DialogueJSON> dialoguesDictionary;

    void Init()
    {
        dialoguesDictionary = new Dictionary<string, DialogueJSON>();
        foreach (DialogueJSON dialogue in dialogues)
            dialoguesDictionary[dialogue.id] = dialogue;
    }

    public DialogueJSON GetDialogue(string dialogue) {
        if (dialoguesDictionary == null) Init();
        return dialoguesDictionary[dialogue];
    }
}

[System.Serializable]
public class DialogueJSON
{
    public string id;
    public List<DialogueLineJSON> lines = new List<DialogueLineJSON>();
}

[System.Serializable]
public class DialogueLineJSON
{
    public string ID;
    [HideInInspector] public string dialogue;
    public AudioClip voice_es;
    public AudioClip voice_en;
    public string es;
    public string en;
}