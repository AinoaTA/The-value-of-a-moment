using UnityEngine;

public class CanvasLookAt : MonoBehaviour
{
    Quaternion lookRotation;
    Camera cam;

    private void Start()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        if (gameObject.activeSelf)
        {
            lookRotation = cam.transform.rotation;
            transform.rotation = lookRotation;
        }
    }
}
