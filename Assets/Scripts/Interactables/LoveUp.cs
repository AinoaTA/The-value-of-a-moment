using UnityEngine;

public class LoveUp : Interactables
{
    public override void ExtraInteraction()
    {
        print("LevelUp");
    }

    public override void EndExtraInteraction()
    {
        base.EndExtraInteraction();
    }
}
