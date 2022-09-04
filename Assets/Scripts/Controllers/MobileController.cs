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
        if (currChat.interactiveChat && currChat.CanStartNewChat())
        {
            currChat.StartNewChat();
            for (int i = 0; i < currChat.conversations[currChat.indexStardChat].allAnswers.Length; i++)
            {
                if (currChat.conversations[currChat.indexStardChat].allAnswers[i].previusRepply != null)
                    StartCoroutine(SelectAnsewerIE(currChat.conversations[currChat.indexStardChat].allAnswers[i].previusRepply, true));

                Messages m = Instantiate(standardAnswer, transform.position, Quaternion.identity, currChat.answerContent.transform);
                m.content.text = currChat.conversations[currChat.indexStardChat].allAnswers[i].content;
                m.characterName.text = "";
                m.mobileAnswer = currChat.conversations[currChat.indexStardChat].allAnswers[i];
            }
        }
    }

    public void SelectAnswer(MobileAnswers answer)
    {
        StartCoroutine(SelectAnsewerIE(answer.nextRepplies));
    }

    IEnumerator SelectAnsewerIE(MobileRepplies selected, bool startConver = false)
    {
        if (currChat.answerContent.transform.childCount >= 1)
            for (int i = 0; i < currChat.answerContent.transform.childCount; i++)
                Destroy(currChat.answerContent.transform.GetChild(i).transform.gameObject);

        for (int i = 0; i < selected.conver.Length; i++)
        {
            Messages rep;
            if (selected.conver[i].names == MobileRepplies.Names.Elle)
                rep = Instantiate(myRepply, transform.position, Quaternion.identity, currChat.visualContent.transform);
            else
                rep = Instantiate(standardRepply, transform.position, Quaternion.identity, currChat.visualContent.transform);

            rep.characterName.text = selected.conver[i].names.ToString();
            rep.content.text = selected.conver[i].answer;
            yield return new WaitForSeconds(0.5f);
        }

        if (startConver)
            yield break;

        if (selected.nextText.Length == 0)
        {
            currChat.ChatFinish();
        }

        for (int i = 0; i < selected.nextText.Length; i++)
        {
            Messages m = Instantiate(standardAnswer, transform.position, Quaternion.identity, currChat.answerContent.transform);
            m.mobileAnswer = selected.nextText[i];
            m.content.text = selected.nextText[i].content;
            m.characterName.text = "";
        }
    }
    public void CloseChat() 
    {
        currChat.gameObject.SetActive(false);
        currChat = null;
    }

    private void Awake()
    {
        GameManager.GetManager().mobile = this;
    }
}