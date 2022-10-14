using System.Collections;
using UnityEngine;

public class DrumInstrument : MonoBehaviour
{
    public Material mouseOverMaterial;
    public Material rightMaterial;
    public Material wrongMaterial;
    public float mantainStateTime = 1;

    public MeshRenderer mesh;
    Material normalMaterial;
    Collider instrumentCollider;
    bool specialState = false;
    bool mouseOver = false;
    IEnumerator restoreCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        instrumentCollider = GetComponent<Collider>();
        normalMaterial = mesh.material;
    }

    public void Enable(bool enabled)
    {
        instrumentCollider.enabled = enabled;
        if (!enabled)
        {
            mouseOver = false;
            if (specialState) return;
            mesh.material = normalMaterial;
        }
    }

    public void MouseOver(bool mouseOver)
    {
        this.mouseOver = mouseOver;
        if (specialState) return;
        mesh.material = mouseOver ? mouseOverMaterial : normalMaterial;
    }

    public void SetWrong()
    {
        mesh.material = wrongMaterial;
        specialState = true;
    }


    public void SetRight()
    {
        mesh.material = rightMaterial;
        specialState = true;
    }

    public void Restore()
    {
        specialState = false;
        mesh.material = mouseOver ? mouseOverMaterial : normalMaterial;
    }
}
