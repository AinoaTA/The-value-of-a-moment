using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class PlayerHandleInputs : MonoBehaviour
{
    public event Action _FirstInteraction, _SecondInteraction, _StopMoving, _ResetMove,
        _MoveUp, _MoveDown, _MoveRight, _MoveLeft, _StartDay, _BackDay;

    public event Action<float> _CameraPitchDelta, _CameraYawDelta;

    private void Awake()
    {
        GameManager.GetManager().playerInputs = this;
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        Vector2 l_MovementAxis = context.ReadValue<Vector2>();
        if (l_MovementAxis == Vector2.zero)
        {
            _StopMoving?.Invoke();
        }
        else
        {
            _ResetMove?.Invoke();
            if (l_MovementAxis.y > 0)
            {
                _MoveUp?.Invoke();
            }
            else if (l_MovementAxis.y < 0)
            {
                _MoveDown?.Invoke();
            }
            if (l_MovementAxis.x > 0)
            {
                _MoveRight?.Invoke();
            }
            else if (l_MovementAxis.x < 0)
            {
                _MoveLeft?.Invoke();
            }
        }
    }

    public void OnFirstInteract(InputAction.CallbackContext context)
    {
        switch (context)
        {
            case var value when context.started:
                _FirstInteraction?.Invoke();
                break;
        }
    }

    public void OnSecondInteract(InputAction.CallbackContext context)
    {
        switch (context)
        {
            case var value when context.started:
                _SecondInteraction?.Invoke();
                break;
        }
    }

    public void OnStartAlarmInteract(InputAction.CallbackContext context)
    {
        switch (context)
        {
            case var value when context.started:
                _StartDay?.Invoke();
                break;
        }
    }

    public void OnBackAlarmInteract(InputAction.CallbackContext context)
    {
        switch (context)
        {
            case var value when context.started:
                _BackDay?.Invoke();
                break;
        }
    }

    public void OnCameraDelta(InputAction.CallbackContext context)
    {
        Vector2 l_CameraDelta = context.ReadValue<Vector2>();
        _CameraPitchDelta?.Invoke(l_CameraDelta.x);
        _CameraYawDelta?.Invoke(l_CameraDelta.y);
    }
}
