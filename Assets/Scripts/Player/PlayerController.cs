using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform playerWakeUp;
    public Transform playerSleep;

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
        PlayerSleepPos();
    }
    public void PlayerWakeUpPos()
    {
        
        FMODUnity.RuntimeManager.PlayOneShot("event:/Env/BedGetUp", transform.position);
        character.enabled = false;
        sleep = false;
        GameManager.GetManager().playerController.playerAnimation.SetAnimation("WakeUp");
        //mov.animator.SetBool("Sleep", sleep);

        transform.SetPositionAndRotation(playerWakeUp.position, playerWakeUp.rotation);
        character.enabled = true;
    }

    public void PlayerSleepPos()
    {
        character.enabled = false;
        sleep = true;
        transform.SetPositionAndRotation(playerSleep.position, playerSleep.rotation);
        GameManager.GetManager().playerController.playerAnimation.SetAnimation("Sleep");
        // mov.prop.transform.rotation = Quaternion.identity;
        //player.animator.SetBool("Sleep", sleep);
        character.enabled = true;
    }

    public void SadMoment()
    {
        //player.animator.Play("Sad");
    }

    public void HappyMoment()
    {
    //    player.animator.Play("Happy");
    }
}