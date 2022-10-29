using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform playerGetUp;
    public Transform playerSleep;
    public float rotationSleep = 90;
    public float rotationWakeup = 90;
    //private bool sleep;
    public Transform root;


    public Transform handBone;
    private CharacterController character;
    public PlayerAnimationController playerAnimation;
    private void Awake()
    {
        GameManager.GetManager().playerController = this;
        character = GetComponent<CharacterController>();
    }
    private void Start()
    {
        character.enabled = false;
        PlayerSleepPos();
    }
    public void PlayerWakeUpPos()
    {
        character.enabled = false;
        //sleep = false;
        playerAnimation.SetAnimation("GetUp");
        GetUp(true);
        StartCoroutine(StartDay());
        character.enabled = true;
    }

    public void PlayerSleepPos()
    {
        //sleep = true;
        GetUp(false);
        playerAnimation.SetAnimation("Sleep");//, true);
    }

    public void SadMoment()
    {
        //player.animator.Play("Sad");
    }

    public void HappyMoment()
    {
        //    player.animator.Play("Happy");
    }

    public void SetAnimation(string name)
    {
        playerAnimation.SetAnimation(name);
    }

    public void SetAnimation(string name, Transform pos)
    {
        playerAnimation.Active(false);
        if (pos != null)
        {
            root.position = pos.position;
            root.localRotation = Quaternion.Euler(0, 180, 0);
        }
        playerAnimation.SetAnimation(name);
    }

    public void TemporalExit()
    {
        root.localPosition = new Vector3(0, -1.2f, 0);
        SetAnimation("Movement");
        root.localRotation = Quaternion.Euler(0, 180, 0);
    }
    void GetUp(bool getUp)
    {
        if (getUp)
        {
            StartCoroutine(GetUpRoutine());
        }
        else
        {
            Vector3 pos = new Vector3(playerGetUp.position.x, transform.position.y,playerGetUp.position.z);
            transform.SetPositionAndRotation(pos, Quaternion.Euler(0, rotationWakeup, 0));
            root.rotation = Quaternion.identity;
            root.SetPositionAndRotation(playerSleep.position, Quaternion.Euler(0, rotationSleep, 0));
        }
    }

    IEnumerator GetUpRoutine()
    {
        float t = 0;
        Quaternion rotInit = root.rotation;
        Vector3 posInit = root.position + new Vector3(0, 0.4f, 0);
        float maxTime = 0.2f;
        while (t < maxTime)
        {
            t += Time.deltaTime;
            root.position = Vector3.Lerp(posInit, playerGetUp.position, t / maxTime);
            root.rotation = Quaternion.Lerp(rotInit, Quaternion.Euler(0, rotationWakeup, 0), t / maxTime);

            yield return null;
        }
    }
    IEnumerator StartDay()
    {
        yield return new WaitForSeconds(4f);
        GameManager.GetManager().StartThirdPersonCamera();
    }

    public void SetPlayerPos(Vector3 pos)
    {
        playerAnimation.transform.position = new Vector3(pos.x, playerAnimation.transform.position.y, pos.z);
    }

    public Vector3 GetPlayerPos()
    {
        return playerAnimation.transform.position;
    }

    public void ResetPlayerPos()
    {
        playerAnimation.transform.position = character.transform.position;
    }

    public void ResetPlayerPos(Vector3 position)
    {
        playerAnimation.transform.position = position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("PuertaAida")) return;

        if (!GameManager.GetManager().checkAida)
        {
            GameManager.GetManager().dialogueManager.SetDialogue("D2PuertAida_RescMino", delegate
            {
                StartCoroutine(ChangeCheckAida());
            });
        }
        else GameManager.GetManager().dialogueManager.SetDialogue("D2EsTarde_PuertAida");
    }

    IEnumerator ChangeCheckAida()
    {
        yield return new WaitForSecondsRealtime(10f);
        GameManager.GetManager().checkAida = true;

        yield return new WaitForSecondsRealtime(10f);
        GameManager.GetManager().dialogueManager.SetDialogue("D2EsTarde");
    }


    public void PlayerMovementAnim(float speed)
    {
        playerAnimation.anim.SetFloat("Speed", speed);
    }

    public void AudioDialogue()
    {
        GameManager.GetManager().dialogueManager.SetDialogue("D2PostCafe", delegate
         {
             GameManager.GetManager().blockController.BlockAll(false);
         });
    }
}