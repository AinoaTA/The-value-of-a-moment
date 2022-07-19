using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.EventSystems;

public class InterctableManager : MonoBehaviour
{
    public List<Interactables> allInteractables = new List<Interactables>();

  

    public Interactables currInteractable;
    private Camera cam;
    public float minDistanceToShow;
    public LayerMask interactableLayers;
    private void OnDisable()
    {
        GameManager.GetManager().playerInputs._FirstInteraction -= FirstInteract;
        GameManager.GetManager().playerInputs._SecondInteraction -= SecondInteract;
    }

    private void Awake()
    {
        GameManager.GetManager().interactableManager = this;
        cam = Camera.main;
        allInteractables = FindObjectsOfType<Interactables>().ToList();
    }
    private void Start()
    {
        GameManager.GetManager().playerInputs._FirstInteraction += FirstInteract;
        GameManager.GetManager().playerInputs._SecondInteraction += SecondInteract;
    }


    public void FirstInteract()
    {
        if (GameManager.GetManager().gameStateController.m_CurrentStateGame != GameStateController.StateGame.GamePlay || currInteractable == null)
            return;

        if (!currInteractable.GetDone())
        {
            currInteractable.Interaction(1);
            currInteractable = null;
        }

    }

    public void SecondInteract()
    {
        if (GameManager.GetManager().gameStateController.m_CurrentStateGame != GameStateController.StateGame.GamePlay || currInteractable == null)
            return;

        if (currInteractable.totalOptions > 1)
        {
            currInteractable.Interaction(2);
            currInteractable = null;
        }
    }

    public void LookingAnInteractable(Interactables interactables)
    {
        currInteractable = interactables;
    }


    private void Update()
    {
        //if (GameManager.GetManager().gameStateController.m_CurrentStateGame != GameStateController.StateGame.GamePlay)
        //    return;

        //Ray l_Ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

        //if (Physics.Raycast(l_Ray, out RaycastHit l_Hit, minDistanceToShow, interactableLayers))
        //{
        //    currInteractable = l_Hit.collider.gameObject.GetComponent<Interactables>(); ;
     
        //    if (currInteractable != null)
        //    {
        //        print(currInteractable.name);
        //        Debug.Log("nice");
        //    }

        //    //if (currInteractable != null && currInteractable != lookingInteractable)
        //    //{
        //    //    if (currInteractable.options > 1 || !currInteractable.GetDone())
        //    //    {
        //    //        lookingInteractable = currInteractable;
        //    //        currInteractable.ShowCanvas();
        //    //    }
        //    //}
        //    //else if (currInteractable == null && lookingInteractable != null)
        //    //{
        //    //    lookingInteractable.HideCanvas();
        //    //    lookingInteractable = null;
        //    //}
        //}
    }
}
