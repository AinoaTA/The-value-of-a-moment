using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionObject : MonoBehaviour
{
    public GameObject OptionsCanvas;
    public Animator anim;
    protected bool showing;
    protected bool done;
    public virtual void Interaction() {}

    private void OnMouseEnter()
    {
        if (GameManager.GetManager().gameStateController.m_CurrentStateGame == GameStateController.StateGame.GamePlay && !showing)// && !actionEnter)
        {
            showing = true;
            anim.SetBool("Showing", showing);
           // GameManager.GetManager().interactableManager.LookingAnInteractable(this);
        }
    }

    private void OnMouseExit()
    {
        if (GameManager.GetManager().gameStateController.m_CurrentStateGame == GameStateController.StateGame.GamePlay && showing)// && !actionEnter)
        {
            showing = false;
            anim.SetBool("Showing", showing);
          //  GameManager.GetManager().interactableManager.LookingAnInteractable(null);
        }
    }
}
