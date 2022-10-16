using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalSceneBar : MonoBehaviour
{
    public Transform barPivot;
    public float scaleSpeed;

    private float autoconfidence, maxAutoConfidence;
    private bool activated;
    private Vector3 newScale;

    void Start()
    {
        activated = false;
        maxAutoConfidence = GameManager.GetManager().autocontrol.maxValue;
        autoconfidence = GameManager.GetManager().autocontrol.GetAutcontrolValue();
        float autoconfidenceFactor = autoconfidence / maxAutoConfidence;
        Debug.Log(autoconfidenceFactor);
        newScale = new Vector3(autoconfidenceFactor, 1f, 1f);
        ActivateAnim();
    }

    void Update()
    {
        if (activated)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, newScale, scaleSpeed * Time.deltaTime);
            //if (transform.localScale.magnitude == newScale.magnitude)
            //    activated = false;
        }
    }

    public void ActivateAnim()
    {
        activated = true;
    }
}
