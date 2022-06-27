using UnityEngine;

public class Trash : Interactables
{
    public enum TrashType { CLOTHES, TRASH }
    public TrashType type = TrashType.TRASH;

    private int numberTrash;
    public int maxTras=5; //5 prendas de ropas

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


    public void Cleaned()
    {
        GameManager.GetManager().Autocontrol.AddAutoControl(4);
        numberTrash++;

        if (numberTrash >= maxTras)
        {
            m_Done = true;
            CheckDoneTask();
        }
    }

    public override void ResetInteractable()
    {
        numberTrash = 0;
        m_Done = false;
    }
}
