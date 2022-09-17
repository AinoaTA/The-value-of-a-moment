using TMPro;
using UnityEngine;

public class EmailAnswer : MonoBehaviour
{
    [SerializeField] Email mail;
    [SerializeField] int idPragraph;
    [HideInInspector] public float autocontrolSave;
    public EmailPossibleAnswer prefabAnswer;
    public Transform contentPossibleAnswers;


    public PossibleAnswer[] possibleAnswers;
    [System.Serializable]
    public struct PossibleAnswer
    {
        public string answer;
        public float autocontrol;
    }

    bool open;
    public void Open()
    {
        if (mail.currentAnswerOpen == null)
            mail.currentAnswerOpen = this;

        else if (mail.currentAnswerOpen != this)
        {
            mail.currentAnswerOpen.CloseAllAnswers();
            mail.currentAnswerOpen = this;
        }

        open = !open;
        if (open)
            ShowAllAnswers();
        else
            CloseAllAnswers();
    }
    public void ShowAllAnswers()
    {
        if (mail.blockWarning) return;
        mail.currentAnswerOpen = this;
        contentPossibleAnswers.gameObject.SetActive(true);

        if (contentPossibleAnswers.childCount <= 0)
        {
            for (int id = 0; id < possibleAnswers.Length; id++)
            {
                EmailPossibleAnswer a = Instantiate(prefabAnswer, transform.position, Quaternion.identity, contentPossibleAnswers);
                a.SetAnswer(possibleAnswers[id].answer, id);
            }
        }
    }

    public void Selected(int id)
    {
        mail.SetTextParagraph(idPragraph, possibleAnswers[id].answer);
        autocontrolSave = possibleAnswers[id].autocontrol;
        CloseAllAnswers();
    }

    void CloseAllAnswers()
    {
        mail.currentAnswerOpen = null;
        contentPossibleAnswers.gameObject.SetActive(false);
    }
}
