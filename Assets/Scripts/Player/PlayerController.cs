using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform playerWakeUp;
    public Transform playerSleep;

    private bool sleep;

    private CharacterController character;

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
        character.enabled = false;
        sleep = false;
        GameManager.GetManager().playerAnimationController.SetAnimation("WakeUp");
        //mov.animator.SetBool("Sleep", sleep);

        transform.SetPositionAndRotation(playerWakeUp.position, playerWakeUp.rotation);
        character.enabled = true;
    }

    public void PlayerSleepPos()
    {
        character.enabled = false;
        sleep = true;
        transform.SetPositionAndRotation(playerSleep.position, playerSleep.rotation);
        GameManager.GetManager().playerAnimationController.SetAnimation("Sleep");
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