using UnityEngine;

public class LoveUp : Interactables
{
    int counter;
    public override void ExtraInteraction()
    {
        GameManager.GetManager().dialogueManager.SetDialogue("ISmartphoneTinder");
        counter++;
        print("LevelUp");
    }

    public override void EndExtraInteraction()
    {
        base.EndExtraInteraction();
    }
}
