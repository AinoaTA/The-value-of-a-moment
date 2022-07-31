using UnityEngine;

public class Regadera : ActionObject
{
    public WaterCan waterCan;
    [HideInInspector] public bool grabbed = false;

    public void Grab()
    {
        if (!grabbed)
        {
            gameObject.SetActive(false);
            GameManager.GetManager().waterCanGrabbed = true;
            grabbed = true;
        }
    }

    public override void Interaction()
    {
        Grab();
        GameManager.GetManager().actionObjectManager.LookingAnInteractable(null);
    }

    public override void ResetObject()
    {
        grabbed = false;
    }
}
