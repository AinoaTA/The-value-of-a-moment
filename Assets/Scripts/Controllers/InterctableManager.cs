using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class InterctableManager : MonoBehaviour
{
    public List<Interactables> allInteractables = new List<Interactables>();
    public Interactables currInteractable;

    private void OnDisable()
    {
        GameManager.GetManager().playerInputs._FirstInteraction -= FirstInteract;
        GameManager.GetManager().playerInputs._SecondInteraction -= SecondInteract;
        GameManager.GetManager().playerInputs._ExitInteraction -= ExitInteract;
    }
    private void Start()
    {
        GameManager.GetManager().playerInputs._FirstInteraction += FirstInteract;
        GameManager.GetManager().playerInputs._SecondInteraction += SecondInteract;
        GameManager.GetManager().playerInputs._ExitInteraction += ExitInteract;
    }
    private void Awake()
    {
        GameManager.GetManager().interactableManager = this;
        allInteractables = FindObjectsOfType<Interactables>().ToList();
    }

    public void FirstInteract()
    {
        if (GameManager.GetManager().gameStateController.m_CurrentStateGame != GameStateController.StateGame.GamePlay || currInteractable == null)
            return;

        if (!currInteractable.GetDone())
            currInteractable.Interaction(1);
    }

    public void SecondInteract()
    {
        if (GameManager.GetManager().gameStateController.m_CurrentStateGame != GameStateController.StateGame.GamePlay || currInteractable == null)
            return;

        if (currInteractable.totalOptions > 1)
            currInteractable.Interaction(2);
    }

    public void ExitInteract()
    {
        if (currInteractable != null && GameManager.GetManager().gameStateController.m_CurrentStateGame == GameStateController.StateGame.MiniGame)
        {
            currInteractable.ExitInteraction();
        }
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
