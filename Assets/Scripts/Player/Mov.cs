using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Mov : MonoBehaviour
{
    //[Range(0,1)]
    //public float dotCutOff = 0.7f;
    private Vector2 m_MovementAxis;
    public Vector2 MovementAxis { get { return m_MovementAxis.normalized; } set { m_MovementAxis = value; } }
    private Vector3 m_Direction;
    public Camera cam;
    public float m_Speed, m_MaxSpeed = 2, m_StopSpeedOffset = 0.2f;
    public GameObject prop;
    bool moving;
    Vector3 m_Forward, m_Right, m_Movement;

    [SerializeField] float m_LerpRotationPercentatge = 0.2f;
    [SerializeField] float m_CurrVelocityPlayer;
    CharacterController m_CharacterController;

    public Animator m_Anim;
    // bool cutOff;
    private void Start()
    {
        m_Speed = m_MaxSpeed;
        m_CharacterController = GetComponent<CharacterController>();

         
        GameManager.GetManager().playerInputs._MoveUp += MoveUp;
        GameManager.GetManager().playerInputs._MoveDown += MoveDown;
        GameManager.GetManager().playerInputs._MoveLeft += MoveLeft;
        GameManager.GetManager().playerInputs._MoveRight += MoveRight;
        GameManager.GetManager().playerInputs._ResetMove += ResetMove;
        GameManager.GetManager().playerInputs._StopMoving += StopMoving;
    }

    private void OnDisable()
    {
        GameManager.GetManager().playerInputs. _MoveUp -= MoveUp;
        GameManager.GetManager().playerInputs._MoveDown -= MoveDown;
        GameManager.GetManager().playerInputs._MoveLeft -= MoveLeft;
        GameManager.GetManager().playerInputs._MoveRight -= MoveRight;
        GameManager.GetManager().playerInputs._ResetMove -= ResetMove;
        GameManager.GetManager().playerInputs._StopMoving -= StopMoving;
    }


    

    private void ResetMove()
    {
        print("reset");
        m_MovementAxis = Vector2.zero;
    }
    private void MoveLeft()
    {
        print("left");
        m_MovementAxis += new Vector2(-1, 0);
        moving = true;
    }

    private void MoveRight()
    {
        print("right");
        m_MovementAxis += new Vector2(1, 0);
        moving = true;
    }

    private void MoveUp()
    {
        print("up"); ;
        m_MovementAxis += new Vector2(0, 1);
        moving = true;
    }

    private void MoveDown()
    {
        print("omodeonw");
        m_MovementAxis += new Vector2(0, -1);
        moving = true;
    }

    private void StopMoving()
    {
        print("stop");
        m_MovementAxis = Vector2.zero;
        moving = false;
    }

    void Update()
    {
        if (GameManager.GetManager().gameStateController.m_CurrentStateGame != GameStateController.StateGame.GamePlay)
        {
            //m_CurrVelocityPlayer = 0;
            //m_Anim.SetFloat("Speed", Mathf.Clamp(m_CurrVelocityPlayer, 0, 1));
            return;
        }

        if (m_MovementAxis != Vector2.zero)
        {
            print("in mov");
            Vector3 forward = cam.transform.forward;
            Vector3 right = cam.transform.right;
            forward.y = 0.0f;
            right.y = 0.0f;
            forward.Normalize();
            right.Normalize();

            Vector2 movementAxis = m_MovementAxis;
            m_Direction += forward * movementAxis.y;
            m_Direction += right * movementAxis.x;
             m_Direction.Normalize();

            Vector3 movement = m_Direction*Time.deltaTime* m_Speed;

            CollisionFlags m_CollisionFlags = m_CharacterController.Move(movement);
        }

        

        //parametros de la camara
        //m_Forward = cam.transform.forward;
        //m_Right = cam.transform.right;

        //m_Forward.y = 0;
        //m_Right.y = 0;

        //m_Forward.Normalize();
        //m_Right.Normalize();

        //if (Input.GetKey(KeyCode.A))
        //{
        //    m_Movement = -m_Right;
        //}
        //if (Input.GetKey(KeyCode.D))
        //{
        //    m_Movement = m_Right;
        //}

        //if (Input.GetKey(KeyCode.W))
        //{
        //    m_Movement += m_Forward;
        //}
        //if (Input.GetKey(KeyCode.S))
        //{
        //    m_Movement -= m_Forward;
        //}

        //slow anim transition(walk to idle)
        //if (!Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        //{
        //    m_Speed -= 0.03f;
        //    m_Speed = Mathf.Clamp(m_Speed, 0.0f, m_MaxSpeed);
        //    m_CurrVelocityPlayer = m_CharacterController.velocity.magnitude;
        //    m_CurrVelocityPlayer -= 0.01f;
        //    Mathf.Clamp(m_CurrVelocityPlayer, 0, 1);
        //}
        //else
        //{
        //    m_CurrVelocityPlayer = m_CharacterController.velocity.magnitude;
        //    m_Speed = m_MaxSpeed;
        //}

        //print(CalculateWall(m_Forward));
        //Debug.DrawLine(transform.position, transform.position + m_Forward * 10);

        //if (cutOff)//(CalculateWall(m_Forward))
        //{
        //    m_CurrVelocityPlayer = 0;
        //    StartCoroutine(DelayAnimation());
        //}

        //m_Anim.SetFloat("Speed", Mathf.Clamp(m_CurrVelocityPlayer, 0, 1));

        //m_Movement.Normalize();

        //if (m_Movement != Vector3.zero)
        //    prop.transform.rotation = Quaternion.Lerp(prop.transform.rotation, Quaternion.LookRotation(m_Movement), m_LerpRotationPercentatge);

        //m_Movement *= m_Speed * Time.deltaTime;

        //CollisionFlags m_CollisionFlags = m_CharacterController.Move(m_Movement);

        //SetAnimations();
    }

    private void SetAnimations()
    {
        if (m_CharacterController.velocity.magnitude <= m_MaxSpeed - m_StopSpeedOffset)
        {
            m_Speed = 0.0f;
        }
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.layer == 3 << 3)
    //    {
    //        Vector3 otherPos = collision.transform.position - transform.position;
    //        float dot = Vector3.Dot(transform.forward, otherPos);

    //        cutOff = dot < dotCutOff;
    //        print(cutOff + collision.collider.name);
    //    }
    //}
}
