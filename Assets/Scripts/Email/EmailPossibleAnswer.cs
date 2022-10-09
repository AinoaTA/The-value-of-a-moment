using TMPro;
using UnityEngine;

public class EmailPossibleAnswer : MonoBehaviour
{
    int id;
    public string answer;
    public TMP_Text resume;

    public void SetAnswer(string t, int id, string ans)
    {
        resume.text = t;
        answer = ans;
        this.id = id;
    }

    public void SelectedAnswer() 
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/Env/UI/PC Keys", transform.position);
        GameManager.GetManager().emailController.mail.currentAnswerOpen.Selected(id);
    }
}
