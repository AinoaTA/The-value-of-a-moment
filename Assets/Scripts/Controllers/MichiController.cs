using UnityEngine;

public class MichiController : MonoBehaviour
{
    private bool theresFood = false;
    public Transform cuenco;
    private Animator animator;
    public Vector3 newPos;
    private float turningRate = 3f;
    private Quaternion targetRotation;
    private bool reset, petting;

    [Range(0.1f, 2f)] public float walkSpeed;

    void Start()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/Env/Cat/Idles", transform.position);
        FMODUnity.RuntimeManager.PlayOneShot("event:/Env/Cat/Purr", transform.position);
        animator = this.GetComponent<Animator>();
        reset = true;
        animator.SetBool("walking", true);
    }

    void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("sitting") || petting) return;
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
        else if(!theresFood)
        {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("idle"))
                reset = true;

            this.transform.position = Vector3.MoveTowards(this.transform.position, newPos, walkSpeed * Time.deltaTime);
            this.transform.localRotation = Quaternion.Slerp(this.transform.rotation, targetRotation, turningRate * Time.deltaTime);

            if (Vector3.Distance(transform.position, newPos) < .2f)
            {
                Miau();
            }
        }

        if(theresFood)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, cuenco.position, walkSpeed * Time.deltaTime);
            if (Vector3.Distance(transform.position, cuenco.position) < .2f)
            {
                Miau();
                // TODO: comer
                theresFood = false;
            }
        }
    }

    private void OnCollisionEnter(Collision other)
    {
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
}
