using UnityEngine;

public class Crying : Interactables
{
    public override void ExtraInteraction()
    {
        print("Crying");
        InteractableBlocked = true;
        GameManager.GetManager().dayController.TaskDone();
        if (GameManager.GetManager().dayController.GetDayNumber() == DayController.Day.two)
        {
            GameManager.GetManager().dialogueManager.SetDialogue("D2AccSelfcOcio_Llorar");
            GameManager.GetManager().IncrementInteractableCount();
        }
    }

    public override void EndExtraInteraction()
    {
        print("Exit Crying");
    }
}
