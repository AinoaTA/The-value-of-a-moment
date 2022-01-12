using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class PlayerController : MonoBehaviour
{
    public PlayerAnimations anim;
    public enum PlayerState
    {
        IDLE = 0,
        MOVE,
    }

    public PlayerState m_PlayerState;

    private Vector3 m_NextPos;
    private NavMeshAgent m_NavMeshAgent;
    private float m_Speed = 2f;

    private void Awake()
    {
        m_NavMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        SetIdle();
        m_NavMeshAgent.speed = m_Speed;
    }
    private void Update()
    {
        switch (m_PlayerState)
        {
            case PlayerState.IDLE:
                {
                    UpdateIdle();
                }
                break;
            case PlayerState.MOVE:
                {
                    UpdateMove();
                }
                break;
            default:
                break;
        }
        SetAnimations();
    }

    public void SetIdle()
    {
        m_PlayerState = PlayerState.IDLE;
    }

    public void SetMove(Vector3 newPos)
    {

        m_NextPos = newPos;
        m_PlayerState = PlayerState.MOVE;
    }

    private void UpdateIdle() { }

    private void UpdateMove()
    {

        m_NextPos.y = 0;
        m_NavMeshAgent.destination = m_NextPos;
        m_NavMeshAgent.isStopped = false;

        StartCoroutine(DelaySetIdle());
    }

    private IEnumerator DelaySetIdle()
    {
        yield return new WaitForSeconds(0.3f);
        if (!m_NavMeshAgent.hasPath && m_NavMeshAgent.velocity.magnitude==0)
            SetIdle();
    }

    private void SetAnimations()
    {
        if (m_NavMeshAgent.velocity.magnitude <= 0.00f)
        {
            anim.SetMovement(0);
        }
        else if (m_NavMeshAgent.velocity.magnitude >= 0.1f)
            anim.SetMovement(m_NavMeshAgent.velocity.magnitude);
    }
}
