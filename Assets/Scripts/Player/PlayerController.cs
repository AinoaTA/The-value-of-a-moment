using UnityEngine;
using UnityEngine.AI;


public class PlayerController : MonoBehaviour
{
    //private CharacterController controller;
    public float movementSpeed = 2f;
    public PlayerAnimations anim;
    public Transform m_PlayerWakeUp;
    public Transform m_PlayerSleep;
    public Transform MeshPlayer;

    //private Collider coll;
    private Rigidbody rb;
    private Vector3 newPos;
    private bool movement;
    private Vector3 m_Dir;

    private GameObject currentSelected;
    [SerializeField]private float minDistance=0.5f;

    private NavMeshAgent navMeshAgent;
    public float speed = 2f;

  
    public Vector3 zero;

    private void Awake()
    {
        GameManager.GetManager().SetPlayer(this);
        navMeshAgent = GetComponent<NavMeshAgent>();
        //coll = GetComponent<Collider>();
        //rb = GetComponent<Rigidbody>();
    }
    private void Start()
    {
        navMeshAgent.enabled = false;
         //controller = this.GetComponent<CharacterController>();
         anim = this.GetComponent<PlayerAnimations>();
        PlayerSleepPos();
        //SetIdle();
    }
   
    private void Update()
    {
        
        if (GameManager.GetManager().m_CurrentStateGame == GameManager.StateGame.GamePlay && movement)
        {
            Desplacement();
        }

        
    }

    public void ActiveMovement(GameObject interactableObject) 
    {
       

        newPos = interactableObject.transform.position;
        newPos.y = 0;
        movement = true;
        currentSelected = interactableObject;
        minDistance = currentSelected.GetComponent<Iinteract>().GetDistance();
        m_Dir = newPos - transform.position;
        m_Dir.y = 0;
        m_Dir.Normalize();
    }
    
    private void Desplacement() 
    {
        Vector3 posPlayer = transform.position;
        posPlayer.y = 0;

        if (Vector3.Distance(posPlayer, newPos)> minDistance)
        {
            ///l_Player - l_DirectionToPlayer * m_MinDistanceToAttack;
            navMeshAgent.destination = currentSelected.transform.position - m_Dir*minDistance;
            
        }
        else
        {
            movement = false;
            currentSelected.GetComponent<Iinteract>().Interaction();
        }
    }

    public void PlayerWakeUpPos()
    {
        //coll.enabled = true;
        //rb.isKinematic = false;
        //controller.enabled = false;
        navMeshAgent.enabled = true;
        transform.position = m_PlayerWakeUp.position;
        transform.rotation = m_PlayerWakeUp.rotation;
        //controller.enabled = true;


    }

    public void PlayerStopTrayectory() { movement = false; }

    public void PlayerSleepPos()
    {

        //controller.enabled = false;
        navMeshAgent.enabled = false;
        transform.position = m_PlayerSleep.position;
        transform.rotation = m_PlayerSleep.rotation;
        //controller.enabled = true;

        //coll.enabled = false;
        //rb.isKinematic = true;
    }
}
