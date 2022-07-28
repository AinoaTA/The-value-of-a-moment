using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }


    public void SetMovement(float speed)
    {
        anim.SetFloat("Speed", speed);
    }

    public void SetInteractable(string interactable)
    {
        anim.SetTrigger(interactable);
    }

    //public void ExitInteractable()
    //{
    //    anim.ResetTrigger("Exit");
    //    if (GameManager.GetManager().m_CurrentStateGame != GameManager.StateGame.GamePlay)
    //    {
    //        anim.SetTrigger("Exit");
    //    }
    //}
}
