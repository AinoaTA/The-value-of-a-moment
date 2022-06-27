using UnityEngine;

public class Trash : Interactables
{
    public enum TrashType { CLOTHES, TRASH }
    public TrashType type = TrashType.TRASH;

    //5 prendas de ropas
     private int numberTrash;
    public int maxTras=5;
    public override void Interaction(int optionNumber)
    {
        switch (optionNumber)
        {
            case 1:

                if (type == TrashType.TRASH)
                    GameManager.GetManager().InventoryTrash.AddTrash(this);
                else
                    GameManager.GetManager().InventoryTrash.AddDirtyClothes(this);

                gameObject.SetActive(false);
                break;
        }
    }

    public override void ResetInteractable()
    {
        numberTrash = 0;
        m_Done = false;
    }
}
