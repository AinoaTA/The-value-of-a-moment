using TMPro;
using UnityEngine;

public class Email : MonoBehaviour
{
    public GameObject emailContent;
    public string companyName;
    public string emailDir;
    public string title;

    public TMP_Text companyN, emailD, titleText;

    public void OpenEmail()
    {
        emailContent.SetActive(true);

        companyN.text = companyName;
        emailD.text = emailDir;
        titleText.text = title;
    }
}
