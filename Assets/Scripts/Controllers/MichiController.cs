using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class MichiController : MonoBehaviour
{
    private bool theresFood = false;
    [SerializeField] private Cuenco cuenco;
    [SerializeField] private Animator animator;
    private Vector3 cuencoPosition;
    public Vector3 newPos;
    private float turningRate = 3f;
    private Quaternion targetRotation;
    private bool reset, petting;
    private NavMeshAgent navMeshAgent;
    private float initialSpeed;
    [Range(0.1f, 2f)] public float walkSpeed;
    public Transform poses;
    List<Transform> allPoses = new List<Transform>();
    [SerializeField] int currentIndexPose;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        reset = true;
        initialSpeed = navMeshAgent.speed;
        animator.SetBool("walking", true);
        if (cuenco) cuencoPosition = cuenco.gameObject.transform.position;

        for (int i = 0; i < poses.childCount; i++)
        {
            allPoses.Add(poses.GetChild(i));
        }
    }

    void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("sitting") || petting) return;
        if (reset)
        {
            reset = false;
            //animator.Play("Walk");
            newPos = RandomNavmeshLocation(20f);
            navMeshAgent.SetDestination(newPos);
            navMeshAgent.isStopped = false;
            animator.SetBool("walking", true);
        }
        else if (!theresFood)
        {
            //if (animator.GetCurrentAnimatorStateInfo(0).IsName("idle"))
            //    reset = true;

            //this.transform.position = Vector3.MoveTowards(this.transform.position, newPos, walkSpeed * Time.deltaTime);
            //this.transform.localRotation = Quaternion.Slerp(this.transform.rotation, targetRotation, turningRate * Time.deltaTime);

            if (Vector3.Distance(transform.position, newPos) < .2f)
            {
                Miau();
            }
        }

        if (theresFood)
        {
            navMeshAgent.SetDestination(cuencoPosition);
            navMeshAgent.speed = 0.5f;
            //transform.position = Vector3.MoveTowards(transform.position, cuencoPosition, walkSpeed * Time.deltaTime);
            if (Vector3.Distance(transform.position, cuencoPosition) < .1f)
            {
                navMeshAgent.speed = initialSpeed;
                Miau();
                // TODO: comer
                FMODUnity.RuntimeManager.PlayOneShot("event:/NPCs/Cat/Eat", transform.position);
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

    public void Miau()
    {
        navMeshAgent.isStopped = true;
        animator.SetBool("walking", false);
        Debug.Log(false);
        animator.ResetTrigger("hasArrived");
        animator.SetTrigger("hasArrived");
        StartCoroutine(Sitting());
    }

    public void Walk()
    {
        RenudarMichi();
        animator.Play("walk");
        petting = false;
    }
    public void PetMichi()
    {
        Miau();
        animator.Play("sit");
        petting = true;
        navMeshAgent.isStopped = true;
    }
    IEnumerator Sitting()
    {
        int seconds = Random.Range(2, 5);
        yield return new WaitForSecondsRealtime(seconds);
        animator.SetBool("walking", true);
        reset = true;
    }

    public void FeedMichi()
    {
        theresFood = true;
        reset = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(transform.position, newPos);
    }


    public Transform GetNewPoint()
    {
        int rnd = Random.Range(0, allPoses.Count);
        return allPoses[rnd];
    }

    public void RenudarMichi() 
    {
        reset = true;
        animator.Play("Walk");
        newPos = GetNewPoint().position;
        navMeshAgent.SetDestination(newPos);

    }
}
