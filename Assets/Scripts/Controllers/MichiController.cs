using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class MichiController : MonoBehaviour
{
    private bool theresFood = false;
    [SerializeField] private Cuenco cuenco;
    private Vector3 cuencoPosition;
    private Animator animator;
    public Vector3 newPos;
    private float turningRate = 3f;
    private Quaternion targetRotation;
    private bool reset, petting;
    private NavMeshAgent navMeshAgent;

    [Range(0.1f, 2f)] public float walkSpeed;

    void Start()
    {
        navMeshAgent = this.GetComponent<NavMeshAgent>();
        animator = this.GetComponent<Animator>();
        reset = true;
        animator.SetBool("walking", true);
        if(cuenco) cuencoPosition = cuenco.gameObject.transform.position;
    }

    void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("sitting") || petting) return;
        if (reset)
        {
            reset = false;
            animator.SetBool("walking", true);
            newPos = RandomNavmeshLocation(20f);
            navMeshAgent.SetDestination(newPos);
            // Calculate new random position
            //float xDist = Random.Range(-5.0f, 5.0f);
            //float zDist = Random.Range(-5.0f, 5.0f);
            //newPos = new Vector3(xDist, this.transform.position.y, zDist);
            //targetRotation = Quaternion.LookRotation(newPos - this.transform.position);
        }
        else if(!theresFood)
        {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("idle"))
                reset = true;

            //this.transform.position = Vector3.MoveTowards(this.transform.position, newPos, walkSpeed * Time.deltaTime);
            //this.transform.localRotation = Quaternion.Slerp(this.transform.rotation, targetRotation, turningRate * Time.deltaTime);

            if (Vector3.Distance(transform.position, newPos) < .2f)
            {
                Miau();
            }
        }

        if(theresFood)
        {
            Debug.Log("Food");
            this.transform.position = Vector3.MoveTowards(this.transform.position, cuencoPosition, walkSpeed * Time.deltaTime);
            if (Vector3.Distance(transform.position, cuencoPosition) < .1f)
            {
                Miau();
                // TODO: comer
                cuenco.ResetCuenco(); // Desaparecer la comida
                theresFood = false;
            }
        }
    }

    public Vector3 RandomNavmeshLocation(float radius)
    {
        Vector3 randomDirection = Random.insideUnitSphere * radius;
        randomDirection += transform.position;
        NavMeshHit hit;
        Vector3 finalPosition = Vector3.zero;
        if (NavMesh.SamplePosition(randomDirection, out hit, radius, 1))
        {
            finalPosition = hit.position;
        }
        return finalPosition;
    }

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log(other.gameObject.name);
        if (other.gameObject.layer != 2)
        {
            Miau();
        }
    }

    private void Miau()
    {
        animator.SetBool("walking", false);
        animator.ResetTrigger("hasArrived");
        animator.SetTrigger("hasArrived");
        reset = true;
    }

    public void PetMichi()
    {
        Debug.Log("petting");
        Miau();
        petting = true;
    }

    public void FeedMichi()
    {
        Debug.Log("feeding");
        theresFood = true;
        reset = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(transform.position, newPos);
    }
}
