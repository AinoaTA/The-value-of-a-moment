using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class Computer : Interactables
{
    public override void Interaction(int options)
    {
        switch (options)
        {
            case 1:
                GameManager.GetManager().m_CurrentStateGame = GameManager.StateGame.MiniGame;
                GameManager.GetManager().CanvasManager.ComputerScreenIn();
                break;
        }
    }
}
