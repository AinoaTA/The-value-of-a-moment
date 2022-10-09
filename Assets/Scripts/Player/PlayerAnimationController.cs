using System.Collections;
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

    IEnumerator routine;
    public void InterctAnim()
    {
        if (routine == null)
            StartCoroutine(routine = AnimationSpeedAffect());
    }

    IEnumerator AnimationSpeedAffect()
    {
        float t = 0;
        anim.speed = 1;
        while (t < 2) 
        {
            t += Time.deltaTime;
            anim.speed = Mathf.Lerp(1, 0, t /2);
            yield return null;
        }
      
        routine = null;
    }
}
