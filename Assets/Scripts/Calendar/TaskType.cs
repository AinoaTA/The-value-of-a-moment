using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class TaskType : MonoBehaviour
{
    [Tooltip("1 - Work, 2 - Ocio, 3 - Clean")]
    public Color[] colors;
    public Color enterColor;
    public Interactables interactableAttached;
    private Image sprite;
    public enum Task { Work, Ocio, Clean }
    public Task task;
    public string nameTask;
    private TMP_Text text;
    Transform parentTransform;

    private void Awake()
    {
        parentTransform = gameObject.transform.parent;
        sprite = GetComponent<Image>();
        text = GetComponentInChildren<TMP_Text>();
    }
    private void Start()
    {
        sprite.color = colors[(int)task];
        text.text = nameTask;
    }

    public void ClicEnter()
    {
        sprite.color = enterColor;
    }

    public void ClicExit()
    {
        sprite.color = colors[(int)task];
    }

    public void SelectTask()
    {
      
        print("select begin drag");
        transform.SetParent(null);
        sprite.maskable = false;
    }

    public void DragTask()
    {
        transform.position = /*Camera.main.ScreenToWorldPoint(*/Input.mousePosition;/*);*/
        print("drag");
    }

    public void DropTask()
    {

        print("drop");
        transform.SetParent(parentTransform);
        sprite.maskable = true;
    }
}
