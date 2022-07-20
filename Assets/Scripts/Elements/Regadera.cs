using UnityEngine;

public class Regadera : ActionObject
{
    public WaterCan waterCan;
    [HideInInspector] public bool grabbed = false;

    public void Grab()
    {
        if (!grabbed)
        {
            this.gameObject.SetActive(false);
            waterCan.tengoRegadera = true;
            GameManager.GetManager().WaterCanGrabbed = true;
            grabbed = true;
           // GameManager.GetManager().ChangeGameState(GameManager.StateGame.GamePlay);
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
        waterCan.tengoRegadera = false;
    }
}
