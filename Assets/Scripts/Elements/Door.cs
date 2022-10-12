
using UnityEngine;

public class Door : GeneralActions
{
    [SerializeField] string nameHabDialogue;
    public override void EnterAction()
    {
        GameManager.GetManager().dialogueManager.SetDialogue(nameHabDialogue, canRepeat: true);
        base.EnterAction();
    }

    public override void ExitAction()
    {
        base.ExitAction();
    }
}
