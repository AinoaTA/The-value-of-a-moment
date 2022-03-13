using UnityEngine;

public class Plant : Interactables, IntfInteract
{
    public bool water;

    private bool m_Done;
    [HideInInspector] public string m_NameObject;
    public string[] m_HelpPhrases;
    //public BedMinigame m_miniGame;
    [SerializeField] private float distance;
    public GameObject wateringCan;
    public float maxX, maxZ;
    Vector3 wateringInitialPos;



    private void Start()
    {
        m_NameObject = "Regar";
        wateringInitialPos = wateringCan.transform.position;
    }



    public void Interaction()
    {
        if (!m_Done) 
        {
            wateringCan.SetActive(true);

            float mouseX = Input.GetAxis("Mouse X")*Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y")*Time.deltaTime;

            Vector3 Xpos = new Vector3(wateringInitialPos.x + mouseX, 0, wateringInitialPos.z + mouseY);

            float XClamped = Mathf.Clamp(Xpos.x, Xpos.x - maxX, Xpos.x + maxX);
            float ZClamped = Mathf.Clamp(Xpos.z, Xpos.z - maxZ, Xpos.z + maxZ);

            wateringCan.transform.position += new Vector3(XClamped, wateringCan.transform.position.y, ZClamped);
        }
    }


    public float GetDistance()
    {
        return distance;
    }

    public bool GetDone()
    {
        return m_Done;
    }

    public string[] GetPhrases()
    {
        return m_HelpPhrases;
    }


    public string NameAction()
    {
        return m_NameObject;
    }
}
