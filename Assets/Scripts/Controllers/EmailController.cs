using UnityEngine;

public class EmailController : MonoBehaviour
{
    [SerializeField] GameObject emailCanvas;
    [HideInInspector] public Email mail;
    public void Start()
    {
        GameManager.GetManager().emailController = this;
    }

    public void ShowEmail(bool v)
    {
        emailCanvas.SetActive(v);
    }
}
