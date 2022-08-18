using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialogues", menuName = "MobileAnswers/NewAnswer", order = 1)]
public class MobileAnswers : ScriptableObject
{
    public Conversation[] conver;
    [System.Serializable]
    public struct Conversation 
    {
        public string name;
        public string answer;
    }

    public void EEEE() { Debug.Log("halo"); }
}
