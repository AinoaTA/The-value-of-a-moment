using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cafetera : Interactables
{
    public override void Interaction(int optionNumber)
    {
        base.Interaction(optionNumber);
        switch (optionNumber)
        {
            case 1:
                FMODUnity.RuntimeManager.PlayOneShot("event:/Env/Coffee Brew", transform.position);
                GameManager.GetManager().gameStateController.ChangeGameState(2);
                GameManager.GetManager().cameraController.StartInteractCam(nameInteractable);
                GameManager.GetManager().dialogueManager.SetDialogue("ITomarCafe");
                break;
        }
    }

    public override void ExitInteraction()
    {
        GameManager.GetManager().StartThirdPersonCamera();
        base.ExitInteraction();
    }
}
