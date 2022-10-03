using UnityEngine;

public class Drum : Interactables
{
    public override void Interaction(int optionNumber)
    {
        base.Interaction(optionNumber);
        switch (optionNumber)
        {
            case 1:
                GameManager.GetManager().gameStateController.ChangeGameState(2);
                // Inicia minijuego
                GameManager.GetManager().cameraController.StartInteractCam(nameInteractable);
                GameManager.GetManager().canvasController.UnLock();
                break;
        }
    }

    public override void ExitInteraction()
    {
        GameManager.GetManager().StartThirdPersonCamera();
        base.ExitInteraction();
    }
}
