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
                    GameManager.GetManager().cameraController.StartInteractCam(nameInteractable);
                    if (GameManager.GetManager().dayController.GetDayNumber() == DayController.Day.two)
                    {
                        GameManager.GetManager().dialogueManager.SetDialogue("D2AccSelfcOcio_Libro");
                    }
                }
                break;
        }
    }
    int counter;
    bool block;
    public override void ExtraInteraction()
    {
        block = true;
        GameManager.GetManager().dialogueManager.SetDialogue("D2AccSelfcOcio_Libro",delegate { block = false; },canRepeat:true);
        counter++;
        
    }

    public override void ExitInteraction()
    {
        if (block) return;
        GameManager.GetManager().StartThirdPersonCamera();
        base.ExitInteraction();
    }
}
