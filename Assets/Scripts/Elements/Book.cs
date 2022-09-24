public class Book : Interactables
{
    public string[] m_BookInteractPhrases;
    public Grabbing grabbing;

    public override void Interaction(int options)
    {
        base.Interaction(options);
        switch (options)
        {
            case 1:
                if (grabbing != null)
                {
                    grabbing.SetAccessCamera(true);
                    GameManager.GetManager().cameraController.StartInteractCam(8);
                }
                break;
        }
    }
}
