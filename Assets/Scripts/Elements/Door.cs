
using UnityEngine;

public class Door : GeneralActions
{
    [SerializeField] string nameHabDialogue;
    int one;
    public override void EnterAction()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/Env/DoorOpen", transform.position);
        if (one == 0)
        {
            one++;
            GameManager.GetManager().dayController.TaskDone();
        }
        GameManager.GetManager().dialogueManager.SetDialogue(nameHabDialogue, canRepeat: true);
        base.EnterAction();
    }

    public override void ResetObject()
    {
        one = 0;
        base.ResetObject();
    }
    public override void ExitAction()
    {
        base.ExitAction();
    }
}
