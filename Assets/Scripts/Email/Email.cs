using TMPro;
using UnityEngine;

public class Email : MonoBehaviour
{
    public GameObject emailContent;
    public string companyName;
    public string emailDir;
    public string title;

    public TMP_Text companyN, emailD, titleText;

    public EmailAnswer currentAnswerOpen;

    private void Start()
    {
        GameManager.GetManager().email = this;
    }
    public void OpenEmail()
    {
        emailContent.SetActive(true);

        companyN.text = companyName;
        emailD.text = emailDir;
        titleText.text = title;
    }
}
