using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Interruptor : Interactables
{
    public GameObject lights;
    public TextMeshProUGUI textDisplay;

    public override void Interaction(int options)
    {
        switch (options)
        {
            case 1:
                TurnLights();
                ChangeText();
                break;
        }
    }
    
    private void TurnLights()
    {
        lights.SetActive(!lights.activeInHierarchy);
    }

    private void ChangeText()
    {
        if(lights.activeInHierarchy) 
        {
            textDisplay.text = "Apagar";
        }
        else
        {
            textDisplay.text = "Encender";
        }
    }
}
