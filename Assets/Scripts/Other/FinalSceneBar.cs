using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FinalSceneBar : MonoBehaviour
{
    public Transform barPivot;
    public float scaleSpeed;

    private float autoconfidence;
    private bool activated = false;
    private Vector3 newScale;
    public TextMeshProUGUI textMesh;

    void Update()
    {
        if (activated)
        {
            barPivot.transform.localScale = Vector3.Lerp(barPivot.transform.localScale, newScale, scaleSpeed * Time.deltaTime);
            //if (transform.localScale.magnitude == newScale.magnitude)
            //    activated = false;
        }
    }

    public void ActivateAnim()
    {
        autoconfidence = GameManager.GetManager().autocontrol.GetAutcontrolValue();
        autoconfidence = 0.5f;
        newScale = new Vector3(autoconfidence, barPivot.transform.localScale.y, barPivot.transform.localScale.z);
        SetFinalText();
        activated = true;
    }

    private void SetFinalText()
    {
        if      (autoconfidence < 0.2f) textMesh.text = "Inténtalo la próxima vez";
        else if (autoconfidence < 0.5f) textMesh.text = "Sigue así";
        else if (autoconfidence < 0.8f) textMesh.text = "Muy buen trabajo";
        else                            textMesh.text = "¡Increíble!";
    }
}
