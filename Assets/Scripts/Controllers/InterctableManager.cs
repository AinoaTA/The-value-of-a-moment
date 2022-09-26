using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InterctableManager : MonoBehaviour
{
    [SerializeField] private List<Interactables> allInteractables = new List<Interactables>();
    [SerializeField] private Interactables currInteractable;

    private void Start()
    {
        GameManager.GetManager().playerInputs._FirstInteraction += FirstInteract;
        GameManager.GetManager().playerInputs._SecondInteraction += SecondInteract;
        GameManager.GetManager().playerInputs._ExitInteraction += ExitInteract;
    }
    private void OnDisable()
    {
        GameManager.GetManager().playerInputs._FirstInteraction -= FirstInteract;
        GameManager.GetManager().playerInputs._SecondInteraction -= SecondInteract;
        GameManager.GetManager().playerInputs._ExitInteraction -= ExitInteract;
    }
    private void Awake()
    {
        GameManager.GetManager().interactableManager = this;
        allInteractables = FindObjectsOfType<Interactables>().ToList();
    }

    public void FirstInteract()
    {
        if (!GameManager.GetManager().gameStateController.CheckGameState(1) || 
            GameManager.GetManager().gameStateController.CheckGameState(3) || currInteractable == null)
            return;

        if (!currInteractable.GetDone())
            currInteractable.Interaction(1);
    }

    public void SecondInteract()
    {
        if (!GameManager.GetManager().gameStateController.CheckGameState(1) || currInteractable == null)
            return;

        if (currInteractable.totalOptions > 1)
            currInteractable.Interaction(2);
    }

    public void ExitInteract()
    {
        //if(GameManager.GetManager().gameStateController.CheckPreviousGameStateWasAnAction

        if (currInteractable != null && GameManager.GetManager().gameStateController.CheckGameState(2))
            currInteractable.ExitInteraction();

    }

    public void LookingAnInteractable(Interactables interactables)
    {
        currInteractable = interactables;
    }

    public void ResetAll()
    {
        for (int i = 0; i < allInteractables.Count; i++)
        {
            allInteractables[i].ResetInteractable();
        }
    }
}
