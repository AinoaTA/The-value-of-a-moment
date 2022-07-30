using System.Collections;
using UnityEngine;

public class Window : Interactables
{
    [SerializeField]private GameObject glass;
    [SerializeField] private GameObject tutorial;
    private GameObject minigameCanvas = null;
    private Vector3 initPos;
    private float mOffset;
    private float zWorldCoord;
    private float minHeight;
    private float maxHeight = 3.5f;
    private bool isOpen = false;
    private bool gameInitialized = false;
    private bool tutorialShowed = false;

    [SerializeField]private float distance;
    bool temp = false;

    private void Start()
    {
        minigameCanvas = tutorial.transform.parent.gameObject;
        minigameCanvas.SetActive(false);
        //GameManager.GetManager().Window = this;
        minHeight = glass.transform.position.y;
        initPos = glass.transform.position;
    }
    #region Inherit Interactable methods

    public override void Interaction(int options)
    {
        base.Interaction(options);
        switch (options)
        {
            case 1:
                if (!isOpen)
                {
                    GameManager.GetManager().gameStateController.ChangeGameState(2);
                    gameInitialized = true;
                    // Inicia minijuego
                    GameManager.GetManager().cameraController.StartInteractCam(4);
                    GameManager.GetManager().canvasController.UnLock();
                }
                break;
            case 2:
                break;
        }
    }

    public override void ExitInteraction()
    {
        gameInitialized = false;
        GameManager.GetManager().StartThirdPersonCamera();
        base.ExitInteraction();
    }

    public void ResetWindow()
    {
        m_Done = false;
        isOpen = m_Done;
        gameInitialized = false;
        glass.transform.position = initPos;
    }

    private void InitTutorial()
    {
        StartCoroutine(ActivateMinigameCanvas());
        StartCoroutine(HideTutorial());
        Animator animator = tutorial.GetComponent<Animator>();
        if (animator != null) animator.SetBool("show", true);
        tutorialShowed = true;
    }

    private IEnumerator HideTutorial()
    {
        yield return new WaitForSecondsRealtime(8);
        tutorial.SetActive(false);
    }

    private IEnumerator ActivateMinigameCanvas()
    {
        yield return new WaitForSecondsRealtime(1.5f);
        minigameCanvas.SetActive(true);
    }

    #endregion

    private void Update()
    {
        if (gameInitialized)
        {
            if (!tutorialShowed)
                InitTutorial();

            //if (gameInitialized && Input.GetKeyDown(KeyCode.Escape))
            //{
            //    minigameCanvas.SetActive(false);
            //    gameInitialized = false;
            //    GameManager.GetManager().StartThirdPersonCamera();
            //}
        }
    }

    #region OnMouse Region
    void OnMouseDown()
    {
        if (gameInitialized)
        {
            zWorldCoord = Camera.main.WorldToScreenPoint(glass.transform.position).z;
            // offset = World pos - Mouse World pos
            mOffset = glass.transform.position.y - GetMouseYaxisAsWorldPoint();
        }
    }

    void OnMouseDrag()
    {
        if (gameInitialized && !isOpen)
        {
            if (tutorialShowed) tutorial.SetActive(false);

            float height = glass.transform.position.y;
            float displacement = GetMouseYaxisAsWorldPoint() + mOffset;

            if (displacement < minHeight)
                height = minHeight;

            else if (displacement < maxHeight)
                height = displacement;

            else if (displacement > maxHeight)
            {
                height = maxHeight;
                m_Done = isOpen = true;
            }
            glass.transform.position = new Vector3(glass.transform.position.x, height, glass.transform.position.z);
        }
    }

    private void OnMouseUp()
    {
        if (m_Done && gameInitialized)
            WindowDone();
    }
    #endregion
    private void WindowDone()
    {
        ExitInteraction();
        CheckDoneTask();
        GameManager.GetManager().autocontrol.AddAutoControl(m_MinAutoControl);
        GameManager.GetManager().StartThirdPersonCamera();
        minigameCanvas.SetActive(false);
        isOpen = true;
        m_Done = true;
        GameManager.GetManager().dayNightCycle.TaskDone();
    }

    private float GetMouseYaxisAsWorldPoint()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = zWorldCoord; // set z coord

        return Camera.main.ScreenToWorldPoint(mousePoint).y;
    }


    #region Dialogues Region

    //public void StartVoiceOffDialogueWindow()
    //{
    //    StartCoroutine(StartWindows());
    //}

    //private IEnumerator StartWindows()
    //{
    //    if (m_PhrasesVoiceOff.Length >= 2)
    //    {
    //        yield return new WaitForSeconds(2);
    //        GameManager.GetManager().Dialogue.SetDialogue(m_PhrasesVoiceOff[0]);
    //        yield return new WaitWhile(() => GameManager.GetManager().Dialogue.CheckDialogueIsPlaying());
    //        GameManager.GetManager().Dialogue.SetDialogue(m_AnswersToVoiceOff[0]);
    //        yield return new WaitForSeconds(3);
    //        GameManager.GetManager().Dialogue.SetDialogue(m_PhrasesVoiceOff[1]);
    //        yield return new WaitWhile(() => GameManager.GetManager().Dialogue.CheckDialogueIsPlaying());
    //        GameManager.GetManager().Dialogue.StopDialogue();
    //    }
    //}

    //private IEnumerator GoodInteraction()
    //{
    //    if (m_PhrasesVoiceOff.Length >= 2)
    //    {
    //        yield return new WaitForSeconds(2);
    //        GameManager.GetManager().Dialogue.SetDialogue(m_PhrasesVoiceOff[2]);
    //        yield return new WaitWhile(() => GameManager.GetManager().Dialogue.CheckDialogueIsPlaying());
    //        GameManager.GetManager().Dialogue.SetDialogue(m_AnswersToVoiceOff[1]);
    //        yield return new WaitForSeconds(1.25f);
    //        GameManager.GetManager().Dialogue.StopDialogue();

    //        StartCoroutine(NextAction());
    //    }
    //}

    //private IEnumerator BadInteraction()
    //{
    //    if (m_PhrasesVoiceOff.Length >= 4)
    //    {
    //        yield return new WaitForSeconds(2);
    //        GameManager.GetManager().Dialogue.SetDialogue(m_PhrasesVoiceOff[3]);
    //        yield return new WaitWhile(() => GameManager.GetManager().Dialogue.CheckDialogueIsPlaying());
    //        GameManager.GetManager().Dialogue.SetDialogue(m_AnswersToVoiceOff[2]);
    //        yield return new WaitForSeconds(2);
    //        GameManager.GetManager().Dialogue.StopDialogue();

    //        StartCoroutine(NextAction());
    //    }
    //}

    //private IEnumerator NextAction()
    //{
    //    if (m_PhrasesVoiceOff.Length >= 6)
    //    {
    //        yield return new WaitForSeconds(2);
    //        GameManager.GetManager().Dialogue.SetDialogue(m_PhrasesVoiceOff[4]);
    //        yield return new WaitWhile(() => GameManager.GetManager().Dialogue.CheckDialogueIsPlaying());
    //        //GameManager.GetManager().Dialogue.SetDialogue(m_AnswersToVoiceOff[3]);
    //        //yield return new WaitForSeconds(2);
    //        //GameManager.GetManager().Dialogue.SetDialogue(m_PhrasesVoiceOff[5]);
    //        //yield return new WaitWhile(() => GameManager.GetManager().Dialogue.CheckDialogueIsPlaying());
    //        GameManager.GetManager().Dialogue.StopDialogue();
    //    }
    //}
    #endregion
}
