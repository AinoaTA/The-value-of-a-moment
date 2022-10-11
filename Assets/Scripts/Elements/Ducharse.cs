using UnityEngine;

public class Ducharse : Interactables
{
    public override void ExtraInteraction()
    {
        print("Uff ducha");
    }

    public override void EndExtraInteraction()
    {
        print("ya no mas ducha");
    }
}
