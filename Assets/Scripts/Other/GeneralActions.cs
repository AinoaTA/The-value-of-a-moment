using UnityEngine;
public class GeneralActions : MonoBehaviour,ILock
{
    public bool InteractableBlocked { get => _blocked; set => _blocked = value; }
    private bool _blocked;
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

    protected virtual void OnMouseEnter()
    {
        if (InteractableBlocked) return;
        if (GameManager.GetManager().gameStateController.CheckGameState(1) && !showing)// && !actionEnter)
        {
            showing = true;
            anim.SetBool("Showing", showing);
            GameManager.GetManager().actionObjectManager.LookingAnInteractable(this);
        }
    }


    protected virtual void OnMouseExit()
    {
        if (InteractableBlocked) return;
        if (showing)
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
        GameManager.GetManager().actionObjectManager.LookingAnInteractable(null);
    }


    public virtual void DoInteraction(int id)  { }
}
