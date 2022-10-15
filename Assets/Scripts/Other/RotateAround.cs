using UnityEngine;

public class RotateAround : MonoBehaviour
{
    public Transform point;
    public float rotationSpeed;

    private Quaternion rotation;

    // Start is called before the first frame update
    void Start()
    {
        rotation = transform.rotation;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.RotateAround(point.position, Vector3.up, rotationSpeed * Time.fixedDeltaTime);
        transform.rotation = rotation;
    }
}
