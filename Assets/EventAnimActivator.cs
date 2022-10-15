using System.Collections;
using UnityEngine;

public class EventAnimActivator : MonoBehaviour
{
    public Animator alexAnimator;

    public void AnimationAlexWalk() 
    {
        StartCoroutine(WalkAlex());
    }

    IEnumerator WalkAlex() 
    {
        float t = 0;
        while (t < 1) 
        {
            t += Time.deltaTime;
            alexAnimator.SetFloat("Walk", t);
            yield return null;
        }
    }
}
