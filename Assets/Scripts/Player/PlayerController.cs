using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    //private CharacterController controller;
    //public float movementSpeed = 2f;
    //public PlayerAnimations anim;
    //public PlayerState m_PlayerState;
    //public bool xAxisInverted;
    //public bool yAxisInverted;
    //public Transform m_PlayerWakeUp;
    //public Transform m_PlayerSleep;
    //private float m_VerticalSpeed = 1;
    //private bool m_OnGround;
    //private float turnSmoothTime = 0.15f;
    //private float turnSmoothVel;
    //public enum PlayerState
    //{
    //    SLEEP = 0,
    //    IDLE,
    //    MOVE,
    //    COMPUTER
    //}

    ////private bool busy;

    //private Vector3 m_NextPos;
    //private float m_Speed;



    //private void Awake()
    //{
    //    GameManager.GetManager().PlayerController = this;
    //}
    //private void Start()
    //{
    //    controller = this.GetComponent<CharacterController>();
    //    anim = this.GetComponent<PlayerAnimations>();
    //    //SetIdle();
    //}
    //private void Move()
    //{

    //    float horizontal = Input.GetAxis("Horizontal") * (xAxisInverted ? -1.0f : 1.0f);
    //    float vertical = Input.GetAxis("Vertical") * (yAxisInverted ? -1.0f : 1.0f);
    //    Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

    //    if (direction.magnitude >= 0.1f)
    //    {
    //        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
    //        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVel, turnSmoothTime);
    //        transform.rotation = Quaternion.Euler(0f, angle, 0f);

    //        Vector3 movement = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
    //        //movement.y = m_VerticalSpeed * Time.deltaTime;
    //        //Gravity(movement);
    //        controller.Move(movement.normalized * movementSpeed * Time.deltaTime);
    //    }

    //    //Vector3 keyInput = new Vector3(Input.GetAxis("Horizontal")*(xAxisInverted ? -1.0f : 1.0f), 0, Input.GetAxis("Vertical") * (yAxisInverted ? -1.0f : 1.0f)); //est� la c�mara mirando desde un lado
    //    //Vector3 l_movement = (transform.TransformDirection(keyInput) * movementSpeed) * Time.deltaTime;



    //    //l_movement.y = m_VerticalSpeed * Time.deltaTime;
    //    //m_VerticalSpeed += Physics.gravity.y * Time.deltaTime;

    //    ////controller.Move(l_movement);
    //    //Gravity(l_movement);
    //    //Update animation(if so)

    //    //if (m_PlayerState != PlayerState.IDLE && keyInput == Vector3.zero)
    //    //    m_PlayerState = PlayerState.IDLE;
    //    //else if (m_PlayerState != PlayerState.MOVE)
    //    //    m_PlayerState = PlayerState.MOVE;
    //}

    //private void Gravity(Vector3 movement)
    //{
    //    CollisionFlags l_CollisionFlags = controller.Move(movement);
    //    if ((l_CollisionFlags & CollisionFlags.Below) != 0)
    //    {
    //        m_OnGround = true;
    //        m_VerticalSpeed = 0.0f;
    //    }
    //    else
    //        m_OnGround = false;
    //    if ((l_CollisionFlags & CollisionFlags.Above) != 0 && m_VerticalSpeed > 0.0f)
    //        m_VerticalSpeed = 0.0f;

    //}
    //private void Update()
    //{
    //    if (GameManager.GetManager().m_CurrentStateGame == GameManager.StateGame.GamePlay)
    //    {
    //        Move();
    //        if (Input.GetKey(KeyCode.Escape))
    //        {
    //            ExitInteractable();
    //        }
    //        //SetAnimations();
    //    }
    //}

    //public void SetInteractable(string interactable)
    //{
    //    anim.SetInteractable(interactable);
    //}

    //public void ExitInteractable()
    //{
    //    anim.ExitInteractable();
    //}


    ////public void SetIdle()
    ////{
    ////    m_PlayerState = PlayerState.IDLE;
    ////}

    ////private void SetAnimations()
    ////{
    ////    if (controller.velocity.magnitude <= 0.00f)
    ////    {
    ////        anim.SetMovement(0);
    ////    }
    ////    else if (controller.velocity.magnitude >= 0.1f)
    ////    {
    ////        print("a");
    ////        anim.SetMovement(controller.velocity.magnitude);
    ////    }
    ////}

    //public void PlayerWakeUpPos()
    //{
    //    controller.enabled = false;
    //    transform.position = m_PlayerWakeUp.position;
    //    transform.rotation = m_PlayerWakeUp.rotation;
    //    controller.enabled = true;
    //}

    //public void PlayerSleepPos()
    //{
    //    controller.enabled = false;
    //    transform.position = m_PlayerSleep.position;
    //    transform.rotation = m_PlayerSleep.rotation;
    //    controller.enabled = true;
    //}


    public float movementSpeed = 2f;
    public PlayerAnimations anim;
    //public PlayerAnimations anim;
    public Transform m_PlayerWakeUp;
    public Transform m_PlayerSleep;
    public Transform MeshPlayer;
    public GameObject PlayerAsset;

    private Vector3 newPos;
    private bool AImomevent;
    private Vector3 m_Dir;
    private Vector3 m_PlayerDirAxis;

    private GameObject currentSelected;
    [SerializeField] private float minDistance = 0.5f;

    private NavMeshAgent navMeshAgent;
    private Collider col;
    public float speed = 2f;
    public Vector3 zero;

    private bool sleep = true;

    private void Awake()
    {
        GameManager.GetManager().PlayerController = this;
        navMeshAgent = GetComponent<NavMeshAgent>();
        anim = this.GetComponent<PlayerAnimations>();
        col = GetComponent<Collider>();

    }
    private void Start()
    {
        AImomevent = false;
        navMeshAgent.enabled = false;
        col.enabled = false;

       // PlayerSleepPos();

    }

    private void Update()
    {
        if (sleep)
            return;

        if (AImomevent)/*GameManager.GetManager().m_CurrentStateGame == GameManager.StateGame.GamePlay && */
        {
            Desplacement();

            
            //SetAnimations();
        }
        else if (Input.GetKey(KeyCode.Escape))
            ExitInteractable();

        float z = Input.GetAxis("Horizontal");
        float x = Input.GetAxis("Vertical");
        m_PlayerDirAxis = new Vector3(x, 0, -z);

        if (m_PlayerDirAxis != Vector3.zero)
            transform.rotation = Quaternion.LookRotation(m_PlayerDirAxis);

        transform.position += m_PlayerDirAxis * Time.deltaTime;
        transform.position += new Vector3(x, 0, -z) * Time.deltaTime;

        // print(sleep);
    }

    public void ActiveMovement(GameObject interactableObject)
    {
        //*** revisar esta funci�n porque seguramente hay cosas que debo quitar. (ainoa)
        if (sleep)
            return;

        col.enabled = false;

        newPos = interactableObject.transform.position;
        newPos.y = 0;
        currentSelected = interactableObject;
        minDistance = currentSelected.GetComponent<IntfInteract>().GetDistance();
        m_Dir = newPos - transform.position;
        m_Dir.y = 0;
        m_Dir.Normalize();


        navMeshAgent.enabled = true;
        AImomevent = true;
    }

    private void Desplacement()
    {
        Vector3 posPlayer = transform.position;
        posPlayer.y = 0;

        if (Vector3.Distance(posPlayer, newPos) > minDistance)
        {
            navMeshAgent.destination = currentSelected.transform.position - m_Dir * minDistance;

            Quaternion lookDir = Quaternion.LookRotation(m_Dir);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookDir, Time.deltaTime * 2);

            navMeshAgent.destination = currentSelected.transform.position;
        }
        else
        {
            navMeshAgent.enabled = false;
            AImomevent = false;
            currentSelected.GetComponent<IntfInteract>().Interaction();
        }
    }

    public void PlayerWakeUpPos()
    {
        transform.position = m_PlayerWakeUp.position;
        transform.rotation = m_PlayerWakeUp.rotation;
        navMeshAgent.enabled = false;
        sleep = false;
        
        StartCoroutine(DelayCollider());
    }

    public void PlayerStopTrayectory()
    {
        AImomevent = false;
        col.enabled = true;
    }

    public void PlayerSleepPos()
    {
        sleep = true;
        navMeshAgent.enabled = false;
        transform.position = m_PlayerSleep.position;
        transform.rotation = m_PlayerSleep.rotation;

        col.enabled = false;
    }

    IEnumerator DelayCollider()
    {
        yield return new WaitForSeconds(0.1f);
        col.enabled = true;
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