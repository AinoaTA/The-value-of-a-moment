using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Vector2 m_MovementAxis;
    public Vector2 MovementAxis { get { return m_MovementAxis.normalized; } set { m_MovementAxis = value; } }
    private Vector3 m_Direction;
    private Camera cam;
    [SerializeField] private float speed, maxSpeed = 1.4f;/*, stopSpeedOffset = 0.2f;*/
    public GameObject prop;
    //bool moving;

    //[SerializeField] float m_LerpRotationPercentatge = 0.2f;
    [SerializeField] float m_CurrVelocityPlayer;
    CharacterController m_CharacterController;

    // public Animator animator;
    private void Awake()
    {
        cam = Camera.main;
    }
    private void Start()
    {
        speed = maxSpeed;
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
        GameManager.GetManager().playerInputs._MoveUp -= MoveUp;
        GameManager.GetManager().playerInputs._MoveDown -= MoveDown;
        GameManager.GetManager().playerInputs._MoveLeft -= MoveLeft;
        GameManager.GetManager().playerInputs._MoveRight -= MoveRight;
        GameManager.GetManager().playerInputs._ResetMove -= ResetMove;
        GameManager.GetManager().playerInputs._StopMoving -= StopMoving;
    }
    private void ResetMove()
    {
        m_MovementAxis = Vector2.zero;
    }
    private void MoveLeft()
    {
        m_MovementAxis += new Vector2(-1, 0);
        //moving = true;
    }

    private void MoveRight()
    {
        m_MovementAxis += new Vector2(1, 0);
        //moving = true;
    }

    private void MoveUp()
    {
        m_MovementAxis += new Vector2(0, 1);
        //moving = true;
    }

    private void MoveDown()
    {
        m_MovementAxis += new Vector2(0, -1);
        //moving = true;
    }

    private void StopMoving()
    {
        m_MovementAxis = Vector2.zero;
        //moving = false;
    }

    void Update()
    {
        if (!GameManager.GetManager().gameStateController.CheckGameState(1))
        {
            //m_CurrVelocityPlayer = 0;
            //m_Anim.SetFloat("Speed", Mathf.Clamp(m_CurrVelocityPlayer, 0, 1));
            return;
        }
        if (m_MovementAxis != Vector2.zero)
        {
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
            Vector3 movement = m_Direction * Time.deltaTime * speed;

            CollisionFlags m_CollisionFlags = m_CharacterController.Move(movement);
        }
    }
}
