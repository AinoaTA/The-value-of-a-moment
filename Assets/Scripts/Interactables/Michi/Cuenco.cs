using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cuenco : Interactables
{
    public MichiController michiController;
    public GameObject comida;
    private bool hasPienso;

    void Start()
    {
        ResetCuenco();
    }

    public override void Interaction(int options)
    {
        base.Interaction(options);
        switch (options)
        {
            case 1:
                if (hasPienso)
                {
                    comida.SetActive(true);
                    hasPienso = false;
                    InteractableBlocked = true;
                    michiController.FeedMichi();
                    GameManager.GetManager().gameStateController.ChangeGameState(2);
                    GameManager.GetManager().cameraController.StartInteractCam(nameInteractable);
                }
                break;
        }
    }
    
    public override void ExitInteraction()
    {
        GameManager.GetManager().StartThirdPersonCamera();
        base.ExitInteraction();
    }

    public void GrabbedPienso()
    {
        hasPienso = true;
        // Activate canvas options
        InteractableBlocked = false;
    }

    public void ResetCuenco()
    {
        InteractableBlocked = true;
        hasPienso = false;
        comida.SetActive(false);
    }
}
