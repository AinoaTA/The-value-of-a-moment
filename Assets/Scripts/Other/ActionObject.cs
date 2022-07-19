using UnityEngine;
using UnityEngine.EventSystems;
public class ActionObject : MonoBehaviour
{
    public GameObject OptionsCanvas;
    public Animator anim;
    protected bool showing;
    protected bool done;
    public virtual void Interaction() { }


    public virtual void ResetObject()
    {
        done = false;
        showing = false;
    }

    private void OnMouseEnter()
    { 
        print(GameManager.GetManager().gameStateController.m_CurrentStateGame == GameStateController.StateGame.GamePlay && !showing);
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

    private void OnMouseOver()
    {
        if (Input.GetKeyDown(KeyCode.E) && GameManager.GetManager().gameStateController.m_CurrentStateGame == GameStateController.StateGame.GamePlay)
            Interaction();
    }
}
