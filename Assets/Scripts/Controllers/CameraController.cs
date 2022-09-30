using Cinemachine;
using System.Collections;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Cinemachine")]
    [Tooltip("3D Camera Input")]
    [SerializeField] private CinemachineInputProvider cameraProvider;
    [SerializeField] private CinemachineBrain brain;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private bool invertY;
    [SerializeField] private float waitingBleendingTime = 1.75f;
    private int defaultPriority = 0;
    private int setPriority = 10;
    private float yVal = 0, xVal = 0;

    [Header("Others")]
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private LayerMask wallMask;

    [Space(10)]
    [Header("Cameras Registered")]
    [SerializeField] private CamerasConfigVirtual[] virtualCameras;
    [System.Serializable]
    public struct CamerasConfigVirtual
    {
        public string Name;
        [HideInInspector] public int ID; //maybe unnecessary parameter if we use camera's name to know what camera will be 
        public CinemachineVirtualCamera cameraType;
    }

    private void Awake()
    {
        GameManager.GetManager().cameraController = this;
        brain = mainCamera.GetComponent<CinemachineBrain>();
    }
    private void Start()
    {
        for (int i = 0; i < virtualCameras.Length; i++)
            virtualCameras[i].ID = i;

        GameManager.GetManager().playerInputs._CameraPitchDelta += CameraPitchDelta;
        GameManager.GetManager().playerInputs._CameraYawDelta += CameraYawDelta;
    }
    private void OnDisable()
    {
        GameManager.GetManager().playerInputs._CameraPitchDelta -= CameraPitchDelta;
        GameManager.GetManager().playerInputs._CameraYawDelta -= CameraYawDelta;
    }

    #region SetPriorities
    /// <summary>
    /// Giving int
    /// </summary>
    /// <param name="id"></param>
    private void SetPriorityCam(int id)
    {
        for (int v = 0; v < virtualCameras.Length; v++)
        {
            if (id == virtualCameras[v].ID)
                virtualCameras[v].cameraType.Priority = setPriority;
            else
                virtualCameras[v].cameraType.Priority = defaultPriority;
        }
        StartCoroutine(CameraSwitchDelay());
    }

    /// <summary>
    /// Giving string
    /// </summary>
    /// <param name="id"></param>
    private void SetPriorityCam(string id)
    {
        for (int v = 0; v < virtualCameras.Length; v++)
        {
            if (id == virtualCameras[v].Name)
                virtualCameras[v].cameraType.Priority = setPriority;
            else
                virtualCameras[v].cameraType.Priority = defaultPriority;
        }

        StartCoroutine(CameraSwitchDelay());
    }
    #endregion
    public int GetID(string name)
    {
        for (int v = 0; v < virtualCameras.Length; v++)
        {
            if (name == virtualCameras[v].Name)
                return virtualCameras[v].ID;
        }
        Debug.LogWarning("There is not a " + name + " Camera set in CamerasController. Maybe it doesn't need one");
        return 0; //3D Camera (Character's Camera)
    }

    /// <summary>
    /// Go back Character's Camera (ID = 0).
    /// </summary>
    public void ExitInteractCam()
    {
        SetPriorityCam(0);
    }

    /// <summary>
    /// Look CamerasManager to know camera IDs. 
    /// Write the ID to change priority on desired camera.
    /// </summary>
    /// <param name="ID"></param>
    public void StartInteractCam(int ID)
    {
        SetPriorityCam(ID);
    }
    public void StartInteractCam(string name)
    {
        SetPriorityCam(name);
    }

    #region CameraMovement
    private void CameraPitchDelta(float delta)
    {
        if (!GameManager.GetManager().gameStateController.CheckGameState(1))
            return;
        yVal = delta * Time.deltaTime;
    }
    private void CameraYawDelta(float delta)
    {
        if (!GameManager.GetManager().gameStateController.CheckGameState(1))
            return;

        xVal = delta * Time.deltaTime;
    }

    IEnumerator CameraSwitchDelay()
    {
        cameraProvider.enabled = false;
        yield return new WaitForSeconds(waitingBleendingTime);
        cameraProvider.enabled = true;
    }
    #endregion

    public void Block3DMovement(bool v) { cameraProvider.enabled = v; }
}
