using UnityEngine;
public class GeneralActions : MonoBehaviour, ILock
{
    public bool InteractableBlocked { get => _blocked; set => _blocked = value; }
    [SerializeField] private bool _blocked;
    public GameObject OptionsCanvas;
    public Animator anim;
    protected bool showing;
    protected bool done;

    [SerializeField] protected string nameAction;

    [SerializeField] bool inDistance;
    float minDistanceToInteract = 4f;

    public virtual void ResetObject()
    {
        done = false;
        showing = false;
    }

    protected virtual void OnMouseEnter()
    {
        if (InteractableBlocked || !inDistance) return;
        if (GameManager.GetManager().gameStateController.CheckGameState(1) && !showing)// && !actionEnter)
        {
            showing = true;
            anim.SetBool("Showing", showing);
            GameManager.GetManager().actionObjectManager.LookingAnInteractable(this);
        }
    }

    protected virtual void OnMouseOver()
    {
        if (InteractableBlocked) return;

        /// QUE VAMOS SOBRADES DE FPS DICES?
        inDistance = (Vector3.Distance(Camera.main.ViewportToWorldPoint(new Vector2(0.5f, 0.5f)), transform.position) <= minDistanceToInteract);

        if (!inDistance && showing)
        {
            GameManager.GetManager().actionObjectManager.LookingAnInteractable(null);
            ExitCanvas();
        }
        if (inDistance && !showing)
            ShowCanvas();
    }

    protected virtual void OnMouseExit()
    {
        if (InteractableBlocked || !inDistance) return;
        if (showing)
        {
            ExitCanvas();
            GameManager.GetManager().actionObjectManager.LookingAnInteractable(null);
        }
    }
    void ShowCanvas()
    {
        showing = true;
        anim.SetBool("Showing", showing);
        GameManager.GetManager().actionObjectManager.LookingAnInteractable(this);

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


    public virtual void DoInteraction(int id) { }
}
