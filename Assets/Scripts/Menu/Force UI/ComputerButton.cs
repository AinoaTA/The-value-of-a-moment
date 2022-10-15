using UnityEngine;
using UnityEngine.Events;
public class ComputerButton : MonoBehaviour, ILock
{
    public bool InteractableBlocked { get => _blocked; set => _blocked = value; }
    [SerializeField] bool _blocked;

    [SerializeField] private Color[] colors;
    [SerializeField] private Vector3 defaultScale;
    [SerializeField] private float scale;
    private SpriteRenderer sprite;
    [Space(20)]

    [SerializeField] private UnityEvent eventButton;

    private void Awake()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
        defaultScale = sprite.transform.localScale;
        scale = defaultScale.x * 0.1f;
    }

    private void OnMouseDown()
    {
        if (InteractableBlocked) return;

        if (eventButton != null)
            eventButton?.Invoke();
        else
            Debug.LogWarning("There is not event attached to this button " + name);
    }

    private void OnMouseOver()
    {
        sprite.transform.localScale = new Vector3(scale + defaultScale.x, scale + defaultScale.y);
    }

    private void OnMouseExit()
    {
        sprite.transform.localScale = defaultScale;
    }

}
