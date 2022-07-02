using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class InterctableManager : MonoBehaviour
{
    public List<Interactables> allInteractables = new List<Interactables>();

    public LayerMask m_LayerMask;
    public LayerMask m_WallMask;
    public Camera cam { private get; set; }
    [SerializeField] private float m_Distance = 50f;

    public Interactables currInteractable;
    private void Awake()
    {
        GameManager.GetManager().interactableManager = this;
        allInteractables = FindObjectsOfType<Interactables>().ToList();
    }

    private void Update()
    {
        if (GameManager.GetManager().gameStateController.m_CurrentStateGame != GameStateController.StateGame.GamePlay || currInteractable == null)
            return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            currInteractable.Interaction(1);
            currInteractable = null;
        }
        else if (Input.GetKeyDown(KeyCode.Q) && currInteractable.totalOptions > 1)
        {
            currInteractable.Interaction(2);
            currInteractable = null;
        }
        //
        //Ray l_Ray = GameManager.GetManager().cameraController.mainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

        //if (Physics.Raycast(l_Ray, out RaycastHit l_Hit, m_Distance, m_LayerMask))
        //{
        //    if (currInteractable == null)
        //        currInteractable = l_Hit.collider.gameObject.GetComponent<Interactables>();

        //    //if (currInteractable != null && currInteractable != lookingInteractable)
        //    //{
        //    //    if (currInteractable.totalOptions > 1 || !currInteractable.GetDone())
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

    public void LookingAnInteractable(Interactables interactables)
    {
        currInteractable = interactables;
    }
}
