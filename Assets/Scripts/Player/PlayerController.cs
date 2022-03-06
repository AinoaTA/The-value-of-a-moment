using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    public float movementSpeed = 2f;
    public PlayerAnimations anim;
    public PlayerState m_PlayerState;
    public bool xAxisInverted;
    public bool yAxisInverted;
    public Transform m_PlayerWakeUp;
    public Transform m_PlayerSleep;
    private float m_VerticalSpeed = 1;
    private bool m_OnGround;
    private float turnSmoothTime = 0.15f;
    private float turnSmoothVel;
    public enum PlayerState
    {
        SLEEP = 0,
        IDLE,
        MOVE,
        COMPUTER
    }

    //private bool busy;

    private Vector3 m_NextPos;
    private float m_Speed;



    private void Awake()
    {
        GameManager.GetManager().PlayerController = this;
    }
    private void Start()
    {
        controller = this.GetComponent<CharacterController>();
        anim = this.GetComponent<PlayerAnimations>();
        //SetIdle();
    }
    private void Move()
    {

        float horizontal = Input.GetAxis("Horizontal") * (xAxisInverted ? -1.0f : 1.0f);
        float vertical = Input.GetAxis("Vertical") * (yAxisInverted ? -1.0f : 1.0f);
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVel, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 movement = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            //movement.y = m_VerticalSpeed * Time.deltaTime;
            //Gravity(movement);
            controller.Move(movement.normalized * movementSpeed * Time.deltaTime);
        }

        //Vector3 keyInput = new Vector3(Input.GetAxis("Horizontal")*(xAxisInverted ? -1.0f : 1.0f), 0, Input.GetAxis("Vertical") * (yAxisInverted ? -1.0f : 1.0f)); //est� la c�mara mirando desde un lado
        //Vector3 l_movement = (transform.TransformDirection(keyInput) * movementSpeed) * Time.deltaTime;



        //l_movement.y = m_VerticalSpeed * Time.deltaTime;
        //m_VerticalSpeed += Physics.gravity.y * Time.deltaTime;

        ////controller.Move(l_movement);
        //Gravity(l_movement);
        //Update animation(if so)

        //if (m_PlayerState != PlayerState.IDLE && keyInput == Vector3.zero)
        //    m_PlayerState = PlayerState.IDLE;
        //else if (m_PlayerState != PlayerState.MOVE)
        //    m_PlayerState = PlayerState.MOVE;
    }

    private void Gravity(Vector3 movement)
    {
        CollisionFlags l_CollisionFlags = controller.Move(movement);
        if ((l_CollisionFlags & CollisionFlags.Below) != 0)
        {
            m_OnGround = true;
            m_VerticalSpeed = 0.0f;
        }
        else
            m_OnGround = false;
        if ((l_CollisionFlags & CollisionFlags.Above) != 0 && m_VerticalSpeed > 0.0f)
            m_VerticalSpeed = 0.0f;

    }
    private void Update()
    {
        if (GameManager.GetManager().m_CurrentStateGame== GameManager.StateGame.GamePlay)
        {
            Move();
            if (Input.GetKey(KeyCode.Escape))
            {
                ExitInteractable();
            }
            //SetAnimations();
        }
    }

    public void SetInteractable(string interactable)
    {
        anim.SetInteractable(interactable);
    }

    public void ExitInteractable()
    {
        anim.ExitInteractable();
    }


    //public void SetIdle()
    //{
    //    m_PlayerState = PlayerState.IDLE;
    //}

    //private void SetAnimations()
    //{
    //    if (controller.velocity.magnitude <= 0.00f)
    //    {
    //        anim.SetMovement(0);
    //    }
    //    else if (controller.velocity.magnitude >= 0.1f)
    //    {
    //        print("a");
    //        anim.SetMovement(controller.velocity.magnitude);
    //    }
    //}

    public void PlayerWakeUpPos()
    {
        controller.enabled = false;
        transform.position = m_PlayerWakeUp.position;
        transform.rotation = m_PlayerWakeUp.rotation;
        controller.enabled = true;
    }

    public void PlayerSleepPos()
    {
        controller.enabled = false;
        transform.position = m_PlayerSleep.position;
        transform.rotation = m_PlayerSleep.rotation;
        controller.enabled = true;
    }
}
