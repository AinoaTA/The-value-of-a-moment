using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrisCounter : MonoBehaviour
{


    void Start()
    {
        foreach (Transform g in transform)//GameObject g in UnityEngine.SceneManagement.SceneManager.GetActiveScene().GetRootGameObjects())
        {
            int triangleCount = 0;
            foreach (MeshFilter m in transform.GetComponentsInChildren(typeof(MeshFilter)))
            {
                triangleCount += m.mesh.triangles.Length;//m.mesh.triangles;
            }
            Debug.Log(g.name + " has " + triangleCount.ToString() + " triangles");
        }
    }
}
