using UnityEngine;

public class Crying : Interactables
{
    public override void ExtraInteraction()
    {
        print("Crying");
        InteractableBlocked = true;
        GameManager.GetManager().dayController.TaskDone();
    }

    public override void EndExtraInteraction()
    {
        print("Exit Crying");
    }
}
