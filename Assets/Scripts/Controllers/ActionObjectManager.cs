using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ActionObjectManager : MonoBehaviour
{
    public List<ActionObject> allInteractables = new List<ActionObject>();
    public ActionObject currActionObj;

    private void OnDisable()
    {
        GameManager.GetManager().playerInputs._FirstInteraction -= InteractionManager;
    }

    private void Awake()
    {
        GameManager.GetManager().actionObjectManager = this;
        allInteractables = FindObjectsOfType<ActionObject>().ToList();
    }
    private void Start()
    {
        GameManager.GetManager().playerInputs._FirstInteraction += InteractionManager;
    }

    public void InteractionManager()
    {
        if (GameManager.GetManager().gameStateController.m_CurrentStateGame != GameStateController.StateGame.GamePlay || currActionObj == null)
            return;

        currActionObj.Interaction();
        //currActionObj = null;
    }

    public void LookingAnInteractable(ActionObject interactables)
    {
        currActionObj = interactables;
    }
}
