using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public PlayerAnimations anim;
    public Transform m_PlayerWakeUp;
    public Transform m_PlayerSleep;

    private Mov mov;
    private bool sleep;

    private CharacterController character;

    private void Awake()
    {
        sleep = true;
        GameManager.GetManager().PlayerController = this;
        anim = this.GetComponent<PlayerAnimations>();
        mov = GetComponent<Mov>();
        character = GetComponent<CharacterController>();
    }
    private void Start()
    {
        PlayerSleepPos();
    }
    public void PlayerWakeUpPos()
    {
        character.enabled = false;
        sleep = false;
        mov.m_Anim.SetBool("Sleep", sleep);

        transform.SetPositionAndRotation(m_PlayerWakeUp.position, m_PlayerWakeUp.rotation);
        character.enabled = true;
    }

    public void PlayerSleepPos()
    {
        character.enabled = false;
        sleep = true;
        transform.SetPositionAndRotation(m_PlayerSleep.position, m_PlayerSleep.rotation);

        mov.m_Anim.SetBool("Sleep", sleep);
        character.enabled = true;
    }

    public void SetInteractable(string interactable)
    {
        anim.SetInteractable(interactable);
    }

    public void ExitInteractable()
    {
        anim.ExitInteractable();
    }
}