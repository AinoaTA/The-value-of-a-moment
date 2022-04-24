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
    private bool started;


    private void Start()
    {
        GameManager.GetManager().plants.Add(this);

        if(waterCan != null) waterCan.gameObject.SetActive(false);
           m_process[currProcess].SetActive(true);
    }

    public override void Interaction(int optionsNumber)
    {
        switch (optionsNumber)
        {
            case 1:

                if (!m_Done)
                {
                    started = true;
                    timer = 0;
                    GameManager.GetManager().PlayerController.SetInteractable("Plant");
                    GameManager.GetManager().m_CurrentStateGame = GameManager.StateGame.MiniGame;
                    GameManager.GetManager().CanvasManager.UnLock();

                    waterCan.gameObject.SetActive(true);
                }
                else
                    FinishInteraction();

                break;
            default:
                break;
        } 
    }

    private void Update()
    {
        if (started && Input.GetKeyDown(KeyCode.Escape))
        {
            GameManager.GetManager().PlayerController.ExitInteractable();
            GameManager.GetManager().m_CurrentStateGame = GameManager.StateGame.GamePlay;
            GameManager.GetManager().CanvasManager.Lock();
            GameManager.GetManager().Autocontrol.AddAutoControl(m_MinAutoControl);
            started = false;
            waterCan.gameObject.SetActive(false);
        }
    }


    private void FinishInteraction()
    {
        GameManager.GetManager().PlayerController.ExitInteractable();
        GameManager.GetManager().m_CurrentStateGame = GameManager.StateGame.GamePlay;
        GameManager.GetManager().CanvasManager.Lock();
        GameManager.GetManager().Autocontrol.AddAutoControl(m_MinAutoControl);
        m_Done = true;
        started = false;
        //GameManager.GetManager().m_CurrentStateGame = GameManager.StateGame.GamePlay;
        //GameManager.GetManager().PlayerController.ExitInteractable();
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
        //else //else no grow. future option.
        //{ 
        

        
        //}
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

