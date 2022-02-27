using System.Collections;
using System.Collections.Generic;
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
        anim.SetFloat("Speed",speed);
    }

    public void SetComputer(bool value)
    {
        anim.SetBool("_Computer", value);
        Debug.Log(anim.GetBool("_Computer"));
    }
}
