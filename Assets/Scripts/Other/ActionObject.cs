using UnityEngine;
public class ActionObject : MonoBehaviour
{
    public GameObject OptionsCanvas;
    public Animator anim;
    protected bool showing;
    protected bool done;

    public virtual void ResetObject()
    {
        done = false;
        showing = false;
    }

    private void OnMouseEnter()
    {
        if (GameManager.GetManager().gameStateController.m_CurrentStateGame == GameStateController.StateGame.GamePlay && !showing)// && !actionEnter)
        {
            showing = true;
            anim.SetBool("Showing", showing);
            GameManager.GetManager().actionObjectManager.LookingAnInteractable(this);
        }
    }

    
    private void OnMouseExit()
    {
        if (showing)// && !actionEnter)
        {
            ExitCanvas();
            GameManager.GetManager().actionObjectManager.LookingAnInteractable(null);
        }
    }
    public virtual void Interaction()
    {
        ExitCanvas();
    }

    void ExitCanvas()
    {
        showing = false;
        anim.SetBool("Showing", showing);
    }

}
