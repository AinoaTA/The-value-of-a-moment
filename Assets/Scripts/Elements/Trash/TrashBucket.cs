using UnityEngine;

public class TrashBucket : Interactables
{
    public enum TypeBucket { CLOTHES, TRASH }
    public TypeBucket type = TypeBucket.TRASH;

    public override void Interaction(int optionNumber)
    {
        switch (optionNumber)
        {
            case 1:
                if (type == TypeBucket.CLOTHES)
                {
                    GameManager.GetManager().InventoryTrash.RemoveDirtyClothes();
                }
                else if (type == TypeBucket.TRASH)
                {
                    GameManager.GetManager().InventoryTrash.RemoveTrash();
                }
                break;
            default:
                break;
        }
    }

    public override void ShowCanvas()
    {
        if (type == TypeBucket.CLOTHES && GameManager.GetManager().InventoryTrash.CurrentDirtyClothes() <= 0)
            return;
        else if (type == TypeBucket.TRASH && GameManager.GetManager().InventoryTrash.CurrentTrash() <= 0)
            return;

        base.ShowCanvas();
    }

    public override void HideCanvas()
    {
        base.HideCanvas();
    }
}
