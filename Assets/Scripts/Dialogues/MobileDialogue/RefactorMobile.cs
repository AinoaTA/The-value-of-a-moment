using UnityEngine;

public class RefactorMobile : MonoBehaviour
{
    /*
      public enum Chats { first, second, third, four }
    public Chats currChat;
    public GameObject standardMessagePrefab, standardReplyPrefab;
    public GameObject standarZoeMessage, standarAriMessage;

    private int currentFirstMomentChat;
    private int currentSecondMomentChat;
    private int currentThirdMomentChat;
    [Space(5)]
    //when you choose message and they have to reply you.
    //[Header("CHAT STRUCTS")]
    //#region chats
    //[SerializeField] AllFirstChatAsnwers[] firstChatAnswers;
    //[Serializable]
    //public struct AllFirstChatAsnwers
    //{
    //    public Sprite[] ellePossibleAnswer;
    //    public Sprite[] someonesReply;
    //}
    ////---//
    //[SerializeField] AllSecondChatAsnwers[] SecondChatAnswers;
    //[Serializable]
    //public struct AllSecondChatAsnwers
    //{
    //    public string[] ellePossibleAnswer;
    //    public string[] ZoeReply;
    //    public string[] AriReply;
    //}
    ////---//
    //[SerializeField] AllThirdChatAsnwers[] ThirdChatAnswers;
    //[Serializable]
    //public struct AllThirdChatAsnwers
    //{
    //    public Sprite[] ellePossibleAnswer;
    //    public Sprite[] someonesReply;
    //}
    //#endregion

    [Header("INDIVIDUAL REFERENCES CHAT")]
    [Tooltip("Actives gameObject chat")]
    [SerializeField] private GameObject[] openGeneralChats;
    [SerializeField] private GameObject[] visualChats;
    [SerializeField] private GameObject[] answerChat;

    private List<GameObject> currAnswersShowing = new List<GameObject>();
    private int openedChat;
    private int numberSelected;

    private void Awake()
    {
        GameManager.GetManager().mobile = this;
    }

    public void OpenChat(int number)
    {
        openedChat = number;
        currChat = (Chats)number;
        openGeneralChats[number].SetActive(true);

        StartChat();
    }

    private void StartChat()
    {

    }

    //refactor
    //private void StartChat()
    //{
    //    switch (currChat)
    //    {
    //        case Chats.first:

    //            if (currentFirstMomentChat >= firstChatAnswers.Length)
    //                return;

    //            for (int i = 0; i < firstChatAnswers[currentFirstMomentChat].ellePossibleAnswer.Length; i++)
    //            {
    //                GameObject answer = Instantiate(standardMessagePrefab, transform.position, Quaternion.identity, answerChat[(int)currChat].transform);
    //                answer.GetComponent<Image>().color = Color.yellow;
    //                currAnswersShowing.Add(answer);
    //            }
    //            break;
    //        case Chats.second:
    //            if (currentSecondMomentChat >= SecondChatAnswers.Length)
    //                return;
    //            for (int i = 0; i < SecondChatAnswers[currentSecondMomentChat].ellePossibleAnswer.Length; i++)
    //            {
    //                GameObject answer = Instantiate(standardMessagePrefab, transform.position, Quaternion.identity, answerChat[(int)currChat].transform);
    //                answer.GetComponent<TriggerAnswerChat>().value = i;
    //                answer.GetComponent<TriggerAnswerChat>().text.text = SecondChatAnswers[currentSecondMomentChat].ellePossibleAnswer[i];
    //                answer.GetComponent<Image>().color = Color.cyan;
    //                currAnswersShowing.Add(answer);
    //            }
    //            break;
    //        case Chats.third:
    //            if (currentThirdMomentChat >= ThirdChatAnswers.Length)
    //                return;
    //            for (int i = 0; i < ThirdChatAnswers[currentThirdMomentChat].ellePossibleAnswer.Length; i++)
    //            {
    //                GameObject answer = Instantiate(standardMessagePrefab, transform.position, Quaternion.identity, answerChat[(int)currChat].transform);
    //                answer.GetComponent<TriggerAnswerChat>().value = i;
    //                answer.GetComponent<Image>().color = Color.yellow;
    //                currAnswersShowing.Add(answer);
    //            }
    //            break;
    //        case Chats.four:
    //            break;
    //    }
    //}

    //public void SelectedAnswer(int numberSelected)
    //{
    //    switch (currChat)
    //    {
    //        case Chats.first:
    //            currentFirstMomentChat++;
    //            currAnswersShowing[numberSelected].GetComponent<Button>().enabled = false;
    //            currAnswersShowing[numberSelected].transform.SetParent(visualChats[openedChat].transform);
    //            break;
    //        case Chats.second:
    //            currAnswersShowing[numberSelected].GetComponent<Button>().enabled = false;
    //            currAnswersShowing[numberSelected].transform.SetParent(visualChats[openedChat].transform);

    //            if (SecondChatAnswers[currentSecondMomentChat].ZoeReply[numberSelected].Length > 0)
    //            {
    //                GameObject newReplyZoe = Instantiate(standarZoeMessage, transform.position, Quaternion.identity, visualChats[openedChat].transform);
    //                newReplyZoe.GetComponent<TriggerAnswerChat>().text.text = SecondChatAnswers[currentSecondMomentChat].ZoeReply[numberSelected];
    //            }

    //            if (SecondChatAnswers[currentSecondMomentChat].AriReply[numberSelected].Length > 0)
    //            {
    //                GameObject newReplyAri = Instantiate(standarAriMessage, transform.position, Quaternion.identity, visualChats[openedChat].transform);
    //                newReplyAri.GetComponent<TriggerAnswerChat>().text.text = SecondChatAnswers[currentSecondMomentChat].AriReply[numberSelected];
    //            }

    //            currentSecondMomentChat++;
    //            break;
    //        case Chats.third:
    //            currentThirdMomentChat++;
    //            currAnswersShowing[numberSelected].GetComponent<Button>().enabled = false;
    //            currAnswersShowing[numberSelected].transform.SetParent(visualChats[openedChat].transform);
    //            break;
    //    }


    //    //GameObject newReply = Instantiate(standardReplyPrefab, transform.position, Quaternion.identity, visualChats[openedChat].transform);

    //    this.numberSelected = numberSelected;
    //    ClearAnswers();
    //    StartChat();
    //}

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
    }

    public void SelectAnswer(MobileAnswers selected)
    {
        selected.EEEE();
    }
     
     */
}
