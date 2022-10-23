using UnityEngine;

public class CanvasLookAt : MonoBehaviour
{
    Quaternion lookRotation;
    Camera cam;
    Vector3 defaultPosition;
    bool isThis = false;
    GameObject canvasInteract;

    private void Awake()
    {
        if (this.gameObject.name == "Canvas(Gato)")
        {
            isThis = true;
            defaultPosition = new Vector3(0f, 0.5f, 0f);
            canvasInteract = this.transform.GetChild(0).gameObject;
        }
    }

    private void Start()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        if (gameObject.activeSelf)
        {
            if (isThis)
            {
                this.transform.localPosition = defaultPosition;
                canvasInteract.transform.localPosition = Vector3.zero;
            }
            lookRotation = cam.transform.rotation;
            transform.rotation = lookRotation;
        }
    }
}
