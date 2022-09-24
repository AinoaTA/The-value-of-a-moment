using UnityEngine;

public class Regadera : GeneralActions
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

    public override void EnterAction()
    {
        Grab();
        GameManager.GetManager().actionObjectManager.LookingAnInteractable(null);
    }

    public override void ResetObject()
    {
        grabbed = false;
    }
}
