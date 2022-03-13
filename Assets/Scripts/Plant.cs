using UnityEngine;

public class Plant : Interactables, IntfInteract
{
    public bool water;

    private bool m_Done;
    [HideInInspector] public string m_NameObject;
    public string[] m_HelpPhrases;
    [SerializeField] private float distance;
    public WaterCan waterCan;
    Vector3 wateringInitialPos;

    private float timer;
    [SerializeField] private float maxTimer = 3f;
    private void Start()
    {
        m_NameObject = "Regar";
        waterCan.GetComponent<WaterCan>();
        waterCan.gameObject.SetActive(false);
    }



    public void Interaction()
    {
        if (!m_Done)
        {
            timer = 0;
            GameManager.GetManager().m_CurrentStateGame = GameManager.StateGame.MiniGame;

            waterCan.gameObject.SetActive(true);
        }
        else
            FinishInteraction();

    }


    private void FinishInteraction()
    {
        m_Done = true;
        GameManager.GetManager().m_CurrentStateGame = GameManager.StateGame.GamePlay;
        GameManager.GetManager().PlayerController.ExitInteractable();

        waterCan.gameObject.SetActive(false);
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

    private void OnTriggerStay(Collider other)
    {
        if (!waterCan.dragg)
            return;

        print(timer);
        if (timer <= maxTimer)
            timer += Time.deltaTime;
        else
        {
            FinishInteraction();
        }
    }
}
