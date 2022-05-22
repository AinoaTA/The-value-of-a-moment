using UnityEngine.UI;
using UnityEngine;
using System;
using System.Collections.Generic;

public class MobileController : MonoBehaviour
{
    public enum Chats { first, second, third, four }
    public Chats currChat;
    public GameObject standarMessagePrefab;

    private int currentFirstMomentChat;
    private int currentSecondMomentChat;
    private int currentThirdMomentChat;
    [Space(5)]
    //when you choose message and they have to reply you.
    [Header ("CHAT STRUCTS")]
    #region chats
    [SerializeField] AllFirstChatAsnwers[] firstChatAnswers;
    [Serializable]
    public struct AllFirstChatAsnwers
    {
        public Sprite[] ellePossibleAnswer;
        public Sprite[] someonesReply;
    }
    //---//
    [SerializeField] AllSecondChatAsnwers[] SecondChatAnswers;
    [Serializable]
    public struct AllSecondChatAsnwers
    {
        public Sprite[] ellePossibleAnswer;
        public Sprite[] someonesReply;
    }
    //---//
    [SerializeField] AllThirdChatAsnwers[] ThirdChatAnswers;
    [Serializable]
    public struct AllThirdChatAsnwers
    {
        public Sprite[] ellePossibleAnswer;
        public Sprite[] someonesReply;
    }
    #endregion

    [Header("INDIVIDUAL REFERENCES CHAT")]
    [Tooltip("Actives gameObject chat")]
    [SerializeField] private GameObject[] openGeneralChats;
    [SerializeField] private GameObject[] visualChats;
    [SerializeField] private GameObject[] answerChat;
 
    private void Awake()
    {
        GameManager.GetManager().mobile = this;
    }
    [Space(15)]

    [SerializeField]private List<GameObject> currAnswersShowing = new List<GameObject>();
    private int openedChat;
    [SerializeField]private int numberSelected;

    public void OpenChat(int number)
    {
        openedChat = number;
        currChat = (Chats)number;
        openGeneralChats[number].SetActive(true);

        StartChat();
    }

    private void StartChat()
    {
        switch (currChat)
        {
            case Chats.first:

                if (currentFirstMomentChat >= firstChatAnswers.Length)
                    return;

                for (int i = 0; i < firstChatAnswers[currentFirstMomentChat].ellePossibleAnswer.Length; i++)
                {
                    GameObject answer = Instantiate(standarMessagePrefab, transform.position, Quaternion.identity, answerChat[(int)currChat].transform);
                    answer.GetComponent<TriggerAnswerChat>().value = i;
                    answer.GetComponent<Image>().color = Color.yellow;
                    currAnswersShowing.Add(answer);
                }
                break;
            case Chats.second:
                if (currentSecondMomentChat >= SecondChatAnswers.Length)
                    return;
                for (int i = 0; i < SecondChatAnswers[currentSecondMomentChat].ellePossibleAnswer.Length; i++)
                {
                    GameObject answer = Instantiate(standarMessagePrefab, transform.position, Quaternion.identity, answerChat[(int)currChat].transform);
                    answer.GetComponent<TriggerAnswerChat>().value = i;
                    answer.GetComponent<Image>().color = Color.cyan;
                    currAnswersShowing.Add(answer);
                }
                break;
            case Chats.third:
                for (int i = 0; i < ThirdChatAnswers[currentThirdMomentChat].ellePossibleAnswer.Length; i++)
                {
                    GameObject answer = Instantiate(standarMessagePrefab, transform.position, Quaternion.identity, answerChat[(int)currChat].transform);
                    answer.GetComponent<TriggerAnswerChat>().value = i;
                    answer.GetComponent<Image>().color = Color.yellow;
                    currAnswersShowing.Add(answer);
                }
                break;
            case Chats.four:
                break;
        }
    }

    public void SelectedAnswer(int numberSelected)
    {
        switch (currChat)
        {
            case Chats.first:
                currentFirstMomentChat++;
                break;
            case Chats.second:
                currentSecondMomentChat++;
                break;
            case Chats.third:
                currentThirdMomentChat++;
                break;
        }
        currAnswersShowing[numberSelected].GetComponent<Button>().enabled = false;
        currAnswersShowing[numberSelected].transform.parent = visualChats[openedChat].transform;

        this.numberSelected = numberSelected;
        ClearAnswers();
        StartChat();
    }

    private void ClearAnswers(bool destroyAll = false)
    {
        for (int i = 0; i < currAnswersShowing.Count; i++)
        {
            if (destroyAll)
            {
                Destroy(currAnswersShowing[i]);
            }
            else
            {
                if (currAnswersShowing[numberSelected] != currAnswersShowing[i])
                    Destroy(currAnswersShowing[i]);
            }
        }
        currAnswersShowing.Clear();
    }

    public void CloseChat()
    {
        ClearAnswers(true);
        openGeneralChats[(int)currChat].SetActive(false);
        GameManager.GetManager().m_CurrentStateGame = GameManager.StateGame.GamePlay;
    }
}