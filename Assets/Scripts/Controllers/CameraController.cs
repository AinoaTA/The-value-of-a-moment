using Cinemachine;
using System.Collections;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Camera mainCamera;
    public bool invertY;
    public float lookSpeed = 1f;
    int defaultPriority = 0;
    int setPriority = 10;
    float yVal = 0, xVal = 0;

    public LayerMask layerMask;
    public LayerMask wallMask;
    public Camera cam { private get; set; }

    public CamerasConfigVirtual[] virtualCameras;
    private bool inTransition;
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
        inTransition = true;
        yield return new WaitForSeconds(1);
        inTransition = false;
    }
}
