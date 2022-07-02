using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class Computer : Interactables
{
    public override void Interaction(int options)
    {
        base.Interaction(options);
        switch (options)
        {
            case 1:
                GameManager.GetManager().gameStateController.m_CurrentStateGame = GameStateController.StateGame.MiniGame;
                GameManager.GetManager().CanvasManager.ComputerScreenIn();
                break;
        }
    }
}
