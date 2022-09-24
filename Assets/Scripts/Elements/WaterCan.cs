using UnityEngine;
using System.Collections;

public class WaterCan : MonoBehaviour
{
    public Vector3 startPos;
    public Vector3 offSet;
    private Vector3 offSetExit = new Vector3(0.5f, 0f, 0.5f);
    public Vector3 clamp;
    public ParticleSystem particles;
    //public bool grabbed;

    public ParticleSystem GrowUpParticle;

    [HideInInspector] public bool dragg;

    private static FMOD.Studio.EventInstance waterplant;

    private void Start()
    {
        waterplant = FMODUnity.RuntimeManager.CreateInstance("event:/Env/Water Plant");
        dragg = false;
        startPos = transform.position;
    }

    private void OnMouseDown()
    {
        waterplant.start();
        waterplant.release();
        particles.Play();
        GrowUpParticle.Stop();
    }

    private void OnMouseDrag()
    {
        dragg = true;

        if (!GrowUpParticle.isPlaying)
        {
            GrowUpParticle.Play();
        }

        //Necessary OffSet
        Vector3 newPos = Camera.main.ScreenToWorldPoint(Input.mousePosition + offSetExit);
        float x = Mathf.Clamp(newPos.x, startPos.x - 0.3f, startPos.x + 0.3f);
        float z = Mathf.Clamp(newPos.z, startPos.z - 0.3f, startPos.z + 0.3f);
        transform.position = new Vector3(x, newPos.y, z);
    }

    private void OnMouseUp()
    {
        GrowUpParticle.Stop();
        dragg = false;
        particles.Stop();
    }


    public void ResetWaterCan()
    {
        waterplant.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        dragg = false;
        particles.Stop();
        transform.position = startPos;
    }
}
