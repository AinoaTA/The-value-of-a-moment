using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform playerGetUp;
    public Transform playerSleep;
    public float rotationSleep = 90;
    public float rotationWakeup = 90;
    private bool sleep;
    public Transform root;

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

        //FMODUnity.RuntimeManager.PlayOneShot("event:/Env/Bed/GetUp", transform.position);
        character.enabled = false;
        sleep = false;
        playerAnimation.SetAnimation("GetUp");
        //Sleep(true);
        StartCoroutine(StartDay());
        character.enabled = true;
    }

    public void PlayerSleepPos()
    {
        sleep = true;
        Sleep(false);
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

    void Sleep(bool wakeup)
    {
        root.rotation = Quaternion.identity;
        //if (wakeup)
        //    root.SetPositionAndRotation(playerGetUp.position, Quaternion.Euler(0, rotationWakeup, 0));
        //else
        if (!wakeup)
            root.SetPositionAndRotation(playerSleep.position, Quaternion.Euler(0, rotationWakeup, 0));



        //transform.SetPositionAndRotation(playerSleep.position, Quaternion.Euler(rotationSleep, 0, rotationSleep));
    }
    [SerializeField] float t = 0;
    IEnumerator StartDay()
    {
        //QUE ASCO LE ESTOY COGIENDO A LAS ANIMACIONES HELP ME
       
        Vector3 prevPos = root.localPosition;
        float provY = root.localPosition.y;
        float time = 3.5f;
        while (t < time)
        {
            t += Time.deltaTime;
            root.localPosition = Vector3.Lerp(prevPos, new Vector3(0, provY,-1.0f), t/ time);
            yield return null;
        }
        yield return new WaitForSeconds(1.5f);
        t = 0;
        prevPos = root.localPosition;
        time = 1;
        while (t < time)
        {
            t += Time.deltaTime;
            root.localPosition = Vector3.Lerp(prevPos, new Vector3(0, -1.2f,0), t/time);
            yield return null;
        }

        yield return new WaitForSeconds(4f);
        GameManager.GetManager().StartThirdPersonCamera();
    }
}