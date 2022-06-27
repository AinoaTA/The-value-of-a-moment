using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class TaskType : MonoBehaviour
{
    [Tooltip("1 - Work, 2 - Ocio, 3 - Clean, 4- AutoCuidado")]
    public Sprite[] colors;
    public Color enterColor;
    [HideInInspector]public Image sprite;
    public Sprite completed;
    public enum Task { Work, Ocio, Basic}
    public Task task;
    public string nameTask;
    private TMP_Text text;
    public Transform content;
    Transform parentTransform;
    bool done;

    public delegate void TaskTypeDelegate(TaskType type);
    public static TaskTypeDelegate taskDelegate;
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
        sprite.sprite = colors[(int)task];
    }
    public void ClicEnter()
    {
        if(!done)
        sprite.color = enterColor;
    }

    public void ClicExit()
    {
        if (!done)
            sprite.color = Color.white;//colors[(int)task];
    }

    public void SelectTask()
    { 
        oldParent = transform.parent;
        transform.SetParent(GameManager.GetManager().calendarController.TaskMovement);
    }

    public void DragTask()
    {
        transform.position = Input.mousePosition; 
    }

    public void DropTask()
    {
        if (!InAnySpaceCalendar)
        { 
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
        print("uy");
        sprite.sprite = completed;
        //sprite.color = Color.green;
        //taskDelegate?.Invoke(this);
    }
    public void ResetTask()
    {
        transform.SetParent(content);
        sprite.sprite = colors[(int)task];
        sprite.maskable = true;
    }
}
