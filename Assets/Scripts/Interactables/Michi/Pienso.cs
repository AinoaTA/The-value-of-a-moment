using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pienso : Interactables
{
    private bool grabbed;

    private void Start()
    {
        grabbed = false;
    }

    public override void Interaction(int optionNumber)
    {
        base.Interaction(optionNumber);
        switch (optionNumber)
        {
            case 1:
                // FMODUnity.RuntimeManager.PlayOneShot("event:/Env/Pienso", transform.position);
                grabbed = true;
                this.gameObject.SetActive(false);
                break;
        }
    }

    public override void ExitInteraction()
    {
        // FMODUnity.RuntimeManager.PlayOneShot("event:/Env/Pienso", transform.position);
        GameManager.GetManager().StartThirdPersonCamera();
        base.ExitInteraction();
    }

    public void ResetPienso()
    {
        grabbed = false;
        gameObject.SetActive(true);
    }
}
