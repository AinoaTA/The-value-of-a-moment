using System.Collections;
using UnityEngine;

public class Mirror : Interactables
{
    private int Counter = 0;

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
                if (!Done)
                {
                    Done = true;
                    GameManager.GetManager().gameStateController.ChangeGameState(2);
                    GameManager.GetManager().cameraController.StartInteractCam(7);

                    StartCoroutine(LookUp());
                    //if (MirrorInteractPhrases.Length > 0)
                    //{
                    //    if (Counter >= InteractPhrases.Length)
                    //        Counter = 0;

                    //    GameManager.GetManager().Dialogue.SetDialogue(InteractPhrases[Counter]);
                    //    GameManager.GetManager().Autocontrol.RemoveAutoControl(5);
                    //    Counter++;

                    //    Done = true;
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
        if (GameManager.GetManager().Autocontrol.Slider.value <= 0.3f)
        {
          //  GameManager.GetManager().Dialogue.SetDialogue(bad1[counterbad1]);
            counterbad1++;
            if (counterbad1 >= bad1.Length)
                counterbad1 = 0;
            GameManager.GetManager().PlayerController.SadMoment();
            GameManager.GetManager().Autocontrol.RemoveAutoControl(5);
        }
        else if (GameManager.GetManager().Autocontrol.Slider.value > 0.3f && GameManager.GetManager().Autocontrol.Slider.value <= 0.5f)
        {
         
            //GameManager.GetManager().Dialogue.SetDialogue(lessbad[counterless]);
            counterless++;
            if (counterless >= lessbad.Length)
                counterless = 0;
            GameManager.GetManager().PlayerController.SadMoment();
            GameManager.GetManager().Autocontrol.RemoveAutoControl(2);
        }
        else if (GameManager.GetManager().Autocontrol.Slider.value > 0.5f && GameManager.GetManager().Autocontrol.Slider.value <= 0.8f)
        {
           // GameManager.GetManager().Dialogue.SetDialogue(normal[counternormal]);
            counternormal++;
            if (counternormal >= normal.Length)
                counternormal = 0;
            GameManager.GetManager().PlayerController.HappyMoment();
            GameManager.GetManager().Autocontrol.AddAutoControl(2);
        }
        else if (GameManager.GetManager().Autocontrol.Slider.value > 0.8f)
        {
           // GameManager.GetManager().Dialogue.SetDialogue(good[countergood]);
            countergood++;
            if (countergood >= good.Length)
                countergood = 0;

            GameManager.GetManager().PlayerController.HappyMoment();
            GameManager.GetManager().Autocontrol.AddAutoControl(2);
        }

        yield return new WaitForSeconds(2);
     //   GameManager.GetManager().Dialogue.StopDialogue();
        GameManager.GetManager().StartThirdPersonCamera();
    }

    public override void ResetInteractable()
    {
        Done = false;
    }
}
