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
    private Quaternion targetRotation;
    private bool reset;

    [Range(0.1f, 2f)] public float walkSpeed;

    // Start is called before the first frame update
    void Start()
    {
        animator = this.GetComponent<Animator>();
        i = 0;
        reset = true;
        animator.SetBool("walking", true);
    }

    // Update is called once per frame
    void Update()
    {
        if(animator.GetCurrentAnimatorStateInfo(0).IsName("sitting")) return;
        if (reset)
        {
            // Calculate new random position
            float xDist = Random.Range(-5.0f, 5.0f);
            float zDist = Random.Range(-5.0f, 5.0f);
            newPos = new Vector3(xDist, this.transform.position.y, zDist);
            targetRotation = Quaternion.LookRotation(newPos - this.transform.position);
            reset = false;
            animator.SetBool("walking", true);
        }
        else
        {
            if(animator.GetCurrentAnimatorStateInfo(0).IsName("idle"))
                reset = true;
            
            this.transform.position = Vector3.MoveTowards(this.transform.position, newPos, walkSpeed * Time.deltaTime);
            this.transform.localRotation = Quaternion.Slerp(this.transform.rotation, targetRotation, turningRate * Time.deltaTime);

            if (Vector3.Distance(transform.position, newPos) < .2f)
            {
                Miau();
            }
        }
        Debug.DrawLine(this.transform.position, newPos, Color.white);
    }

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("OnCollisionEnter");
        if(other.gameObject.layer != 9)
        {
            Debug.Log("Collision");
            Miau();
        }
    }

    private void Miau()
    {
        animator.SetBool("walking", false);
        animator.ResetTrigger("hasArrived");
        animator.SetTrigger("hasArrived");
    }
}
