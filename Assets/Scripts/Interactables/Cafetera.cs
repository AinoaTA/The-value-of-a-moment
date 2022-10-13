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
                switch (GameManager.GetManager().dayController.GetDayNumber())
                {
                    case DayController.Day.one:
                        GameManager.GetManager().dialogueManager.SetDialogue("ITomarCafe");
                        break;
                    case DayController.Day.two:
                        GameManager.GetManager().dialogueManager.SetDialogue("D2AccComBeb_Cafetera");
                        GameManager.GetManager().IncrementInteractableCount();
                        break;
                    case DayController.Day.three:
                        break;
                    case DayController.Day.fourth:
                        break;
                    default:
                        break;
                }
                break;
        }
    }

    public override void ExitInteraction()
    {
        GameManager.GetManager().StartThirdPersonCamera();
        base.ExitInteraction();
    }
}
