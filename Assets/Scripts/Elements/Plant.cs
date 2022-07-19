using System.Collections;
using UnityEngine;

public class Plant : Interactables
{
    public bool water;
    public GameObject Tutorial;
    private GameObject minigameCanvas = null;

    [SerializeField] private float distance;
    public WaterCan waterCan;
    Vector3 wateringInitialPos;

    [SerializeField] private float timer;
    [SerializeField] private float maxTimer = 3f;
    private int currProcess = 0;
    public GameObject[] process;
    private bool started;
    public Regadera regadera;
    private bool tutorialShowed = false;

    private void Start()
    {
        minigameCanvas = Tutorial.transform.parent.gameObject;
        minigameCanvas.SetActive(false);
        //GameManager.GetManager().Plants.Add(this);

        if(waterCan != null) waterCan.gameObject.SetActive(false);
            process[currProcess].SetActive(true);
    }

    public override void Interaction(int options)
    {
        base.Interaction(options);
        switch (options)
        {
            case 1:
                if (!Done && regadera.grabbed)
                {
                    started = true;
                    timer = 0;
                    GameManager.GetManager().cameraController.StartInteractCam(6);
                    GameManager.GetManager().gameStateController.ChangeGameState(2);
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

        if(tutorialShowed && waterCan.dragg) Tutorial.SetActive(false);

        if (started && Input.GetKeyDown(KeyCode.Escape))
        {
            GameManager.GetManager().StartThirdPersonCamera();
            started = false;
            waterCan.gameObject.SetActive(false);
            waterCan.ResetWaterCan();
            actionEnter = false;
        }
    }

    private void FinishInteraction()
    {
        minigameCanvas.SetActive(false);
        waterCan.GrowUpParticle.Play();
        waterCan.gameObject.SetActive(false);
        GameManager.GetManager().StartThirdPersonCamera();
        GameManager.GetManager().Autocontrol.AddAutoControl(MinAutoControl);
        Done = true;
        started = false;
        waterCan.dragg = false;
        CheckDoneTask();
        GameManager.GetManager().dayNightCycle.TaskDone();
        waterCan.gameObject.SetActive(false);
        actionEnter = false;
    }

    public override void ResetInteractable()
    {
        base.ResetInteractable();
        regadera.ResetObject();
        waterCan.ResetWaterCan();
        timer = 0;
        started = false;

    }

    public void NextDay()
    {
        //grow
        if (Done)
        {
            waterCan.GrowUpParticle.Stop();
            if (currProcess < process.Length)
            {
                process[currProcess].SetActive(false);
                currProcess++;
                waterCan.GrowUpParticle.Play();
                process[currProcess].SetActive(true);
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
        if(Tutorial.GetComponent<Animator>() != null)
            Tutorial.GetComponent<Animator>().SetBool("show", true);
        tutorialShowed = true;
    }

    private IEnumerator HideTutorial()
    {
        yield return new WaitForSecondsRealtime(8);
        Tutorial.SetActive(false);
    }

    private IEnumerator ActivateMinigameCanvas()
    {
        yield return new WaitForSecondsRealtime(1.5f);
        minigameCanvas.SetActive(true);
    }
}

