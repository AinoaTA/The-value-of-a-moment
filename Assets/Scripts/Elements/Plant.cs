using System.Collections;
using UnityEngine;

public class Plant : Interactables
{
    public bool water;
    public GameObject m_Tutorial;
    private GameObject minigameCanvas = null;

    [SerializeField] private float distance;
    public WaterCan waterCan;
    Vector3 wateringInitialPos;

    [SerializeField] private float timer;
    [SerializeField] private float maxTimer = 3f;
    private int currProcess = 0;
    public GameObject[] m_process;
    private bool started;
    public Regadera regadera;
    private bool tutorialShowed = false;

    private void Start()
    {
        minigameCanvas = m_Tutorial.transform.parent.gameObject;
        minigameCanvas.SetActive(false);
        GameManager.GetManager().Plants.Add(this);

        if(waterCan != null) waterCan.gameObject.SetActive(false);
            m_process[currProcess].SetActive(true);
    }

    public override void Interaction(int options)
    {
        switch (options)
        {
            case 1:
                if (!m_Done && regadera.grabbed)
                {
                    started = true;
                    timer = 0;
                    GameManager.GetManager().cameraController.StartInteractCam(6);
                    GameManager.GetManager().m_CurrentStateGame = GameManager.StateGame.MiniGame;
                    GameManager.GetManager().CanvasManager.UnLock();

                    StartCoroutine(ActivateWaterCan());
                }
                break;
            default:
                break;
        }
    }

    private void Update()
    {
        if (!tutorialShowed && started)
            InitTutorial();

        if(tutorialShowed && waterCan.dragg) m_Tutorial.SetActive(false);

        if (started && Input.GetKeyDown(KeyCode.Escape))
        {
            GameManager.GetManager().StartThirdPersonCamera();
            started = false;
            waterCan.gameObject.SetActive(false);
            waterCan.ResetWaterCan();
        }
    }

    private void FinishInteraction()
    {
        minigameCanvas.SetActive(false);
        waterCan.GrowUpParticle.Play();
        waterCan.gameObject.SetActive(false);
        GameManager.GetManager().StartThirdPersonCamera();
        GameManager.GetManager().Autocontrol.AddAutoControl(m_MinAutoControl);
        m_Done = true;
        started = false;
        waterCan.dragg = false;
        CheckDoneTask();
        GameManager.GetManager().dayNightCycle.TaskDone();
        waterCan.gameObject.SetActive(false);
    }

    public override void ResetInteractable()
    {
        base.ResetInteractable();
        regadera.ResetInteractable();
        waterCan.ResetWaterCan();
        timer = 0;
        started = false;
    }

    public void NextDay()
    {
        //grow
        if (m_Done)
        {
            waterCan.GrowUpParticle.Stop();
            if (currProcess < m_process.Length)
            {
                m_process[currProcess].SetActive(false);
                currProcess++;
                waterCan.GrowUpParticle.Play();
                m_process[currProcess].SetActive(true);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (!waterCan.dragg)
            return;

        if (timer <= maxTimer)
            timer += Time.deltaTime;
        else
        {
            FinishInteraction();
        }
    }
    private IEnumerator ActivateWaterCan()
    {
        yield return new WaitForSecondsRealtime(1.5f);
        waterCan.gameObject.SetActive(true);
    }

    private void InitTutorial()
    {
        StartCoroutine(ActivateMinigameCanvas());
        StartCoroutine(HideTutorial());
        if(m_Tutorial.GetComponent<Animator>() != null)
            m_Tutorial.GetComponent<Animator>().SetBool("show", true);
        tutorialShowed = true;
    }

    private IEnumerator HideTutorial()
    {
        yield return new WaitForSecondsRealtime(8);
        m_Tutorial.SetActive(false);
    }

    private IEnumerator ActivateMinigameCanvas()
    {
        yield return new WaitForSecondsRealtime(1.5f);
        minigameCanvas.SetActive(true);
    }
}

