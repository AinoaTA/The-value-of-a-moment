using System.Collections;
using UnityEngine;

public class Plant : Interactables
{
    public bool water;

    [SerializeField] private float distance;
    public WaterCan waterCan;
    Vector3 wateringInitialPos;

    [SerializeField]private float timer;
    [SerializeField] private float maxTimer = 3f;
    private int currProcess=0;
    public GameObject[] m_process;
    private bool started;

    private void Start()
    {
        GameManager.GetManager().Plants.Add(this);

        if(waterCan != null) waterCan.gameObject.SetActive(false);
            m_process[currProcess].SetActive(true);
    }

    public override void Interaction(int options)
    {
        switch (options)
        {
            case 1:
                if (!m_Done)
                {
                    started = true;
                    timer = 0;
                    GameManager.GetManager().PlayerController.SetInteractable("Plant");
                    GameManager.GetManager().m_CurrentStateGame = GameManager.StateGame.MiniGame;
                    GameManager.GetManager().CanvasManager.UnLock();

                    StartCoroutine(ActivateWaterCan());
                }
                //else
                //    FinishInteraction();

                break;
            default:
                break;
        } 
    }

    private void Update()
    {
        if (started && Input.GetKeyDown(KeyCode.Escape))
        {
            GameManager.GetManager().StartThirdPersonCamera();
           // GameManager.GetManager().Autocontrol.AddAutoControl(m_MinAutoControl);
            started = false;
            waterCan.gameObject.SetActive(false);
        }
    }

    private void FinishInteraction()
    {

        waterCan.GrowUpParticle.Play();
        GameManager.GetManager().StartThirdPersonCamera();
        GameManager.GetManager().Autocontrol.AddAutoControl(m_MinAutoControl);
        print("plant");
        m_Done = true;
        started = false;
        CheckDoneTask();
        GameManager.GetManager().dayNightCycle.TaskDone();
        waterCan.gameObject.SetActive(false);
    }

    public override void ResetInteractable()
    {
        base.ResetInteractable();
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
        //else //else no grow. future option.
        //{ 
        
        //}
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

    //public override void ExitInteraction()
    //{
    //    waterCan.gameObject.SetActive(false);
    //    timer = 0;
    //    base.ExitInteraction();
    //}

    private IEnumerator ActivateWaterCan()
    {
        yield return new WaitForSecondsRealtime(1.5f);
        waterCan.gameObject.SetActive(true);
    }
}

