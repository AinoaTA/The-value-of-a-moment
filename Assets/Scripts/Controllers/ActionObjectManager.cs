using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ActionObjectManager : MonoBehaviour
{
    [SerializeField] private List<ActionObject> allObjects = new List<ActionObject>();
    [SerializeField] private ActionObject currObject;

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
