using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    public Animator anim;

    private void Awake()
    {
        GameManager.GetManager().playerAnimationController = this;
    }
    public void SetAnimation(string animationName) 
    {
        anim.Play(animationName);
    }

    public void SetAnimationSpeed(string varName, float value)
    {
        anim.SetFloat(varName, value);
    }

}
