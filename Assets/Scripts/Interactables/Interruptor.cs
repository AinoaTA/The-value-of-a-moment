using TMPro;
using UnityEngine;

public class Interruptor : GeneralActions
{
    public GameObject lights;
    public TextMeshProUGUI textDisplay;
    private bool isLightOn = false;
    public float minLight = 925.4644f;
    private float lightIntensity;

    private void Start()
    {
        textDisplay.text = "[E] Encender";
        foreach (var light in lights.GetComponentsInChildren<Light>())
        {
            lightIntensity = light.intensity;
            light.intensity = minLight;
        }
    }

    public override void EnterAction()
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

            if (isLightOn)
            {
                light.intensity = minLight;
                FMODUnity.RuntimeManager.PlayOneShot("event:/Env/LightOff", transform.position);
            }
            else
            {
                light.intensity = lightIntensity;
                FMODUnity.RuntimeManager.PlayOneShot("event:/Env/LightOn", transform.position);
            }
        }
        isLightOn = !isLightOn;
    }

    private void ChangeText()
    {
        if (isLightOn)
        {
            textDisplay.text = "[E] Apagar";
        }
        else
        {
            textDisplay.text = "[E] Encender";
        }
    }
}
