using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prueba : MonoBehaviour
{
    public Animator anim;


    private IEnumerator Start()
    {
        SetRoot(false);
        yield return new WaitForSeconds(1.5f);
        SetRoot(true);
        anim.SetTrigger("one");

        yield return new WaitForSeconds(5f);
        SetRoot(false);
        anim.SetTrigger("one");
       
    }
    public void SetRoot(bool v) 
    {
        anim.applyRootMotion = v;
    }
}
