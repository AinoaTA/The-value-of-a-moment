using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crying : Interactables
{
    public override void ExtraInteraction()
    {
        print("Crying");
    }

    public override void EndExtraInteraction()
    {
        print("Exit Crying");
    }
}
