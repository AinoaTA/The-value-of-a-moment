using UnityEngine;

public class MirrorDistorsion : MonoBehaviour
{
    [SerializeField]
    private Camera mirrorCamera;
    [SerializeField]
    private Texture reflexTexture;

    [Range(60f, 140f)]
    public float fov;

    void Update()
    {
        setCameraFOV(fov);
    }

    private void setCameraFOV(float fov)
    {
        mirrorCamera.fieldOfView = fov;
    }

    private float getCameraFOV()
    {
        return mirrorCamera.fieldOfView;
    }


    private void setTextureTexelSize(Vector2 texelSize)
    {
        //reflexTexture.texelSize = texelSize;
    }

    private Vector2 getTextureTexelSize()
    {
        return reflexTexture.texelSize;
    }

}
