using TMPro;
using UnityEngine;

public class EmailPossibleAnswer : MonoBehaviour
{
    int id;
    public TMP_Text answer;

    public void SetAnswer(string t, int id)
    {
        answer.text = t;
        this.id = id;
    }

    public void SelectedAnswer() 
    {
        GameManager.GetManager().email.currentAnswerOpen.Selected(id);
    }
}
