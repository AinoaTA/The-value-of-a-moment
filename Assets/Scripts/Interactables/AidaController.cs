using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class AidaController : MonoBehaviour
{
    public Transform spawnPos, roomAidaPos;
    NavMeshAgent navMesh;
    public Animator animAida;
    public float maxTime = 10;
    public GameObject doorAida;
    private void Start()
    {
        navMesh = GetComponent<NavMeshAgent>();
        navMesh.enabled = false;
    }

    float t = 0;
    bool timeOut;
    private void Update()
    {
        if (GameManager.GetManager().openAida) return;
        if (t < maxTime)
        {
            t += Time.deltaTime;
            if (t > maxTime)
            {
                timeOut = true;
                StartCoroutine(OpenDoor(false));
            }
        }
    }

    IEnumerator OpenDoor(bool elleOpened)
    {
        transform.position = spawnPos.position;
        navMesh.enabled = true;
        navMesh.SetDestination(spawnPos.position);


        //float distance;
        //Vector3 dir = GameManager.GetManager().playerController.transform.position - transform.position;
        //dir.Normalize();
        navMesh.SetDestination(GameManager.GetManager().playerController.transform.position);
        animAida.Play("Walking");
        yield return new WaitUntil(() => navMesh.pathStatus == NavMeshPathStatus.PathComplete && !navMesh.hasPath);
        if (elleOpened)
        {
            animAida.Play("Idle");
            GameManager.GetManager().dialogueManager.SetDialogue("D2MundoAida",delegate 
            {
                navMesh.SetDestination(roomAidaPos.position);
            });

           
        }

        yield return null;
    }




}
