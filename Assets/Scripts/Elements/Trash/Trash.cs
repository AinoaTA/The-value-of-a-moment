using UnityEngine;

public class Trash : Interactables
{
    public enum TrashType { CLOTHES, TRASH }
    public TrashType type = TrashType.TRASH;

    public override void Interaction(int optionNumber)
    {
        switch (optionNumber)
        {
            case 1:

                if (type == TrashType.TRASH)
                    GameManager.GetManager().InventoryTrash.AddTrash();
                else
                    GameManager.GetManager().InventoryTrash.AddDirtyClothes();

                gameObject.SetActive(false);
                break;
        }
    }
}
