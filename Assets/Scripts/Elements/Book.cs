public class Book : Interactables
{
    public string[] m_BookInteractPhrases;
    public Grabbing m_Grabbing;

    public delegate void DelegateSFXBook();
    public static DelegateSFXBook m_DelegateSFXBook;

    public override void Interaction(int options)
    {
        base.Interaction(options);
        switch (options)
        {
            case 1:
                if (m_Grabbing != null)
                {
                    FMODUnity.RuntimeManager.PlayOneShot("event:/Env/Book Pickup", transform.position);
                    m_Grabbing.SetAccessCamera(true);
                    GameManager.GetManager().cameraController.StartInteractCam(8);
                }
                break;
        }
    }
}
