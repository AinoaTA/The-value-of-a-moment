using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Interruptor : Interactables
{
    public GameObject lights;
    public TextMeshProUGUI textDisplay;

    private void Start()
    {
        // Reset values
        lights.SetActive(false);
        textDisplay.text = "[E] Encender";
    }

    public override void Interaction(int options)
    {
        switch (options)
        {
            case 1:
                TurnLights();
                ChangeText();
                GameManager.GetManager().EndMinigameForLights();
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
            textDisplay.text = "[E] Apagar";
        }
        else
        {
            textDisplay.text = "[E] Encender";
        }
    }
}
