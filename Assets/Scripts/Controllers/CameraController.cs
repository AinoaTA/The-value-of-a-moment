using Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Camera MainCamera;
    int defaultPriority = 0;
    int setPriority = 10;

    public CamerasConfigVirtual[] virtualCameras;
    [System.Serializable]
    public struct CamerasConfigVirtual
    {
        public string Name;
        public int ID;
        public CinemachineVirtualCamera cameraType;
    }

    private void Start()
    {
        GameManager.GetManager().cameraController = this;
        GameManager.GetManager().cam = MainCamera;
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

    /// <summary>
    /// Set ID 0 because is player (3D) camera
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
}
