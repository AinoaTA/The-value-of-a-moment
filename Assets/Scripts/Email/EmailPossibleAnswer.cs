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
        FMODUnity.RuntimeManager.PlayOneShot("event:/Env/PC/Keys", transform.position);
        GameManager.GetManager().emailController.mail.currentAnswerOpen.Selected(id);
    }
}
