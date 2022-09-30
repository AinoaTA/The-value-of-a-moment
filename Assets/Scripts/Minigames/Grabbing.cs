using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabbing : MonoBehaviour
{
    public Camera cam;

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
    private Quaternion previousQuat;
    [SerializeField] private Transform target;
    [SerializeField] private float grabbingSpeed = 0.01f;
    private bool isObjectGrabbed = false;
    private bool leaving = false;

    // Start is called before the first frame update
    void Start()
    {
        previousPos = this.transform.position;
        previousQuat = this.transform.rotation;
        //cam = brain.ActiveVirtualCamera;
    }

    // Update is called once per frame
    void Update()
    {
        if (isObjectGrabbed && Input.GetKeyDown(KeyCode.Escape))
        {
            leaving = true;
            isObjectGrabbed = false;
            GameManager.GetManager().gameStateController.ChangeGameState(1);
            FMODUnity.RuntimeManager.PlayOneShot("event:/Env/Book/Drop", transform.position);
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
                FMODUnity.RuntimeManager.PlayOneShot("event:/Env/Book/Look", transform.position);
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
        GameManager.GetManager().gameStateController.ChangeGameState(2);
        GameManager.GetManager().canvasController.Pointer.SetActive(false);
        this.transform.position = Vector3.MoveTowards(this.transform.position, target.position, grabbingSpeed * Time.deltaTime);

        if (Vector3.Distance(this.transform.position, target.position) < 0.5f)
        {
            StartCoroutine(MakePlayerDissapear());
            isObjectGrabbed = true;
            once = false;
        }
    }

    private void LeaveObject()
    {
        GameManager.GetManager().canvasController.Pointer.SetActive(true);
        this.transform.position = Vector3.MoveTowards(this.transform.position, previousPos, grabbingSpeed * Time.deltaTime);
        this.transform.rotation = previousQuat;
        if (Vector3.Distance(this.transform.position, previousPos) < 0.1f)
        {
            cam.cullingMask = -1;
            isObjectGrabbed = false;
            once = true;
            leaving = false;
            SetAccessCamera(false);
        }
    }

    private IEnumerator MakePlayerDissapear()
    {
        yield return new WaitForSecondsRealtime(1.5f);
        cam.cullingMask &= ~(1 << LayerMask.NameToLayer("Player"));
    }

}
