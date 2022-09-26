using UnityEngine;

public class Interactables : MonoBehaviour
{
    [Header("Data")]
    public string nameInteractable;

    [Header("Options")]
    public int totalOptions = 1;
    [SerializeField] protected bool interactDone;
    [SerializeField] protected float m_MaxAutoControl, m_MiddleAutoControl, m_MinAutoControl;

    [Header("Activated Action")]
    public bool canDoInAction=false;

    [Header("Canvas")]
    [SerializeField] private GameObject OptionsCanvas;
    [SerializeField] private Animator anim;

    public virtual bool GetDone() { return interactDone; }

    public virtual void Interaction(int optionNumber)
    {
        actionEnter = true;
        SetCanvasValue(false);
    }

    public virtual void ExitInteraction()
    {
        actionEnter = false;
        SetCanvasValue(false);
        GameManager.GetManager().interactableManager.LookingAnInteractable(null);
    }
    [HideInInspector] public bool showing = false;
    protected bool actionEnter;

    private void Start()
    {
        SetCanvasValue(false);
    }

    public virtual void ResetInteractable()
    {
        interactDone = false;
        actionEnter = false;
        SetCanvasValue(false);
    }

    private void OnMouseEnter()
    {
        Show();
    }
    private void OnMouseExit()
    {
        Hide();
    }

    protected void Show()
    {
        if (GameManager.GetManager().gameStateController.CheckGameState(1) && !showing && !actionEnter && !interactDone)
        {
            showing = true;
            anim.SetBool("Showing", showing);
            GameManager.GetManager().interactableManager.LookingAnInteractable(this);
        }
    }

    protected void Hide()
    {
        if (showing && !actionEnter && !interactDone)
        {
            showing = false;
            anim.SetBool("Showing", showing);
            GameManager.GetManager().interactableManager.LookingAnInteractable(null);
        }
    }

    public void SetCanvasValue(bool showing_)
    {
        showing = showing_;
        anim.SetBool("Showing", showing_);
    }
}
