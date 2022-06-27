using Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Camera MainCamera;

    public CinemachineVirtualCamera[] cameras;

    private void Start()
    {
        GameManager.GetManager().cameraController = this;
        GameManager.GetManager().cam = MainCamera;
    }
}
