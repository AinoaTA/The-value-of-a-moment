using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MichiController))]
public class Michi : Interactables
{
    private MichiController controller;

    private void Start()
    {
        controller = this.GetComponent<MichiController>();
    }

    public override void Interaction(int options)
    {
        base.Interaction(options);
        switch (options)
        {
            case 1:
                FMODUnity.RuntimeManager.PlayOneShot("event:/NPCs/Cat/Pet");
                GameManager.GetManager().gameStateController.ChangeGameState(2);
                GameManager.GetManager().cameraController.StartInteractCam(nameInteractable);
                controller.PetMichi();
                break;
            case 2:
                break;
        }
    }
    
    public override void ExitInteraction()
    {
        GameManager.GetManager().StartThirdPersonCamera();
        base.ExitInteraction();
    }
}
