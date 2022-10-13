using UnityEngine;
using UnityEngine.Events;

public class Lock : MonoBehaviour, ILock
{
    public bool InteractableBlocked { get => _blocked; set => _blocked = value; }
    [SerializeField] bool _blocked;

    [SerializeField] private UnityEvent eventButton;

    public void Open() 
    {
        if (InteractableBlocked) return;
            eventButton?.Invoke();
    }
}
