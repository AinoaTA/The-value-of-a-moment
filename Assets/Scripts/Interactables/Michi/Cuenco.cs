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
                    if (GameManager.GetManager().dayController.GetDayNumber() == DayController.Day.two)
                    {
                        GameManager.GetManager().dialogueManager.SetDialogue("D2AccMino_Alimentar");
                        GameManager.GetManager().IncrementInteractableCount();
                    }
                    comida.SetActive(true);
                    hasPienso = false;
                    InteractableBlocked = true;
                    michiController.FeedMichi();
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
