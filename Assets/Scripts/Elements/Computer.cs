using UnityEngine;

public class Computer : Interactables
{
    public override void Interaction(int options)
    {
        print("hola");
        base.Interaction(options);
        switch (options)
        {
            case 1:
                print("iu");
                GameManager.GetManager().gameStateController.ChangeGameState(2);
                GameManager.GetManager().canvasController.ComputerScreenIn();
                break;
        }
    }

    public override void ExitInteraction()
    {
        GameManager.GetManager().canvasController.ComputerScreenOut();
        GameManager.GetManager().StartThirdPersonCamera();
        base.ExitInteraction();
    }
}
