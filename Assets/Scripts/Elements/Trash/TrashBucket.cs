using UnityEngine;

public class TrashBucket : Interactables
{
    [SerializeField] private enum TypeBucket { CLOTHES, TRASH }
    [SerializeField] private TypeBucket type = TypeBucket.TRASH;

    private int currCapacity;
    [SerializeField]private int maxCapacity = 5;

    public override void Interaction(int optionNumber)
    {
        switch (optionNumber)
        {
            case 1:
                if (type == TypeBucket.CLOTHES)
                {
                    GameManager.GetManager().trashInventory.RemoveDirtyClothes(this);
                }
                else if (type == TypeBucket.TRASH)
                {
                    GameManager.GetManager().trashInventory.RemoveTrash();
                }
                break;
            default:
                break;
        }
    }

    public void SomethingCleaned()
    {
        GameManager.GetManager().autocontrol.AddAutoControl(m_MinAutoControl);
        currCapacity++;
        if (currCapacity >= maxCapacity)
        {
            GameManager.GetManager().dayNightCycle.TaskDone();
            CheckDoneTask();
        }
    }

    public override void ResetInteractable()
    {
        currCapacity = 0;
    }
}
