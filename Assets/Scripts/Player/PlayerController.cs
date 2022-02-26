using System.Collections;
using UnityEngine;
using UnityEngine.AI;


public class PlayerController : MonoBehaviour
{
    public float movementSpeed = 2f;
    //public PlayerAnimations anim;
    public Transform m_PlayerWakeUp;
    public Transform m_PlayerSleep;
    public Transform MeshPlayer;
    public GameObject PlayerAsset;

    private Vector3 newPos;
    private bool movement;
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
        GameManager.GetManager().SetPlayer(this);
        col = GetComponent<Collider>();

    }
    private void Start()
    {
        movement = false;
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.enabled = false;
        //anim = GetComponent<PlayerAnimations>();
        col.enabled = false;

        PlayerSleepPos();
    }

    private void Update()
    {
        if (movement)/*GameManager.GetManager().m_CurrentStateGame == GameManager.StateGame.GamePlay && */
            Desplacement();

        if (sleep)
            return;

        float z = Input.GetAxis("Horizontal");
        float x = Input.GetAxis("Vertical");
        m_PlayerDirAxis = new Vector3(x, 0, -z);

        if (m_PlayerDirAxis != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(m_PlayerDirAxis);
        }

        transform.position += m_PlayerDirAxis * Time.deltaTime;

    }

    public void ActiveMovement(GameObject interactableObject)
    {
        //*** revisar esta función porque seguramente hay cosas que debo quitar. (ainoa)
        if (sleep)
            return;

        col.enabled = false;

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

            Quaternion lookDir = Quaternion.LookRotation(m_Dir);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookDir, Time.deltaTime * 2);

            navMeshAgent.destination = currentSelected.transform.position;
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

        StartCoroutine(DelayCollider());
    }

    public void PlayerStopTrayectory()
    {
        movement = false;
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
}
