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
        autoconfidence = 20f;
        activated = false;
        maxAutoConfidence = 100f;
        //autoconfidence = GameManager.GetManager().autocontrol.GetAutcontrolValue();
        //Debug.Log(autoconfidence);
        float autoconfidenceFactor = autoconfidence / maxAutoConfidence;
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
