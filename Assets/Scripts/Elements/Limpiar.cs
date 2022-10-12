using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Limpiar : GeneralActions
{
    public override void EnterAction()
    {
        switch (GameManager.GetManager().dayController.GetTimeDay())
        {
            case DayController.DayTime.Manana:
                break;
            case DayController.DayTime.MedioDia:
                GameManager.GetManager().dialogueManager.SetDialogue("ICocina", canRepeat: true);
                break;
            case DayController.DayTime.Tarde:
                GameManager.GetManager().dialogueManager.SetDialogue("ICocina", canRepeat: true);
                break;
            case DayController.DayTime.Noche:
                GameManager.GetManager().dialogueManager.SetDialogue("Anochece", canRepeat: true);
                break;
            default:
                break;
        }
        InteractableBlocked = true;
        base.EnterAction();
    }

    public override void ExitAction()
    {
        base.ExitAction();
    }
}
