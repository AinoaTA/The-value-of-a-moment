using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Collections;

public class BlockController : MonoBehaviour
{
    [Header("Interactables")]
    public BlockInteractables[] dayOneInteractable;
    public BlockInteractables[] dayTwoInteractable;
    public BlockInteractables[] dayThreeInteractable;

    [SerializeField]
    private List<ILock> locks = new List<ILock>();

    [System.Serializable]
    public struct BlockInteractables
    {
        public string name;
        public int id;
        //public Interactables[] interactable;
        //public GeneralActions[] actions;

        public MonoBehaviour[] locks;
        public DayController.DayTime dayTimeCanUnlock;
        public bool hasConditionToUnlock;
    }
    bool stop;
    private void Awake()
    {
        GameManager.GetManager().blockController = this;

        stop = true;
        //super guarro I know.
        List<MonoBehaviour> a = FindObjectsOfType<MonoBehaviour>().ToList();
        for (int i = 0; i < a.Count; i++)
        {
            if (a[i].GetComponent<ILock>() != null)
                locks.Add(a[i].GetComponent<ILock>());
        }
        for (int i = 0; i < locks.Count; i++)
            locks[i].InteractableBlocked = true;

        print(locks.Count);
        stop = false;
    }
    IEnumerator Start()
    {
        yield return new WaitWhile(() => stop);
        CheckUnlockInteractable(dayOneInteractable);

    }

    public void CheckUnlockInteractable(BlockInteractables[] list)
    {
        for (int i = 0; i < list.Length; i++)
        {
            if (!list[i].hasConditionToUnlock)
            {
                for (int e = 0; e < list[i].locks.Length; e++)
                {
                    list[i].locks[e].GetComponent<ILock>().InteractableBlocked = false;
                }
                list[i].id = i;
            }
        }
    }

    public void Unlock(string name) 
    {
        
        switch (GameManager.GetManager().dayController.GetDayNumber())
        {
            case DayController.Day.one:
                for (int i = 0; i < dayOneInteractable.Length; i++)
                {
                    if (dayOneInteractable[i].name == name)
                    {
                        for (int e = 0; e < dayOneInteractable[i].locks.Length; e++)
                        {
                            dayOneInteractable[i].locks[e].GetComponent<ILock>().InteractableBlocked = false;
                        }
                        return;
                    }
                }
                break;
            case DayController.Day.two:
                break;
            case DayController.Day.three:
                break;
            case DayController.Day.fourth:
                break;
            default:
                break;
        }

        for (int i = 0; i < dayOneInteractable.Length; i++)
        {

        }

    }
}
