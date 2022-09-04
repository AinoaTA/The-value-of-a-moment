using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WPPChat : MonoBehaviour
{
    [Tooltip("Where messages saves")]
    public GameObject visualContent;
    [Tooltip("Where answer to send saves")]
    public GameObject answerContent;
    public bool interactiveChat;
    public int indexStardChat;
    public bool chatFinish;
    public bool chatStarted;
    public ConverDay[] conversations;
    [System.Serializable]
    public struct ConverDay
    {
        public string name;
        public MobileAnswers[] allAnswers;
    }

    public void ChatFinish()
    {
        chatStarted = false;
        chatFinish = true;
    }
    public void NewDayNewChat()
    {
        chatStarted = false;
        chatFinish = false;
        indexStardChat++;
    }
}
