using UnityEngine;
using UnityEngine.AI;


public class PlayerController : MonoBehaviour
{
    public float movementSpeed = 2f;
    public PlayerAnimations anim;
    public Transform m_PlayerWakeUp;
    public Transform m_PlayerSleep;
    public Transform MeshPlayer;
    public GameObject PlayerAsset;

    private Vector3 newPos;
    private bool movement;
    private Vector3 m_Dir;

    private GameObject currentSelected;
    [SerializeField] private float minDistance = 0.5f;

    private NavMeshAgent navMeshAgent;
    public float speed = 2f;
    public Vector3 zero;

    private bool sleep=true;

    private void Awake()
    {
        GameManager.GetManager().SetPlayer(this);
        
    }
    private void Start()
    {
        movement = false;
           navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.enabled = false;
        anim = this.GetComponent<PlayerAnimations>();
        PlayerSleepPos();
    }

    private void Update()
    {
        print(navMeshAgent.pathStatus);
        if (movement)/*GameManager.GetManager().m_CurrentStateGame == GameManager.StateGame.GamePlay && */
        {
            Desplacement();
        }


        if (sleep)
            return;
        float z = Input.GetAxis("Horizontal");
        float x = Input.GetAxis("Vertical");

        transform.position += new Vector3(x,0 ,-z)*Time.deltaTime;
    }

    public void ActiveMovement(GameObject interactableObject)
    {
        if (sleep)
            return;
        newPos = interactableObject.transform.position;
        newPos.y = 0;
        currentSelected = interactableObject;
        minDistance = currentSelected.GetComponent<Iinteract>().GetDistance();
        m_Dir = newPos - transform.position;
        m_Dir.y = 0;
        m_Dir.Normalize();


        navMeshAgent.enabled = true;
        movement = true;
    }

    private void Desplacement()
    {
        Vector3 posPlayer = transform.position;
        posPlayer.y = 0;

        if (Vector3.Distance(posPlayer, newPos) > minDistance)
        {
            navMeshAgent.destination = currentSelected.transform.position - m_Dir * minDistance;
        }
        else
        {
            navMeshAgent.enabled = false;
            movement = false;
            currentSelected.GetComponent<Iinteract>().Interaction();
        }
    }

    public void PlayerWakeUpPos()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        transform.position = m_PlayerWakeUp.position;
        transform.rotation = m_PlayerWakeUp.rotation;
        sleep = false;

    }

    public void PlayerStopTrayectory() { movement = false; }

    public void PlayerSleepPos()
    {
        sleep = true;
        navMeshAgent.enabled = false;
        transform.position = m_PlayerSleep.position;
        transform.rotation = m_PlayerSleep.rotation;

    }
}
