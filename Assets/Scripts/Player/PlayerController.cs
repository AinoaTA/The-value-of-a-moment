using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform playerGetUp;
    public Transform playerSleep;
    public float rotationSleep = 90;
    public float rotationWakeup = 90;
    private bool sleep;

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
        playerAnimation.SetAnimation("WakeUp");
        Sleep(true);
        character.enabled = true;
    }

    public void PlayerSleepPos()
    {
        sleep = true;
        Sleep(false);
        playerAnimation.SetAnimation("Sleep");
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
        transform.rotation = Quaternion.identity;
        if (wakeup)
            transform.SetPositionAndRotation(playerGetUp.position, Quaternion.Euler(0, rotationWakeup, 0));
        else
            transform.SetPositionAndRotation(playerSleep.position, Quaternion.Euler(rotationSleep, 0, rotationSleep));
    }
}