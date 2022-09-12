public class VR : Interactables
{
    private int m_Counter = 0;

    private void Start()
    {
        //GameManager.GetManager().VR = this;
    }
    public override void Interaction(int options)
    {
        switch (options)
        {
            case 1:
                if (!interactDone)
                {
                    //if (m_Counter >= m_HelpPhrasesVoiceOff.Length - 1)
                    //    m_Counter = 0;

                    //GameManager.GetManager().Dialogue.SetDialogue(m_InteractPhrases[m_Counter]);
                    GameManager.GetManager().autocontrol.AddAutoControl(3);
                    m_Counter++;

                    interactDone = true;
                }

                break;
        }

    }

    public void ResetVRDay()
    {
        interactDone = false;
    }
}
