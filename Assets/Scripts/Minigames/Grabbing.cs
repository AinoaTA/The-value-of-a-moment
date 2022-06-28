using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabbing : MonoBehaviour
{
    public CinemachineBrain brain;
    private ICinemachineCamera cam;

    [Header("Mouse Sensitivity")]
    [Range(0.01f, 3f)]
    public float MouseSensitivity = 1.0f;

    [Header("Camera Movement")]
    public float turningSpeed = 50.0f;  // The speed of movement of the camera
    public bool yAxisInverted = false;
    public float maxYCameraAngle = 90.0f;
    public float minYCameraAngle = 50.0f;
    private float cameraPitch = 0.0f;
    public bool canAccesCamera = false;
    private bool once = true;
    private Vector3 previousPos;
    [SerializeField] private Transform target;
    [SerializeField] private float grabbingSpeed = 0.01f;
    private bool isObjectGrabbed = false;
    private bool leaving = false;

    // Start is called before the first frame update
    void Start()
    {
        previousPos = this.transform.position;
        //cam = brain.ActiveVirtualCamera;
    }

    // Update is called once per frame
    void Update()
    {
        if (isObjectGrabbed && Input.GetKeyDown(KeyCode.Escape))
        {
            leaving = true;
            isObjectGrabbed = false;
            GameManager.GetManager().m_CurrentStateGame = GameManager.StateGame.MiniGame;
            GameManager.GetManager().PlayerController.ExitInteractable();
        }

        if (leaving)
            LeaveObject();

        if(canAccesCamera)
        {
            if(!isObjectGrabbed && once)
                GrabObject();

            Vector2 MouseRot;
            if (yAxisInverted)
            {
                MouseRot = new Vector2(Input.GetAxis("Mouse X"), -Input.GetAxis("Mouse Y")) * Time.deltaTime * Time.timeScale;
            }
            else
            {
                MouseRot = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")) * Time.deltaTime * Time.timeScale;
            }
            transform.Rotate(0, MouseRot.x * turningSpeed * MouseSensitivity, 0);

            cameraPitch += Mathf.Clamp(MouseRot.y * turningSpeed * MouseSensitivity, -minYCameraAngle - cameraPitch, maxYCameraAngle - cameraPitch);
            //cam.transform.localRotation = Quaternion.Euler(-cameraPitch, 0.0f, 0.0f);
        }
    }
    
    public void SetAccessCamera(bool value)
    {
        canAccesCamera = value;
    }

    private void GrabObject()
    {
        this.transform.position = Vector3.MoveTowards(this.transform.position, target.position, grabbingSpeed * Time.deltaTime);

        if (Vector3.Distance(this.transform.position, target.position) < 0.5f)
        {
            isObjectGrabbed = true;
            once = false;
        }
    }

    private void LeaveObject()
    {
        this.transform.position = Vector3.MoveTowards(this.transform.position, previousPos, grabbingSpeed * Time.deltaTime);

        if (Vector3.Distance(this.transform.position, previousPos) < 0.5f)
        {
            isObjectGrabbed = false;
            once = true;
            leaving = false;
            SetAccessCamera(false);
        }
    }
}
