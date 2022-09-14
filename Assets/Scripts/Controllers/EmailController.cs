using UnityEngine;

public class EmailController : MonoBehaviour
{
    public GameObject emailCanvas;
    public void Start()
    {
        GameManager.GetManager().emailController = this;
    }

    public void ShowEmail(bool v)
    {
        emailCanvas.SetActive(v);   
    }
}
