using TMPro;
using UnityEngine;

public class EmailAnswer : MonoBehaviour
{
    public TMP_Text t;
    public float autocontrolSave;
    public EmailPossibleAnswer prefabAnswer;
    public Transform contentPossibleAnswers;

    public PossibleAnswer[] possibleAnswers;
    [System.Serializable]
    public struct PossibleAnswer
    {
        public string answer;
        public float autocontrol;
    }

    public void ShowAllAnswers()
    {
        if (GameManager.GetManager().email.blockWarning) return;
        GameManager.GetManager().email.currentAnswerOpen = this;
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
        t.text = possibleAnswers[id].answer;
        autocontrolSave = possibleAnswers[id].autocontrol;
        CloseAllAnswers();
    }

    void CloseAllAnswers()
    {
        GameManager.GetManager().email.currentAnswerOpen = null;
        contentPossibleAnswers.gameObject.SetActive(false);
    }
}
