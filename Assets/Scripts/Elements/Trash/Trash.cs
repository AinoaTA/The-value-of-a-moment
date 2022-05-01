using UnityEngine;

public class Trash : Interactables
{
    public override void Interaction(int optionNumber)
    {
        switch (optionNumber)
        {
            case 1:
                GameManager.GetManager().InventoryTrash.AddTrash();
                gameObject.SetActive(false);
                break;
        }
    }
}
