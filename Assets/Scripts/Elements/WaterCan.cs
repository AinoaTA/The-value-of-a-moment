using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterCan : MonoBehaviour
{
    public Vector3 offSet;
    private Vector3 offSetExit = new Vector3(0.5f, 0, 0.5f);
    public ParticleSystem particles;

    public ParticleSystem GrowUpParticle;

    [HideInInspector] public bool dragg;

    private bool grabbed = false;

    private void Start()
    {
        GameManager.GetManager().WaterCan = this;
    }

    public void Grab()
    {
        this.gameObject.SetActive(false);
        GameManager.GetManager().WaterCanGrabbed = true;
    }

    private void OnMouseDown()
    {
        particles.Play();
        GrowUpParticle.Stop();
    }

    private void OnMouseDrag()
    {
        dragg = true;

        if (!GrowUpParticle.isPlaying)
            GrowUpParticle.Play();

        //Necessary OffSet
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition + offSet);
    }

    private void OnMouseUp()
    {
        GrowUpParticle.Stop();
        dragg = false;
        particles.Stop();
    }

}
