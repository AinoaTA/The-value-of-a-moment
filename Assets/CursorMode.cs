using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.UI;

public class CursorMode : PointerInputModule//InputSystemUIInputModule
{

    protected override void Start()
    {
        base.Start();
       
       // Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }


    protected override void ProcessMove(PointerEventData pointerEvent)
    {
        print("AE");
        var targetGO = (Cursor.lockState == CursorLockMode.Locked ? null : pointerEvent.pointerCurrentRaycast.gameObject);
        HandlePointerExitAndEnter(pointerEvent, targetGO);
    }

    public override void Process()
    {
       // throw new System.NotImplementedException();
    }

    // Current cursor lock state (memory cache)
    private CursorLockMode _currentLockState = CursorLockMode.None;

    /// <summary>
    /// Process the current tick for the module.
    /// </summary>
    //public override void Process()
    //{
    //    print("a");
    //    _currentLockState = Cursor.lockState;

    //    Cursor.lockState = CursorLockMode.None;

    //    base.Process();

    //    Cursor.lockState = _currentLockState;
    //}
}

