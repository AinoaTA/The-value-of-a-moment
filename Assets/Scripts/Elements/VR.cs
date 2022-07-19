using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VR : Interactables
{
    private int Counter = 0;

    private void Start()
    {
        //GameManager.GetManager().VR = this;
    }
    public override void Interaction(int options)
    {
        switch (options)
        {
            case 1:
                if (!Done)
                {
                    //if (Counter >= HelpPhrasesVoiceOff.Length - 1)
                    //    Counter = 0;

                    //GameManager.GetManager().Dialogue.SetDialogue(InteractPhrases[Counter]);
                    GameManager.GetManager().Autocontrol.AddAutoControl(3);
                    Counter++;

                    Done = true;
                }

                break;
        } 
      
    }

    public void ResetVRDay()
    {
        Done = false;
    }

}
