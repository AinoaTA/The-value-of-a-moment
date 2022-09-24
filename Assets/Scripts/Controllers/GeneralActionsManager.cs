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
    }

    public void InteractionManager()
    {
        if (!GameManager.GetManager().gameStateController.CheckGameState(1) || currObject == null)
            return;

        currObject.EnterAction();
    }

    public void LookingAnInteractable(GeneralActions interactables)
    {
        currObject = interactables;
    }


    public void ResetAll()
    {
        for (int i = 0; i < allObjects.Count; i++)
            allObjects[i].ResetObject();
    }

    public void ExitActionManager()
    {
        if (currObject != null && GameManager.GetManager().gameStateController.CheckGameState(3))
            currObject.ExitAction();

    }
}
