using System.Collections;
using UnityEngine;


public class Bed : Interactables
{
    public Camera cam;
    public GameObject m_Tutorial;
    private GameObject minigameCanvas = null;
    public GameObject m_SheetBad;
    public GameObject m_Sheet;  //sabana
    public GameObject interactTextBed;
    public GameObject sleepTextBed;
    private bool gameInitialized;
    Vector3 initPosBadSheet;
    float minDesplacement;
    float maxDesplacement = 2.17f;
    private float zWorldCoord;
    private float mOffset;
    private bool tutorialShowed = false;
    private Vector3 initPosDormirText;
    private Vector3 lastPosDormirText;
    private GameObject badBed;

    private void Start()
    {
        if(m_Tutorial != null)
        {
            minigameCanvas = m_Tutorial.transform.parent.gameObject;
            minigameCanvas.SetActive(false);
        }
        if(m_SheetBad != null)
        {
            badBed = m_SheetBad.transform.parent.gameObject;
            m_SheetBad.SetActive(true);
            initPosBadSheet = m_SheetBad.transform.position;
            minDesplacement = m_SheetBad.transform.position.x;
        }
        options = 2;
        GameManager.GetManager().Bed = this;
        gameInitialized = false;

        initPosDormirText = sleepTextBed.transform.localPosition;
        lastPosDormirText = new Vector3(-0.03f, lastPosDormirText.y, lastPosDormirText.z);
    }

    void OnMouseDrag()
    {
        if (gameInitialized && !m_Done)
        {
            if(!tutorialShowed)
                InitTutorial();
            else
                m_Tutorial.SetActive(false);

            float movement = m_SheetBad.transform.position.x;
            float displacement = GetMouseXaxisAsWorldPoint() + mOffset;

            if (displacement < minDesplacement)
            {
                print("not enough");
                movement = minDesplacement;
            }
            else if (displacement < maxDesplacement)
                movement = displacement;

            else if (displacement > maxDesplacement)
            {
                movement = maxDesplacement;
                m_Done = true;
            }
            m_SheetBad.transform.position = new Vector3(movement, m_SheetBad.transform.position.y, m_SheetBad.transform.position.z);
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && gameInitialized)
        {
            Debug.Log("Exit");
            Exit();
        }
    }
    public void Exit()
    {
        gameInitialized = false;
        cam.cullingMask = -1;
        minigameCanvas.SetActive(false);
        GameManager.GetManager().StartThirdPersonCamera();
    }
    
    private void InitTutorial()
    {
        StartCoroutine(ActivateMinigameCanvas());
        Animator animator = m_Tutorial.GetComponent<Animator>();
        if(animator != null) animator.SetBool("show", true);
        StartCoroutine(HideTutorial());
        tutorialShowed = true;
    }
    
    private IEnumerator HideTutorial()
    {
        yield return new WaitForSecondsRealtime(8);
        m_Tutorial.SetActive(false);
    }

    private IEnumerator ActivateMinigameCanvas()
    {
        yield return new WaitForSecondsRealtime(1f);
        minigameCanvas.SetActive(true);
    }

    private void OnMouseUp()
    {
        if (m_Done && gameInitialized)
            BedDone();
    }

    void OnMouseDown()
    {
        zWorldCoord = Camera.main.WorldToScreenPoint(m_SheetBad.transform.position).z;
        // offset = World pos - Mouse World pos
        mOffset = m_SheetBad.transform.position.y - GetMouseXaxisAsWorldPoint();
    }

    public void BedDone()
    {
        gameInitialized = false;
        minigameCanvas.SetActive(false);
        m_Done = true;
        cam.cullingMask = -1;
        //Cambiamos la sabana u objeto cama.
        m_Sheet.SetActive(true);
        badBed.SetActive(false);
        interactTextBed.SetActive(false);
        sleepTextBed.transform.localPosition = lastPosDormirText;
        GameManager.GetManager().dayNightCycle.TaskDone();
        GameManager.GetManager().StartThirdPersonCamera();
        GameManager.GetManager().Autocontrol.AddAutoControl(m_MinAutoControl);
    }

    public void ResetBed()
    {
        GameManager.GetManager().Alarm.SetAlarmActive();
        GameManager.GetManager().Alarm.ResetTime();
        m_Done = false;
        m_Sheet.SetActive(false);
        badBed.SetActive(true);
        sleepTextBed.transform.localPosition = initPosDormirText;
        m_SheetBad.transform.position = initPosBadSheet;
        interactTextBed.SetActive(true);
        gameInitialized = false;
    }

    public override void Interaction(int options)
    {
        switch (options)
        {
            case 1:
                if (!m_Done)
                {
                    GameManager.GetManager().PlayerController.SetInteractable("Bed");
                    gameInitialized = true;
                    GameManager.GetManager().CanvasManager.UnLock();
                    GameManager.GetManager().m_CurrentStateGame = GameManager.StateGame.MiniGame;
                    cam.cullingMask &= ~(1 << LayerMask.NameToLayer("Player"));
                    StartCoroutine(ActivateMinigameCanvas());
                }
                break;
            case 2:
                GameManager.GetManager().CanvasManager.FadeIn();
                GameManager.GetManager().m_CurrentStateGame = GameManager.StateGame.Init;
                GameManager.GetManager().CanvasManager.Lock();
                GameManager.GetManager().Dialogue.StopDialogue();
                
                StartCoroutine(DelayReset());
                break;
            default:
                break;
        }
    }

    private float GetMouseXaxisAsWorldPoint()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = zWorldCoord; // set z coord

        return Camera.main.ScreenToWorldPoint(mousePoint).x;
    }

    private IEnumerator DelayReset()
    {
        GameManager.GetManager().SoundController.QuitAllMusic();
        GameManager.GetManager().CanvasManager.Pointer.SetActive(false);
        yield return new WaitForSeconds(0.5f);

        GameManager.GetManager().PlayerController.SetInteractable("Alarm");
        GameManager.GetManager().PlayerController.PlayerSleepPos();
        GameManager.GetManager().Dialogue.StopDialogue();
        GameManager.GetManager().Window.ResetWindow();
        GameManager.GetManager().calendarController.GlobalReset();
        GameManager.GetManager().ProgramMinigame.ResetAllGame();
        GameManager.GetManager().bucket.ResetInteractable();
        GameManager.GetManager().Mirror.ResetInteractable();
        //GameManager.GetManager().Book.ResetInteractable();
        //GameManager.GetManager().VR.ResetVRDay();

        for (int i = 0; i < GameManager.GetManager().trashes.Count; i++)
        {
            GameManager.GetManager().trashes[i].gameObject.SetActive(true);
            GameManager.GetManager().trashes[i].ResetInteractable();
        }

        for (int i = 0; i < GameManager.GetManager().Plants.Count; i++)
        {
            GameManager.GetManager().Plants[i].NextDay();
            GameManager.GetManager().Plants[i].ResetInteractable();
        }
        //no borrar hasta que estan tooooooodas las animaciones colocadas aqui.
        Debug.Log("NO FORGET: actions to reset.");
        ResetBed();
        yield return new WaitForSeconds(2);
        GameManager.GetManager().Autocontrol.AutocontrolSleep();
        GameManager.GetManager().dayNightCycle.NewDay();
        GameManager.GetManager().m_CurrentStateGame = GameManager.StateGame.Init;
    }

    public override void ExitInteraction()
    {
        cam.cullingMask = -1;
        base.ExitInteraction();

    }
}
