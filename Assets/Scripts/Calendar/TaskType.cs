using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class TaskType : MonoBehaviour
{
    [Tooltip("1 - Work, 2 - Ocio, 3 - Clean, 4- AutoCuidado")]
    public Color[] colors;
    public Color enterColor;
    public Interactables interactableAttached;
    private Image sprite;
    public enum Task { Work, Ocio, Clean, Autocuidado }
    public Task task;
    public string nameTask;
    private TMP_Text text;
    public Transform content;
    Transform parentTransform;
    bool done;

    [HideInInspector] public bool InAnySpaceCalendar;
    [HideInInspector] public SpaceCalendar calendar;
    private Transform oldParent;
    private void Awake()
    {
        parentTransform = gameObject.transform.parent;
        sprite = GetComponent<Image>();
        text = GetComponentInChildren<TMP_Text>();
    }
    private void Start()
    {
        text.text = nameTask;
        interactableAttached.taskAssociated = this;
        sprite.color = colors[(int)task];
    }
    public void ClicEnter()
    {
        if(!done)
        sprite.color = enterColor;
    }

    public void ClicExit()
    {
        if (!done)
            sprite.color = colors[(int)task];
    }

    public void SelectTask()
    {
        print("select begin drag");
        oldParent = transform.parent;
        transform.SetParent(GameManager.GetManager().calendarController.TaskMovement);
    }

    public void DragTask()
    {
        transform.position = Input.mousePosition;
        print("drag");
    }

    public void DropTask()
    {
        if (!InAnySpaceCalendar)
        {
            print("drop no calendar");
            transform.SetParent(parentTransform);
            sprite.maskable = true;
        }
        else if (calendar.ThereIsSpace())
        {
            transform.SetParent(calendar.transform);
        }
        else
        {
            transform.SetParent(oldParent);
        }
    }

    public void Done()
    {
        sprite.color = Color.green;
    }
    public void ResetTask()
    {
        transform.SetParent(content);
        sprite.color = colors[(int)task];
        sprite.maskable = true;
    }
}
