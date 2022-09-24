using UnityEngine;
public class GeneralActions : MonoBehaviour
{
    public GameObject OptionsCanvas;
    public Animator anim;
    protected bool showing;
    protected bool done;
   [SerializeField]protected string nameAction;

    public virtual void ResetObject()
    {
        done = false;
        showing = false;
    }

    private void OnMouseEnter()
    {
        if (GameManager.GetManager().gameStateController.CheckGameState(1) && !showing)// && !actionEnter)
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
    void ExitCanvas()
    {
        showing = false;
        anim.SetBool("Showing", showing);
    }

    public virtual void EnterAction()
    {
        ExitCanvas();
    }

    public virtual void ExitAction()
    {
        ExitCanvas();
    }
}
