using UnityEngine;

public class WPPChat : MonoBehaviour
{
    [Tooltip("Where messages saves")]
    public GameObject visualContent;
    [Tooltip("Where answer to send saves")]
    public GameObject answerContent;
    public bool interactiveChat;
    public int indexStardChat;
    [HideInInspector] public bool chatFinish;
    [HideInInspector] public bool chatStarted;
    bool nextChat = true;
    public ConverDay[] conversations;
    [System.Serializable]
    public struct ConverDay
    {
        public string name;
        public MobileAnswers[] allAnswers;
    }

    public bool CanStartNewChat()
    {
        return nextChat;
    }

    public void StartNewChat()
    {
        nextChat = false;
        chatStarted = true;
    }


    public void ChatFinish()
    {
        chatStarted = false;
        chatFinish = true;
    }
    public void NewDayNewChat()
    {
        nextChat = true;
        chatStarted = false;
        chatFinish = false;
        indexStardChat++;
    }
}
