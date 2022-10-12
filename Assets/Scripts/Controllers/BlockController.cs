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

    [SerializeField] bool unlockAll;
    [System.Serializable]
    public struct BlockInteractables
    {
        public string name;
        public int id;
        public MonoBehaviour[] locks;
        public DayController.DayTime dayTimeCanUnlock;
        public bool hasConditionToUnlock;
    }
    bool stop;
    private void Awake()
    {
        GameManager.GetManager().blockController = this;
        if (unlockAll) return;
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

        //UnlockAll();
    }

    public void CheckUnlockInteractable(BlockInteractables[] list)
    {
        for (int i = 0; i < list.Length; i++)
        {
            if (!list[i].hasConditionToUnlock)
            {
                for (int e = 0; e < list[i].locks.Length; e++)
                {
                    ILock ilock;
                    var check = list[i].locks[e].TryGetComponent<ILock>(out ilock);
                    if (check)
                        ilock.InteractableBlocked = false;
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
                            dayOneInteractable[i].locks[e].GetComponent<ILock>().InteractableBlocked = false;

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
    }

    #region unlockall & also by daytime

    public void UnlockAll()
    {
        for (int i = 0; i < dayOneInteractable.Length; i++)
        {
            for (int e = 0; e < dayOneInteractable[i].locks.Length; e++)
            {
                print("name: "+ dayOneInteractable[i]+" name obj: "+dayOneInteractable[i].locks[e]);
                dayOneInteractable[i].locks[e].GetComponent<ILock>().InteractableBlocked = false;
            }
        }
    }


    public void UnlockAll(DayController.DayTime time)
    {
        for (int i = 0; i < dayOneInteractable.Length; i++)
        {
            for (int e = 0; e < dayOneInteractable[i].locks.Length; e++)
            {
                if (dayOneInteractable[i].dayTimeCanUnlock == time)
                    dayOneInteractable[i].locks[e].GetComponent<ILock>().InteractableBlocked = false;
            }
        }
    }
    #endregion

    public void LockSpecific(string name)
    {
        for (int i = 0; i < dayOneInteractable.Length; i++)
        {
            if (dayOneInteractable[i].name == name)
            {
                for (int e = 0; e < dayOneInteractable[i].locks.Length; e++)
                    dayOneInteractable[i].locks[e].GetComponent<ILock>().InteractableBlocked = true;
                return;
            }
        }
    }
}
