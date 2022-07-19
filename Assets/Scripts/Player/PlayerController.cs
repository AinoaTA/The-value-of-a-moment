using UnityEngine;

//[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public PlayerAnimations anim;
    public Transform PlayerWakeUp;
    public Transform PlayerSleep;

    private Mov mov;
    private bool sleep;

    private CharacterController character;

    private void Awake()
    {
        GameManager.GetManager().PlayerController = this;
        anim = this.GetComponent<PlayerAnimations>();
        mov = GetComponent<Mov>();
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
        mov.Anim.SetBool("Sleep", sleep);

        transform.SetPositionAndRotation(PlayerWakeUp.position, PlayerWakeUp.rotation);
        character.enabled = true;
    }

    public void PlayerSleepPos()
    {
        character.enabled = false;
        sleep = true;
        transform.SetPositionAndRotation(PlayerSleep.position, PlayerSleep.rotation);
       // mov.prop.transform.rotation = Quaternion.identity;
        mov.Anim.SetBool("Sleep", sleep);
        character.enabled = true;
    }

    public void SetInteractable(string interactable)
    {
        anim.SetInteractable(interactable);
    }

    public void ExitInteractable()
    {
        print("Se está llamado");
        //if (GameManager.GetManager().CurrentStateGame != GameManager.StateGame.GamePlay)
        //{
            //anim.ExitInteractable();
        //}
    }

    public void SadMoment()
    {
        mov.Anim.Play("Sad");
    }

    public void HappyMoment()
    {
        mov.Anim.Play("Happy");
    }
}