using UnityEngine;

[RequireComponent(typeof(MichiController))]
public class Michi : Interactables
{
    private MichiController controller;

    private void Start()
    {
        controller = GetComponent<MichiController>();
    }

    public override void Interaction(int options)
    {
        base.Interaction(options);
        switch (options)
        {
            case 1:
                switch (GameManager.GetManager().dayController.GetDayNumber())
                {
                    case DayController.Day.one:
                        GameManager.GetManager().dialogueManager.SetDialogue("IMino");
                        break;
                    case DayController.Day.two:
                        GameManager.GetManager().dialogueManager.SetDialogue("D2AccMino_Acariciar");
                        GameManager.GetManager().IncrementInteractableCount();
                        break;
                    default: break;
                }
                FMODUnity.RuntimeManager.PlayOneShot("event:/NPCs/Cat/Pet", transform.position);
                GameManager.GetManager().gameStateController.ChangeGameState(2);
                GameManager.GetManager().cameraController.StartInteractCam(nameInteractable);
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = true;
                controller.PetMichi();

                break;
            case 2:
                break;
        }
    }

    public override void ExitInteraction()
    {
        controller.Walk();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        GameManager.GetManager().StartThirdPersonCamera();
        base.ExitInteraction();
    }

}
