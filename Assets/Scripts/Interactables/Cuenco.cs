public class Cuenco : Interactables
{
    public override void Interaction(int options)
    {
        base.Interaction(options);
        switch (options)
        {
            case 1:
                GameManager.GetManager().gameStateController.ChangeGameState(2);
                GameManager.GetManager().cameraController.StartInteractCam(8);
                break;
        }
    }
}
