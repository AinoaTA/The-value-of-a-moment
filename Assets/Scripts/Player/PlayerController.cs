using UnityEngine;

//[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public PlayerAnimations anim;
    public Transform playerWakeUp;
    public Transform playerSleep;

    private PlayerMovement mov;
    private bool sleep;

    private CharacterController character;

    private void Awake()
    {
        GameManager.GetManager().playerController = this;
        anim = this.GetComponent<PlayerAnimations>();
        mov = GetComponent<PlayerMovement>();
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
        mov.animator.SetBool("Sleep", sleep);

        transform.SetPositionAndRotation(playerWakeUp.position, playerWakeUp.rotation);
        character.enabled = true;
    }

    public void PlayerSleepPos()
    {
        character.enabled = false;
        sleep = true;
        transform.SetPositionAndRotation(playerSleep.position, playerSleep.rotation);
       // mov.prop.transform.rotation = Quaternion.identity;
        mov.animator.SetBool("Sleep", sleep);
        character.enabled = true;
    }

    public void SetInteractable(string interactable)
    {
        anim.SetInteractable(interactable);
    }

    public void ExitInteractable()
    {
        print("Se está llamado");
        //if (GameManager.GetManager().m_CurrentStateGame != GameManager.StateGame.GamePlay)
        //{
            //anim.ExitInteractable();
        //}
    }

    public void SadMoment()
    {
        mov.animator.Play("Sad");
    }

    public void HappyMoment()
    {
        mov.animator.Play("Happy");
    }
}