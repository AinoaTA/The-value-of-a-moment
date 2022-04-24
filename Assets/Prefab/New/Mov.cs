using System.Collections;
using UnityEngine;

public class Mov : MonoBehaviour
{
    public float m_angleCutOff = 30;
    public Camera cam;
    public float m_Speed, m_MaxSpeed = 2, m_StopSpeedOffset = 0.2f;
    public GameObject prop;

    Vector3 m_Forward, m_Right, m_Movement;

    [SerializeField] float m_LerpRotationPercentatge = 0.2f;
    [SerializeField] float m_CurrVelocityPlayer;
    CharacterController m_CharacterController;
    
    public Animator m_Anim;
    private void Start()
    {
        m_Speed = m_MaxSpeed;
        m_CharacterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (GameManager.GetManager().m_CurrentStateGame != GameManager.StateGame.GamePlay)
        {
            m_CurrVelocityPlayer = 0;
            m_Anim.SetFloat("Speed", Mathf.Clamp(m_CurrVelocityPlayer, 0, 1));
            return;
        }

        //parametros de la camara
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
        //print(CalculateWall(m_Forward));
        //Debug.DrawLine(transform.position, transform.position + m_Forward * 10);
        //if (CalculateWall(m_Forward))
        //{
        //    m_CurrVelocityPlayer = 0;
        //    //StartCoroutine(DelayAnimation());
        //}
       
        m_Anim.SetFloat("Speed", Mathf.Clamp(m_CurrVelocityPlayer, 0, 1));

        m_Movement.Normalize();


        if (m_Movement != Vector3.zero)
            prop.transform.rotation = Quaternion.Lerp(prop.transform.rotation, Quaternion.LookRotation(m_Movement), m_LerpRotationPercentatge);

        m_Movement *= m_Speed * Time.deltaTime;

        CollisionFlags m_CollisionFlags = m_CharacterController.Move(m_Movement);

        SetAnimations();
    }

    private void SetAnimations()
    {
        if (m_CharacterController.velocity.magnitude <= m_MaxSpeed - m_StopSpeedOffset)
        {
            m_Speed = 0.0f;
        }
    }


    //private bool CalculateWall(Vector3 playerForward)
    //{
    //    Vector3 otherPos = GameManager.GetManager().PlayerController.WallPoint - transform.position;
    //    float cosAngle = Vector3.Dot(playerForward, otherPos);
    //    float angle = Mathf.Acos(cosAngle) * Mathf.Rad2Deg;

    //    Debug.DrawLine(GameManager.GetManager().PlayerController.WallPoint,GameManager.GetManager().PlayerController.WallPoint + otherPos * -10,Color.red);
    //    return angle<m_angleCutOff;
    //}

    //IEnumerator DelayAnimation()
    //{

    //    float t = 0;
    //    float prev = m_CurrVelocityPlayer;
    //    while (t < 2)
    //    {
    //        yield return new WaitForSeconds(0.5f);
    //        t += Time.deltaTime;
    //        m_CurrVelocityPlayer = Mathf.Lerp(prev, 0, t / 1);
    //        m_Anim.SetFloat("Speed", Mathf.Clamp(m_CurrVelocityPlayer, 0, 1));
    //    }

    //}
}
