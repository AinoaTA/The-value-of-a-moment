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
    [SerializeField] private float lookSpeed = 1f;
    [SerializeField] private float waitingBleendingTime = 1.75f;
    private int defaultPriority = 0;
    private int setPriority = 10;
    private float yVal = 0, xVal = 0;

    [Header("Others")]
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private LayerMask wallMask;
    public Camera cam { private get; set; }

    [Space(10)]
    [Header("Cameras Registered")]
    [SerializeField] private CamerasConfigVirtual[] virtualCameras;
    [System.Serializable]
    public struct CamerasConfigVirtual
    {
        public string Name;
        public int ID;
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

    private void SetPriorityCam(int id)
    {
        for (int v = 0; v < virtualCameras.Length; v++)
        {
            if (id == virtualCameras[v].ID)
            {
                virtualCameras[v].cameraType.Priority = setPriority;
            }
            else
                virtualCameras[v].cameraType.Priority = defaultPriority;
        }

        StartCoroutine(CameraSwitchDelay());
    }

    public int GetID(string name)
    {
        for (int v = 0; v < virtualCameras.Length; v++)
        {
            if (name == virtualCameras[v].Name)
            {
                return virtualCameras[v].ID;
            }
        }
        Debug.LogWarning("There is not a " + name + " Camera set in CamerasController");
        return 0;
    }

    /// <summary>
    /// ID 0 by default because is player (3D) camera.
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

    private void CameraPitchDelta(float delta)
    {
        if (GameManager.GetManager().gameStateController.m_CurrentStateGame != GameStateController.StateGame.GamePlay)
            return;
        yVal = delta * Time.deltaTime;
    }
    private void CameraYawDelta(float delta)
    {
        if (GameManager.GetManager().gameStateController.m_CurrentStateGame != GameStateController.StateGame.GamePlay)
            return;

        xVal = delta * Time.deltaTime;
    }
    IEnumerator CameraSwitchDelay()
    {
        cameraProvider.enabled = false;
        yield return new WaitForSeconds(waitingBleendingTime);
        cameraProvider.enabled = true;
    }
}
