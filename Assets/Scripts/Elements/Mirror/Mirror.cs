using System.Collections;
using UnityEngine;

public class Mirror : Interactables
{
    private int m_Counter = 0;

    public string[] bad1, lessbad, normal, good;
    private int counterbad1, counterless, counternormal, countergood;

    private void Start()
    {
        GameManager.GetManager().Mirror = this;
    }

    public override void Interaction(int options)
    {
        switch (options)
        {
            case 1:
                if (!m_Done)
                {
                    m_Done = true;
                    GameManager.GetManager().m_CurrentStateGame = GameManager.StateGame.MiniGame;
                    GameManager.GetManager().PlayerController.SetInteractable("Mirror");

                    StartCoroutine(LookUp());
                    //if (m_MirrorInteractPhrases.Length > 0)
                    //{
                    //    if (m_Counter >= m_InteractPhrases.Length)
                    //        m_Counter = 0;

                    //    GameManager.GetManager().Dialogue.SetDialogue(m_InteractPhrases[m_Counter]);
                    //    GameManager.GetManager().Autocontrol.RemoveAutoControl(5);
                    //    m_Counter++;

                    //    m_Done = true;
                    //}
                }
                break;
        }
    }

    private IEnumerator LookUp()
    {
        yield return new WaitForSeconds(1);
        if (GameManager.GetManager().Autocontrol.m_Slider.value <= 0.3f)
        {
            GameManager.GetManager().Dialogue.SetDialogue(bad1[counterbad1]);
            counterbad1++;
            if (counterbad1 >= bad1.Length)
                counterbad1 = 0;
            GameManager.GetManager().PlayerController.SadMoment();
            GameManager.GetManager().Autocontrol.RemoveAutoControl(5);
        }
        else if (GameManager.GetManager().Autocontrol.m_Slider.value > 0.3f && GameManager.GetManager().Autocontrol.m_Slider.value <= 0.5f)
        {
         
            GameManager.GetManager().Dialogue.SetDialogue(lessbad[counterless]);
            counterless++;
            if (counterless >= lessbad.Length)
                counterless = 0;
            GameManager.GetManager().PlayerController.SadMoment();
            GameManager.GetManager().Autocontrol.RemoveAutoControl(2);
        }
        else if (GameManager.GetManager().Autocontrol.m_Slider.value > 0.5f && GameManager.GetManager().Autocontrol.m_Slider.value <= 0.8f)
        {
            GameManager.GetManager().Dialogue.SetDialogue(normal[counternormal]);
            counternormal++;
            if (counternormal >= normal.Length)
                counternormal = 0;
            GameManager.GetManager().PlayerController.HappyMoment();
            GameManager.GetManager().Autocontrol.AddAutoControl(2);
        }
        else if (GameManager.GetManager().Autocontrol.m_Slider.value > 0.8f)
        {
            GameManager.GetManager().Dialogue.SetDialogue(good[countergood]);
            countergood++;
            if (countergood >= good.Length)
                countergood = 0;

            GameManager.GetManager().PlayerController.HappyMoment();
            GameManager.GetManager().Autocontrol.AddAutoControl(2);
        }

        yield return new WaitForSeconds(2);
        GameManager.GetManager().Dialogue.StopDialogue();
        GameManager.GetManager().StartThirdPersonCamera();
    }

    public override void ResetInteractable()
    {
        m_Done = false;
    }
}
