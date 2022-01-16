using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{

    public float movementSpeed;//
    private void Start()
    {
        //colocamos valores
        movementSpeed = 2f;

        controller = this.GetComponent<CharacterController>();
        anim = this.GetComponent<PlayerAnimations>();
        SetIdle();
    }
    private void Move()
    {
                                                 //eje x          eje y        eje z   
        Vector3 keyInput = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        controller.Move((transform.TransformDirection(keyInput) * movementSpeed) * Time.deltaTime); //da movimiento



        // Update animation (if so)
        if (m_PlayerState != PlayerState.IDLE && keyInput == Vector3.zero)
            m_PlayerState = PlayerState.IDLE;
        else if (m_PlayerState != PlayerState.MOVE)
            m_PlayerState = PlayerState.MOVE;
    }




























    public PlayerAnimations anim;
    public PlayerState m_PlayerState;
    public enum PlayerState
    {
        IDLE = 0,
        MOVE,
    }

    private CharacterController controller;
    private bool busy;

    private Vector3 m_NextPos;
    private float m_Speed;

    
   

    private void Update()
    {
        if (!busy)
        {
            Move();
            SetAnimations();
        }
    }

    

    public void SetIdle()
    {
        m_PlayerState = PlayerState.IDLE;
    }

    private void SetAnimations()
    {
        if (controller.velocity.magnitude <= 0.00f)
        {
            anim.SetMovement(0);
        }
        else if (controller.velocity.magnitude >= 0.1f)
        {
            anim.SetMovement(controller.velocity.magnitude);
        }
    }
}
