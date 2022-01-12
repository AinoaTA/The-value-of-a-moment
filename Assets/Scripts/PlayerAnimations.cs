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
}
