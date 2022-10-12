using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Limpiar : GeneralActions
{
    public override void EnterAction()
    {
        GameManager.GetManager().dialogueManager.SetDialogue("ICocina",canRepeat:true);
        base.EnterAction();
    }

    public override void ExitAction()
    {
        base.ExitAction();
    }
}
