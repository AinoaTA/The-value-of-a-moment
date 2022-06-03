using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Regadera : Interactables
{
    private void Start()
    {
        GameManager.GetManager().WaterCan = this;
    }

    public void Grab()
    {
        this.gameObject.SetActive(false);
        GameManager.GetManager().WaterCanGrabbed = true;
        GameManager.GetManager().EndMinigame();
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
}
