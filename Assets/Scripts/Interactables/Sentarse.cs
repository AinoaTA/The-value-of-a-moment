using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sentarse : GeneralActions
{
    public override void EnterAction()
    {
        GameManager.GetManager().gameStateController.ChangeGameState(3);
        GameManager.GetManager().cameraController.StartInteractCam(nameAction);
    }

    public override void ExitAction()
    {
        GameManager.GetManager().StartThirdPersonCamera();
        base.ExitAction();
    }
}
