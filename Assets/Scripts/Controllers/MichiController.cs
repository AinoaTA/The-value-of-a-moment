using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MichiController : MonoBehaviour
{
    // public Pathfinding pathfinding;
    private Animator animator;
    private string path;
    private int i;
    public Vector3 newPos;
    private float turningRate = 3f;

    [Range(0.1f, 2f)] public float walkSpeed;

    // Start is called before the first frame update
    void Start()
    {
        animator = this.GetComponent<Animator>();
        i = 0;
        path = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (path == null)   // Create new path
        {
            if(i != 0 && !animator.GetAnimatorTransitionInfo(0).IsName("miau -> idle")) return;
            // Calculate new random position
            float xDist = Random.Range(-10.0f, 10.0f);
            float zDist = Random.Range(-10.0f, 10.0f);

            newPos = new Vector3(xDist, this.transform.position.y,zDist);
            path = "dfa";
            // Debug.Log("michi newpos: " + newPos);

            animator.SetBool("walking", true);
        }
        else
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, newPos, walkSpeed * Time.deltaTime);
            // this.transform.LookAt(newPos);
            var targetRotation = Quaternion.LookRotation(newPos - this.transform.position);
            this.transform.localRotation = Quaternion.Slerp(this.transform.rotation, targetRotation, turningRate * Time.deltaTime);

            if (Vector3.Distance(transform.position, newPos) < 2f)
            {
                Debug.Log("MICHI has arrived");
                path = null;
                animator.SetBool("walking", false);
                animator.SetTrigger("hasArrived");
            }
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.layer != 9)
        {
            animator.SetBool("walking", false);
            animator.SetTrigger("hasArrived");
            Debug.Log("Michi collided with " + other.gameObject);
            path = null;
        }
    }
}
