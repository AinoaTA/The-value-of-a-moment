using UnityEngine;

public class TrisCounter : MonoBehaviour
{
    void Start()
    {
        foreach (GameObject g in UnityEngine.SceneManagement.SceneManager.GetActiveScene().GetRootGameObjects())
        {
            int triangleCount = 0;
            foreach (MeshFilter m in transform.GetComponentsInChildren(typeof(MeshFilter)))
            {
                if (m.mesh.isReadable)
                    triangleCount += m.mesh.triangles.Length;//m.mesh.triangles;
            }
            Debug.Log(g.name + " has " + triangleCount.ToString() + " triangles");
        }
    }
}
