using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ActionObjectManager : MonoBehaviour
{
    public List<ActionObject> allObjects = new List<ActionObject>();
    public ActionObject currObject;

    private void OnDisable()
    {
        GameManager.GetManager().playerInputs._FirstInteraction -= InteractionManager;
    }

    private void Awake()
    {
        GameManager.GetManager().actionObjectManager = this;
        allObjects = FindObjectsOfType<ActionObject>().ToList();
    }
    private void Start()
    {
        GameManager.GetManager().playerInputs._FirstInteraction += InteractionManager;
    }

    public void InteractionManager()
    {
        if (GameManager.GetManager().gameStateController.m_CurrentStateGame != GameStateController.StateGame.GamePlay || currObject == null)
            return;

        currObject.Interaction();
    }

    public void LookingAnInteractable(ActionObject interactables)
    {
        currObject = interactables;
    }


    public void ResetAll()
    {
        for (int i = 0; i < allObjects.Count; i++)
            allObjects[i].ResetObject();
    }
}
