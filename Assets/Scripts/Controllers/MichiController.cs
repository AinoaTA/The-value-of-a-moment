using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class MichiController : MonoBehaviour
{
    [SerializeField] private Cuenco cuenco;
    [SerializeField] private Animator animator;
    private NavMeshPath path;
    private NavMeshAgent navMeshAgent;
    private Vector3 cuencoPosition, newPos;
    private bool reset, petting, sitting;
    public bool theresFood;
    private float initialSpeed;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        path = new NavMeshPath();
        reset = true;
        theresFood = false;
        initialSpeed = navMeshAgent.speed;
        animator.SetBool("walking", true);
        if (cuenco) cuencoPosition = cuenco.gameObject.transform.position;
    }

    void Update()
    {
        if (sitting || petting) return;
        if (reset)
        {
            reset = false;
            navMeshAgent.isStopped = false;
            newPos = RandomNavmeshLocation(20f);
            NavMesh.CalculatePath(transform.position, newPos, NavMesh.AllAreas, path);
            navMeshAgent.path = path;
            animator.SetBool("walking", true);
        }
        else if (!theresFood && navMeshAgent.remainingDistance < .2f)
        {
            Miau();
        }

        if (theresFood)
        {
            NavMesh.CalculatePath(transform.position, cuencoPosition, NavMesh.AllAreas, path);
            navMeshAgent.path = path;
            navMeshAgent.speed = 0.5f;
            if (navMeshAgent.remainingDistance < 0.5f)
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
        Vector3 finalPosition = Vector3.zero;
        if (NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, radius, 1))
        {
            finalPosition = hit.position;
        }
        return finalPosition;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer != 2)
        {
            //Miau();
        }
    }

    public void Miau()
    {
        sitting = true;
        navMeshAgent.ResetPath();
        navMeshAgent.isStopped = true;
        animator.SetBool("walking", false);
        animator.ResetTrigger("hasArrived");
        animator.SetTrigger("hasArrived");
        StartCoroutine(Sitting());
    }

    public void UnpetMichi()
    {
        reset = true;
        petting = false;
        sitting = false;
    }

    public void PetMichi()
    {
        Miau();
        petting = true;
        navMeshAgent.isStopped = true;
    }

    IEnumerator Sitting()
    {
        int seconds = Random.Range(5, 25);
        yield return new WaitForSecondsRealtime(seconds);
        animator.SetBool("walking", true);
        reset = true;
        sitting = false;
    }

    public void FeedMichi()
    {
        theresFood = true;
        navMeshAgent.ResetPath();
        reset = false;
    }

    private void OnDrawGizmos()
    {
        if (path == null) return;
        Gizmos.color = Color.magenta;
        for (int i = 0; i < path.corners.Length - 1; i++)
            Gizmos.DrawLine(path.corners[i], path.corners[i + 1]);
    }
}
