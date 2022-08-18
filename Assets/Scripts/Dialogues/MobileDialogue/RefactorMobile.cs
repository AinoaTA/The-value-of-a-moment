using UnityEngine;

public class RefactorMobile : MonoBehaviour
{
    public GameObject interactiveChat;
    public GameObject answerChat;
    
    public void OpenChat()//int number)
    {
        interactiveChat.SetActive(true);
    }


}
