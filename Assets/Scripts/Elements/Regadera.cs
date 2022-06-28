using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Regadera : Interactables
{
    public WaterCan waterCan;
    [HideInInspector]public bool grabbed = false;

    private void Start()
    {
        GameManager.GetManager().WaterCan = this;
    }

    public void Grab()
    {
        if(!grabbed)
        {
            this.gameObject.SetActive(false);
            waterCan.tengoRegadera = true;
            GameManager.GetManager().WaterCanGrabbed = true;
            grabbed = true;
           // GameManager.GetManager().ChangeGameState(GameManager.StateGame.GamePlay);
        }
    }

    public override void Interaction(int optionsSelected)
    {
        switch (optionsSelected)
        {
            case 1:
                Grab();
                break;
            case 2:
                break;
        } 
    }

    public override void ResetInteractable()
    {
        grabbed = false;
        waterCan.tengoRegadera = false;
    }
}
