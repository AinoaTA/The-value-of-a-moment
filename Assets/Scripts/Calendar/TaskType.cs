using UnityEngine;
using UnityEngine.UI;
using TMPro;
namespace Calendar
{
    public class TaskType : MonoBehaviour
    {
        public enum Task { Work, Ocio, Basic }
        public Task task;

        [Tooltip("1 - Work, 2 - Ocio, 3 - Clean, 4- AutoCuidado")]
        [SerializeField] private Sprite[] colors;
        [SerializeField] private Sprite completed;
        [SerializeField] private Transform content;
        [SerializeField] private Color enterColor;
        public Image sprite;
        public string nameTask;

        [HideInInspector] public bool InAnySpaceCalendar;
        [HideInInspector] public SpaceCalendar calendar;

        public delegate void TaskTypeDelegate(TaskType type);
        public static TaskTypeDelegate taskDelegate;

        private TMP_Text text;
        private Transform parentTransform;
        private Transform oldParent;
        private bool done;
        private bool modifiedCalendar => GameManager.GetManager().calendarController.modified;

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
            if (!done)
                sprite.color = enterColor;
        }

        public void ClicExit()
        {
            if (!done)
                sprite.color = Color.white;
        }

        public void SelectTask()
        {
            if (modifiedCalendar)
                return;
            oldParent = transform.parent;
            transform.SetParent(GameManager.GetManager().calendarController.TaskMovement);
        }

        public void DragTask()
        {
            if (modifiedCalendar)
                return;
            transform.position = Input.mousePosition;
        }

        public void DropTask()
        {
            if (modifiedCalendar)
                return;

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
            sprite.sprite = completed;
        }
        public void ResetTask()
        {
            transform.SetParent(content);
            sprite.sprite = colors[(int)task];
            sprite.maskable = true;
        }
    }
}
