using UnityEngine;

public class TrashBucket : Interactables
{
    public enum TypeBucket { CLOTHES, TRASH }
    public TypeBucket type = TypeBucket.TRASH;

    private int numberTrash;
    public int maxTras = 5;

    public override void Interaction(int optionNumber)
    {
        switch (optionNumber)
        {
            case 1:
                if (type == TypeBucket.CLOTHES)
                {
                    GameManager.GetManager().InventoryTrash.RemoveDirtyClothes(this);

                    if (numberTrash >= maxTras)
                    {
                        CheckDoneTask();
                    }
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

    public void SomethingCleaned()
    {
        numberTrash++;
        if (numberTrash >= maxTras)
        { 
        
        
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
