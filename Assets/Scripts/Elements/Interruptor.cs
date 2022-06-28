using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Interruptor : Interactables
{
    public GameObject lights;
    public TextMeshProUGUI textDisplay;
    private bool isLightOn = false;
    public int minLight=6000, maxLight=12000;
    private void Start()
    {
        // Reset values
        textDisplay.text = "E Encender";
        foreach (var light in lights.GetComponentsInChildren<Light>())
        {
             light.intensity = minLight;
        }

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
            if(isLightOn) light.intensity = minLight;
            else light.intensity = maxLight;
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
