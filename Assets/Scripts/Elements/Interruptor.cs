using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Interruptor : Interactables
{
    public GameObject lights;
    public TextMeshProUGUI textDisplay;
    private bool isLightOn = false;

    private void Start()
    {
        // Reset values
        textDisplay.text = "E Encender";
    }

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
        foreach (var light in lights.GetComponentsInChildren<Light>())
        {
            if(isLightOn) light.intensity = 6000;
            else light.intensity = 12000;
        }
        isLightOn = !isLightOn;
    }

    private void ChangeText()
    {
        if(isLightOn) 
        {
            textDisplay.text = "E Apagar";
        }
        else
        {
            textDisplay.text = "E Encender";
        }
    }
}
