public class Book : Interactables
{
    private int m_Counter = 0;
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
                    FMODUnity.RuntimeManager.PlayOneShot("event:/Env/Book", transform.position);
                    m_Grabbing.SetAccessCamera(true);
                    GameManager.GetManager().cameraController.StartInteractCam(8);
                    //GameManager.GetManager().PlayerController.SetInteractable("Grab");
                    //GameManager.GetManager().m_CurrentStateGame = GameManager.StateGame.GamePlay;
                    //HideCanvas();

                    //if (m_Counter >= m_InteractPhrases.Length)
                    //    m_Counter = 0;

                    //StartCoroutine(DelayDialogue());
                }
                break;
        }
    }
}
