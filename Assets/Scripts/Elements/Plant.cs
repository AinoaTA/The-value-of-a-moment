using UnityEngine;

public class Plant : Interactables
{
    public bool water;

    //[HideInInspector] public string m_NameObject;
    //public string[] m_HelpPhrases;
    [SerializeField] private float distance;
    public WaterCan waterCan;
    Vector3 wateringInitialPos;

    private float timer;
    [SerializeField] private float maxTimer = 3f;
    private int currProcess=0;
    public GameObject[] m_process;

    private void Start()
    {
        //waterCan=GetComponent<WaterCan>();
        waterCan.gameObject.SetActive(false);
        m_process[currProcess].SetActive(true);
    }

    public override void Interaction()
    {
        if (!m_Done)
        {
            GameManager.GetManager().PlayerController.SetInteractable("Plant");
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


    public void NextDay()
    {
        //grow
        if (m_Done)
        {
            m_process[currProcess].SetActive(false);
            currProcess++;
            m_process[currProcess].SetActive(true);
        }
        else //else no grow. future option.
        { 
        

        
        }
    }


    //public override bool GetDone()
    //{
    //    return m_Done;
    //}

    //public override string[] GetPhrases()
    //{
    //    return m_HelpPhrases;
    //}


    //public override string NameAction()
    //{
    //    return m_NameObject;
    //}

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
