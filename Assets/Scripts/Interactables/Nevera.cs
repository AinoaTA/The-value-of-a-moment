using System.Collections;
using UnityEngine;

public class Nevera : GeneralActions
{
    public override void EnterAction()
    {
        base.EnterAction();
        GameManager.GetManager().gameStateController.ChangeGameState(3);
        GameManager.GetManager().cameraController.StartInteractCam(nameAction);
        GameManager.GetManager().dialogueManager.SetDialogue("IPicarAlgo",
        delegate
        {
            StartCoroutine(Delay());
        }, canRepeat: true);
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(0.5f);
        GameManager.GetManager().dialogueManager.SetDialogue("Ducha");
    }

    public override void ExitAction()
    {
        if (GameManager.GetManager().interactableManager.currInteractable != null)
            GameManager.GetManager().interactableManager.currInteractable.EndExtraInteraction();
        GameManager.GetManager().interactableManager.LookingAnInteractable(null);
        //canvas.SetBool("Showing", false);
        GameManager.GetManager().StartThirdPersonCamera();
        base.ExitAction();
    }
}
