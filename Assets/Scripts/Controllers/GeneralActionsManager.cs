using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GeneralActionsManager : MonoBehaviour
{
    [SerializeField] private List<GeneralActions> allObjects = new List<GeneralActions>();
    [SerializeField] private GeneralActions currObject;

    private void OnDisable()
    {
        GameManager.GetManager().playerInputs._FirstInteraction -= InteractionManager;
        GameManager.GetManager().playerInputs._SecondInteraction -= SecondExtraInteraction;
        GameManager.GetManager().playerInputs._ExitInteraction -= ExitActionManager;
    }

    private void Awake()
    {
        GameManager.GetManager().actionObjectManager = this;
        allObjects = FindObjectsOfType<GeneralActions>().ToList();
    }
    private void Start()
    {
        GameManager.GetManager().playerInputs._FirstInteraction += InteractionManager;
        GameManager.GetManager().playerInputs._ExitInteraction += ExitActionManager;
        GameManager.GetManager().playerInputs._SecondInteraction += SecondExtraInteraction;
    }

    public void LookingAnInteractable(GeneralActions interactables)
    {
        currObject = interactables;
    }

    public void ResetAll()
    {
        for (int i = 0; i < allObjects.Count; i++)
        {
            allObjects[i].gameObject.SetActive(true);
              allObjects[i].ResetObject();
        }

        LookingAnInteractable(null);
    }

    public void ExitActionManager()
    {
        if (currObject != null && GameManager.GetManager().gameStateController.CheckGameState(3))
            currObject.ExitAction();
    }

    #region Interactions/Actions
    /// <summary>
    /// First place for Actions. 
    /// If Action has extra interactions, this is the first interaction option.
    /// </summary>
    private void InteractionManager()
    {
        if (GameManager.GetManager().gameStateController.CheckGameState(1) && currObject != null)
            currObject.EnterAction();
        else if (GameManager.GetManager().gameStateController.CheckGameState(3) && currObject != null)
            currObject.DoInteraction(0);
    }
    /// <summary>
    /// Second interaction option. Not used for Actions
    /// </summary>
    private void SecondExtraInteraction()
    {
        if (GameManager.GetManager().gameStateController.CheckGameState(3) && currObject != null)
            currObject.DoInteraction(1);
    }
    #endregion
}
