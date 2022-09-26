using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sit : GeneralActions
{
    #region Extra Actions
    public DoSomething[] moreOptions;
    [System.Serializable]
    public struct DoSomething
    {
        public string name;
        public string animationName;
        public Interactables interaction;
        //dialogues xdxd

    }


    void StartExtraInteraction(int id)
    {//example
        moreOptions[id].interaction.Interaction(1);
    }


    #endregion
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
