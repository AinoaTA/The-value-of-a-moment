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
            //currInteractable = null;
        }
        else if (Input.GetKeyDown(KeyCode.Q) && currInteractable.totalOptions > 1)
        {
            currInteractable.Interaction(2);
            //currInteractable = null;
        }
    }

    public void LookingAnInteractable(Interactables interactables)
    {
        currInteractable = interactables;
    }
}
