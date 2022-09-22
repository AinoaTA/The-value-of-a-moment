using TMPro;
using UnityEngine;

public class Interruptor : ActionObject
{
    public GameObject lights;
    public TextMeshProUGUI textDisplay;
    private bool isLightOn = false;
    public int minLight = 6000, maxLight = 12000;

    private void Start()
    {
        textDisplay.text = "E Encender";
        foreach (var light in lights.GetComponentsInChildren<Light>())
            light.intensity = minLight;
    }

    public override void Interaction()
    {
        GameManager.GetManager().actionObjectManager.LookingAnInteractable(this);
        TurnLights();
        ChangeText();
    }

    public override void ResetObject()
    {
        base.ResetObject();
    }

    private void TurnLights()
    {
        foreach (var light in lights.GetComponentsInChildren<Light>())
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/Env/Light", transform.position);
            if (isLightOn) light.intensity = minLight;
            else light.intensity = maxLight;
        }
        isLightOn = !isLightOn;
    }

    private void ChangeText()
    {
        if (isLightOn)
        {
            textDisplay.text = "E Apagar";
        }
        else
        {
            textDisplay.text = "E Encender";
        }
    }
}
