using System.Collections;
using UnityEngine;

public class Mirror : Interactables
{
    //private int m_Counter = 0;

    public string[] bad1, lessbad, normal, good;
    private int counterbad1, counterless, counternormal, countergood;

    private void Start()
    {
       // GameManager.GetManager().Mirror = this;
    }

    public override void Interaction(int options)
    {
        base.Interaction(options);
        switch (options)
        {
            case 1:
                FMODUnity.RuntimeManager.PlayOneShot("event:/UI/Zoom In", transform.position);
                if (!interactDone)
                {
                    interactDone = true;
                    GameManager.GetManager().gameStateController.ChangeGameState(2);
                    GameManager.GetManager().cameraController.StartInteractCam(7);

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


    //private void OnMouseDown()
    //{
    //    if (!actionEnter)
    //    {
    //        SetCanvasValue(false);
    //        actionEnter = true;
    //        Interaction(1);
    //    }
    //}

    private IEnumerator LookUp()
    {
        yield return new WaitForSeconds(1);
        if (GameManager.GetManager().autocontrol.GetAutcontrolValue() <= 0.3f)
        {
          //  GameManager.GetManager().Dialogue.SetDialogue(bad1[counterbad1]);
            counterbad1++;
            if (counterbad1 >= bad1.Length)
                counterbad1 = 0;
            GameManager.GetManager().playerController.SadMoment();
            GameManager.GetManager().autocontrol.RemoveAutoControl(5);
        }
        else if (GameManager.GetManager().autocontrol.GetAutcontrolValue() > 0.3f && GameManager.GetManager().autocontrol.GetAutcontrolValue() <= 0.5f)
        {
         
            //GameManager.GetManager().Dialogue.SetDialogue(lessbad[counterless]);
            counterless++;
            if (counterless >= lessbad.Length)
                counterless = 0;
            GameManager.GetManager().playerController.SadMoment();
            GameManager.GetManager().autocontrol.RemoveAutoControl(2);
        }
        else if (GameManager.GetManager().autocontrol.GetAutcontrolValue() > 0.5f && GameManager.GetManager().autocontrol.GetAutcontrolValue() <= 0.8f)
        {
           // GameManager.GetManager().Dialogue.SetDialogue(normal[counternormal]);
            counternormal++;
            if (counternormal >= normal.Length)
                counternormal = 0;
            GameManager.GetManager().playerController.HappyMoment();
            GameManager.GetManager().autocontrol.AddAutoControl(2);
        }
        else if (GameManager.GetManager().autocontrol.GetAutcontrolValue() > 0.8f)
        {
           // GameManager.GetManager().Dialogue.SetDialogue(good[countergood]);
            countergood++;
            if (countergood >= good.Length)
                countergood = 0;

            GameManager.GetManager().playerController.HappyMoment();
            GameManager.GetManager().autocontrol.AddAutoControl(2);
        }

        yield return new WaitForSeconds(2);
     //   GameManager.GetManager().Dialogue.StopDialogue();
        GameManager.GetManager().StartThirdPersonCamera();
    }

    public override void ResetInteractable()
    {
        interactDone = false;
    }
}
