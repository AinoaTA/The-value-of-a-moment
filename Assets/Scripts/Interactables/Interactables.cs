using UnityEngine;

public class Interactables : MonoBehaviour, ILock
{
    public bool InteractableBlocked { get => _blocked; set => _blocked = value; }
    [SerializeField] private bool _blocked;

    [Header("Data")]
    public string nameInteractable;

    [Header("Options")]
    public int totalOptions = 1;
    [SerializeField] protected bool interactDone;
    [SerializeField] protected float m_MaxAutoControl, m_MiddleAutoControl, m_MinAutoControl;

    [Header("Canvas")]
    [SerializeField] private GameObject OptionsCanvas;
    [SerializeField] private Animator anim;
    [SerializeField] private GameObject[] texts;
    private Vector3[] textsInitPos;

    [HideInInspector] public bool showing = false;
    [HideInInspector] public bool actionEnter;
    public virtual bool GetDone() { return interactDone; }

    public virtual void Interaction(int optionNumber)
    {
        SetCanvasValue(false);
    }

    public virtual void ExitInteraction()
    {
        SetCanvasValue(false);
        GameManager.GetManager().interactableManager.LookingAnInteractable(null);
    }


    private void Start()
    {
        SetCanvasValue(false);
    }

    public virtual void ResetInteractable()
    {
        if (interactDone && totalOptions > 1)
            ResetOptionsPos();

        interactDone = false;
        SetCanvasValue(false);
    }

    protected virtual void OnMouseEnter()
    {
        if (InteractableBlocked) return;
        Show();
    }
    private void OnMouseExit()
    {
        if (InteractableBlocked) return;
        Hide();
    }

    protected void Show()
    {
        if (GameManager.GetManager().gameStateController.CheckGameState(1) && !showing && !actionEnter)
        {
            if (interactDone && totalOptions <= 1) return;

            showing = true;
            anim.SetBool("Showing", showing);
            GameManager.GetManager().interactableManager.LookingAnInteractable(this);
        }
    }

    protected void Hide()
    {
        if (showing && !actionEnter)
        {
            if (interactDone && totalOptions <= 1) return;

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

    public virtual void ExtraInteraction() { }

    public virtual void EndExtraInteraction()
    {
        GameManager.GetManager().interactableManager.LookingAnInteractable(null);
    }

    protected void OptionComplete()
    {
        textsInitPos = new Vector3[texts.Length];
        for (int i = 0; i < texts.Length; i++)
            textsInitPos[i] = texts[i].transform.position;

        texts[0].SetActive(false);
        texts[1].transform.localPosition = Vector3.zero;
    }

    void ResetOptionsPos()
    {
        texts[0].SetActive(true);
        texts[1].transform.localPosition = textsInitPos[1];
    }
}
