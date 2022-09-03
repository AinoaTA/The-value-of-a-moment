using System.Collections;
using UnityEngine;

public class MobileController : MonoBehaviour
{
    public Messages standardAnswer;
    public Messages standardRepply;
    public Messages myRepply;
    public WPPChat[] chats;
    WPPChat currChat;
    public void OpenChat(int number)
    {
        currChat = chats[number];
        currChat.gameObject.SetActive(true);

        if (currChat.interactiveChat && !currChat.ChatStarted())
        {
            for (int i = 0; i < currChat.allAnswers.Length; i++)
            {
                Messages m = Instantiate(standardAnswer, transform.position, Quaternion.identity, currChat.answerContent.transform);
                m.content.text = currChat.allAnswers[currChat.indexStardChat].content;
                m.characterName.text = "";
                m.id_index = i;
                m.mobileAnswer = currChat.allAnswers[i];
            }
        }
    }

    public void SelectAnswer(MobileAnswers answer)
    {
        StartCoroutine(SelectAnsewerIE(answer.nextRepplies));
    }

    IEnumerator SelectAnsewerIE(MobileRepplies selected)
    {

        if (currChat.answerContent.transform.childCount >= 1)
            for (int i = 0; i < currChat.answerContent.transform.childCount; i++)
                Destroy(currChat.answerContent.transform.GetChild(i).transform.gameObject);


        for (int i = 0; i < selected.conver.Length; i++)
        {
            Messages rep;
            if (selected.conver[i].name == "Elle")
                rep = Instantiate(myRepply, transform.position, Quaternion.identity, currChat.visualContent.transform);
            else
                rep = Instantiate(standardRepply, transform.position, Quaternion.identity, currChat.visualContent.transform);

            rep.characterName.text = selected.conver[i].name;
            rep.content.text = selected.conver[i].answer;
            yield return new WaitForSeconds(0.5f);
        }


        for (int i = 0; i < selected.nextText.Length; i++)
        {
            Messages m = Instantiate(standardAnswer, transform.position, Quaternion.identity, currChat.answerContent.transform);
            m.mobileAnswer = selected.nextText[i];
            m.content.text = selected.nextText[i].content;
            m.name = "";
        }


    }
    private void Awake()
    {
        GameManager.GetManager().mobile = this;
    }
}