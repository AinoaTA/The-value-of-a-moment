using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    public Animator anim;
    public void SetAnimation(string animationName) 
    {
        Debug.LogWarning(animationName + " played");
        anim.Play(animationName);
    }

    public void SetAnimationSpeed(string varName, float value)
    {
        anim.SetFloat(varName, value);
    }
}
