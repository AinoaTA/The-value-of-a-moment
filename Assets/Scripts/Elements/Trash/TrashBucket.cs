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
        GameManager.GetManager().Autocontrol.AddAutoControl(m_MinAutoControl);
        numberTrash++;
        if (numberTrash >= maxTras)
        {
            CheckDoneTask();
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
    public override void ResetInteractable()
    {
        numberTrash = 0;
    }
    public override void HideCanvas()
    {
        base.HideCanvas();
    }
}
