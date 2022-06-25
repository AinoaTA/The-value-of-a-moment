
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Camera MainCamera;

    private void Start()
    {
        GameManager.GetManager().cameraController = this;
        GameManager.GetManager().cam = MainCamera;
    }
}
