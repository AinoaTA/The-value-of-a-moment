using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Corcho : Interactables
{
    public override void Interaction(int options)
    {
        Debug.Log(options);
        base.Interaction(options);
        switch (options)
        {
            case 1:
                GameManager.GetManager().gameStateController.ChangeGameState(2);
                GameManager.GetManager().cameraController.StartInteractCam(9);
                break;
            case 2:
                break;
        }
    }
}
