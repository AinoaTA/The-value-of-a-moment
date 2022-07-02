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

    private void Awake()
    {
        GameManager.GetManager().cameraController = this;
        GameManager.GetManager().cam = MainCamera;
    }
    private void Start()
    {
        for (int i = 0; i < virtualCameras.Length; i++)
        {
            virtualCameras[i].ID = i;
        }
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
                Debug.Log("Camera ID " + name + " set");
                return virtualCameras[v].ID;
            }
        }
        Debug.Log("There is not a " + name + " Camera set in CamerasController");
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
}
