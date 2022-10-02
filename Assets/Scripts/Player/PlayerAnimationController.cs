using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    public Animator anim;
    public void SetAnimation(string animationName, bool rootMotion = false)
    {
        anim.applyRootMotion = rootMotion;
        Debug.LogWarning(animationName + " played");
        anim.Play(animationName);
        anim.enabled = true;
    }

    public void SetAnimationSpeed(string varName, float value)
    {
        anim.SetFloat(varName, value);
    }

    public void Active(bool v) { anim.enabled = v; }

    public void StartDay() 
    {
       //anim.applyRootMotion = true;
        anim.SetTrigger("GetUp");
    }
}
