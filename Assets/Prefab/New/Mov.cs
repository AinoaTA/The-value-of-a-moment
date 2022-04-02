using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mov : MonoBehaviour
{
    public Camera cam;
    public float m_Speed;
    public float m_MaxSpeed = 2;

    Vector3 m_Forward;
    Vector3 m_Right;

    Vector3 m_Movement;

    [SerializeField] float m_LerpRotationPercentatge = 0.2f;
    [SerializeField] float m_CurrVelocityPlayer;
    CharacterController m_CharacterController;
    Animator m_Anim;

    private void Start()
    {
        m_Speed = m_MaxSpeed;
        m_CharacterController = GetComponent<CharacterController>();
        m_Anim = GetComponentInChildren<Animator>();
    }
    void Update()
    {
        m_Forward = cam.transform.forward;
        m_Right = cam.transform.right;

        m_Forward.y = 0;
        m_Right.y = 0;

        m_Forward.Normalize();
        m_Right.Normalize();

        if (Input.GetKey(KeyCode.A))
        {
            m_Movement = -m_Right;
        }
        if (Input.GetKey(KeyCode.D))
        {
            m_Movement = m_Right;
        }

        if (Input.GetKey(KeyCode.W))
        {
            m_Movement += m_Forward;
        }
        if (Input.GetKey(KeyCode.S))
        {
            m_Movement -= m_Forward;
        }

        //slow anim transition (walk to idle) 
        if (!Input.anyKey)
        {
            m_Speed -= 0.03f;
            m_Speed = Mathf.Clamp(m_Speed, 0.0f, m_MaxSpeed);
            m_CurrVelocityPlayer = m_CharacterController.velocity.magnitude;
            m_CurrVelocityPlayer -= 0.01f;
            Mathf.Clamp(m_CurrVelocityPlayer, 0, 1);
        }
        else
        {
            m_CurrVelocityPlayer = m_CharacterController.velocity.magnitude;
            m_Speed = m_MaxSpeed;
        }
        m_Anim.SetFloat("Speed", Mathf.Clamp(m_CurrVelocityPlayer, 0, 1));

        m_Movement.Normalize();
        if (m_Movement != Vector3.zero)
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(m_Movement), m_LerpRotationPercentatge);

        m_Movement *= m_Speed * Time.deltaTime;

        CollisionFlags m_CollisionFlags = m_CharacterController.Move(m_Movement);
    }

}
