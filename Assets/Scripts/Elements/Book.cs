using System.Collections;
using UnityEngine;

public class Book : Interactables
{
    private int Counter = 0;
    public string[] BookInteractPhrases;
    public Grabbing Grabbing;

    public delegate void DelegateSFXBook();
    public static DelegateSFXBook DelegateSFXBook;

    public override void Interaction(int options)
    {
        base.Interaction(options);
        switch (options)
        {
            case 1:
                if(Grabbing != null)
                {
                    Grabbing.SetAccessCamera(true);
                    GameManager.GetManager().cameraController.StartInteractCam(8);
                    //GameManager.GetManager().PlayerController.SetInteractable("Grab");
                    //GameManager.GetManager().CurrentStateGame = GameManager.StateGame.GamePlay;
                    //HideCanvas();

                    //if (Counter >= InteractPhrases.Length)
                    //    Counter = 0;

                    //StartCoroutine(DelayDialogue());
                }
                break;
        }
    }
}
