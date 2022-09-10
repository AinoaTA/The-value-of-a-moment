using System.Collections.Generic;
using UnityEngine;

public class DialogueReader : MonoBehaviour
{
    public TextAsset firstDay;

    [System.Serializable]
    public class Dialogue
    {
        public string ID;
        public string es;
        public string en;
    }

    [System.Serializable]
    public class DialogueList 
    {
        public Dialogue[] dialogue;
    }

    public DialogueList dialogueList = new DialogueList();

    private void Start()
    {
        dialogueList = JsonUtility.FromJson<DialogueList>(firstDay.text);
        print(dialogueList);
    }
}
