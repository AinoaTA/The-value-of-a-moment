using UnityEngine;

public class BlockController : MonoBehaviour
{
    [Header("Interactables")]
    public BlockInteractables[] dayOneInteractable;
    public BlockInteractables[] dayTwoInteractable;
    public BlockInteractables[] dayThreeInteractable;

    [System.Serializable]
    public struct BlockInteractables
    {
        public string name;
        public int id;
        public ILock interactable;
        public DayController.DayTime dayTimeCanUnlock;
        public bool hasConditionToUnlock;
    }
    private void Awake()
    {
        CheckUnlockInteractable(dayOneInteractable);
    }

    public void CheckUnlockInteractable(BlockInteractables[] list) 
    {
        for (int i = 0; i < list.Length; i++)
        {
            if (!list[i].hasConditionToUnlock)
            {
                list[i].interactable.InteractableBlocked = false;
                list[i].id = i;
            }
        }
    }
}
