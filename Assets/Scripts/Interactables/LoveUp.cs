using UnityEngine;

public class LoveUp : Interactables
{
    int counter;
    public override void ExtraInteraction()
    {
        switch (GameManager.GetManager().dayController.GetDayNumber())
        {
            case DayController.Day.one:
                GameManager.GetManager().dialogueManager.SetDialogue("ISmartphoneTinder");
                break;
            case DayController.Day.two:
                GameManager.GetManager().dialogueManager.SetDialogue("D2AccTelef_LoveUp");
                break;
            default: break;
        }

        counter++;
        print("LevelUp");
    }

    public override void EndExtraInteraction()
    {
        base.EndExtraInteraction();
    }
}
