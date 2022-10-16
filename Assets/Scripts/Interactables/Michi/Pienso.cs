using UnityEngine;

public class Pienso : Interactables
{
    private bool grabbed;
    [SerializeField] private Cuenco cuenco;

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
                FMODUnity.RuntimeManager.PlayOneShot("event:/Env/GrabCatFood", transform.position);
                grabbed = true;
                cuenco.GrabbedPienso();
                gameObject.SetActive(false);
                break;
        }
    }

    public override void ExitInteraction()
    {
        GameManager.GetManager().StartThirdPersonCamera();
        base.ExitInteraction();
    }

    public void ResetPienso()
    {
        grabbed = false;
        gameObject.SetActive(true);
    }
}
