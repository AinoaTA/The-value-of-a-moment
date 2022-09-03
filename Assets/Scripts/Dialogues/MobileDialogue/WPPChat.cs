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
    public MobileAnswers[] allAnswers;

    public bool ChatStarted()
    {
        return allAnswers[indexStardChat].started;
    }

    public void ChatFinish()
    {
        allAnswers[indexStardChat].finished= true;
        indexStardChat++;
    }



}
