using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BlockController : MonoBehaviour
{
    public GameObject diaUno,diaDos;
    public Transform rotateAlexDoor, rotateAidaDoor;
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
        ToActive();
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
                UnlockingInteractables(dayOneInteractable, name);
                break;
            case DayController.Day.two:
                UnlockingInteractables(dayTwoInteractable, name);
                break;
            case DayController.Day.three:
                UnlockingInteractables(dayThreeInteractable, name);
                break;
            case DayController.Day.fourth:
                break;
            default:
                break;
        }
    }

    private void UnlockingInteractables(BlockInteractables[] dayInteractables, string name)
    {
        for (int i = 0; i < dayInteractables.Length; i++)
        {
            if (dayInteractables[i].name == name)
            {
                for (int e = 0; e < dayInteractables[i].locks.Length; e++)
                    dayInteractables[i].locks[e].GetComponent<ILock>().InteractableBlocked = false;

                return;
            }
        }
    }

    #region unlockall & also by daytime

    public void BlockAll(bool block)
    {
        switch (GameManager.GetManager().dayController.GetDayNumber())
        {
            case DayController.Day.one:
                for (int i = 0; i < dayOneInteractable.Length; i++)
                {
                    for (int e = 0; e < dayOneInteractable[i].locks.Length; e++)
                    {
                        print("name: " + dayOneInteractable[i] + " name obj: " + dayOneInteractable[i].locks[e]);
                        dayOneInteractable[i].locks[e].GetComponent<ILock>().InteractableBlocked = block;
                    }
                }
                break;
            case DayController.Day.two:
                for (int i = 0; i < dayTwoInteractable.Length; i++)
                {
                    for (int e = 0; e < dayTwoInteractable[i].locks.Length; e++)
                    {
                        print("name: " + dayTwoInteractable[i] + " name obj: " + dayTwoInteractable[i].locks[e]);
                        dayTwoInteractable[i].locks[e].GetComponent<ILock>().InteractableBlocked = block;
                    }
                }
                break;
            case DayController.Day.three:
                break;
            default:
                break;
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
    public void ToActive()
    {
        switch (GameManager.GetManager().dayController.GetDayNumber())
        {
            case DayController.Day.one:
                diaUno.SetActive(true);
                break;
            case DayController.Day.two:

                diaUno.SetActive(false);
                diaDos.SetActive(true);
                rotateAlexDoor.rotation = Quaternion.Euler(0, -100, 0);
                rotateAidaDoor.rotation = Quaternion.Euler(0, -100, 0);
                break;
            case DayController.Day.three:
                diaDos.SetActive(false);
                break;
            case DayController.Day.fourth:
                break;
            default:
                break;
        }
    }
}
