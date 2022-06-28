using System.Collections;
using UnityEngine;

public class Trash : Interactables
{
    public enum TrashType { CLOTHES, TRASH }
    public TrashType type = TrashType.TRASH;
    // 5 prendas de ropa
    public int maxTras = 5;
    public Transform target;

    private int numberTrash;
    private float grabbingSpeed = 10f;
    private bool grabbing = false;
    
    private void Start()
    {
        GameManager.GetManager().trashes.Add(this);
    }

    public override void Interaction(int optionNumber)
    {
        switch (optionNumber)
        {
            case 1:
                grabbing = true;
                break;
        }
    }

    private void Update()
    {
        if(grabbing)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, target.position, grabbingSpeed * Time.deltaTime);

            if(Vector3.Distance(this.transform.position, target.position) < 0.1f)
            {
                if (type == TrashType.TRASH)
                    GameManager.GetManager().InventoryTrash.AddTrash(this);
                else
                    GameManager.GetManager().InventoryTrash.AddDirtyClothes(this);

                this.gameObject.SetActive(false);
            }
        }
    }

    public override void ResetInteractable()
    {
        grabbing = false;
        numberTrash = 0;
        m_Done = false;
    }
}
