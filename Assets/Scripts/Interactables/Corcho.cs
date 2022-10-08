using UnityEngine;

public class Corcho : Interactables
{
    [HideInInspector] public bool playing = true;
    [SerializeField] CorchoImage[] images;
    [SerializeField] BoxCollider ownCollider;
    public override void Interaction(int options)
    {
        base.Interaction(options);
        switch (options)
        {
            case 1:
                BlockAll(true);
                ownCollider.enabled = false;
                GameManager.GetManager().canvasController.UnLock();
                GameManager.GetManager().gameStateController.ChangeGameState(2);
                GameManager.GetManager().cameraController.StartInteractCam(nameInteractable);
                break;
            case 2:
                break;
        }
    }

    public override void ExitInteraction()
    {
        if (!playing)
        {
            BlockAll(false);
            ownCollider.enabled = true;
            GameManager.GetManager().StartThirdPersonCamera();
            base.ExitInteraction();
        }
    }

    public void BlockAll(bool t) 
    {
        for (int i = 0; i < images.Length; i++)
            images[i].Ready(t);
    }
}
