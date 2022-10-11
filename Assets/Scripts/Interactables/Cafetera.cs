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
                // gameInitialized = true;
                // Inicia minijuego
                GameManager.GetManager().cameraController.StartInteractCam(nameInteractable);
                // GameManager.GetManager().canvasController.UnLock();
                GameManager.GetManager().dialogueManager.SetDialogue("ITomarCafe");
                
                break;
        }
    }

    public override void ExitInteraction()
    {
       // gameInitialized = false;
        GameManager.GetManager().StartThirdPersonCamera();
        base.ExitInteraction();
    }
}
