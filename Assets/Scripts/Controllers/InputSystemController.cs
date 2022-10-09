using UnityEngine;
using UnityEngine.InputSystem;

public class InputSystemController : MonoBehaviour
{
    PlayerInput playerInput;
    private void Awake()
    {
        playerInput = FindObjectOfType<PlayerInput>();
    }
    public void SwitchToMenuActionMap()
    {
        playerInput.SwitchCurrentActionMap("Menu");
    }
    //public void SwitchToActionMapPauseMenu()
    //{
    //    playerInput.SwitchCurrentActionMap("PauseMenu");
    //}
    public void SwitchToPlayerActionMap()
    {
        playerInput.SwitchCurrentActionMap("Player");
    }
}
