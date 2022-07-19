using System.Collections;
using UnityEngine;

public class Window : Interactables
{
    public GameObject Glass;
    public GameObject Tutorial;
    private GameObject minigameCanvas = null;
    private Vector3 initPos;
    private float mOffset;
    private float zWorldCoord;
    private float minHeight;
    private float maxHeight = 3.5f;
    private bool isOpen = false;
    private bool gameInitialized = false;
    private bool tutorialShowed = false;

    public float distance;
    bool temp = false;

    private void Start()
    {
        minigameCanvas = Tutorial.transform.parent.gameObject;
        minigameCanvas.SetActive(false);
        //GameManager.GetManager().Window = this;
        minHeight = Glass.transform.position.y;
        initPos = Glass.transform.position;
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
                    gameInitialized = true;
                    // Inicia minijuego
                    GameManager.GetManager().cameraController.StartInteractCam(4);
                    GameManager.GetManager().CanvasManager.UnLock();
                    GameManager.GetManager().gameStateController.ChangeGameState(2);
                }
                break;
            case 2:
                break;
        }
    }

    public override void ExitInteraction()
    {
        gameInitialized = false;
        base.ExitInteraction();
    }

    public void ResetWindow()
    {
        Done = false;
        isOpen = Done;
        gameInitialized = false;
        Glass.transform.position = initPos;
    }

    private void InitTutorial()
    {
        StartCoroutine(ActivateMinigameCanvas());
        StartCoroutine(HideTutorial());
        Animator animator = Tutorial.GetComponent<Animator>();
        if (animator != null) animator.SetBool("show", true);
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
            zWorldCoord = Camera.main.WorldToScreenPoint(Glass.transform.position).z;
            // offset = World pos - Mouse World pos
            mOffset = Glass.transform.position.y - GetMouseYaxisAsWorldPoint();
        }
    }

    void OnMouseDrag()
    {
        if (gameInitialized && !isOpen)
        {
            if (tutorialShowed) Tutorial.SetActive(false);

            float height = Glass.transform.position.y;
            float displacement = GetMouseYaxisAsWorldPoint() + mOffset;

            if (displacement < minHeight)
                height = minHeight;

            else if (displacement < maxHeight)
                height = displacement;

            else if (displacement > maxHeight)
            {
                height = maxHeight;
                Done = isOpen = true;
            }
            Glass.transform.position = new Vector3(Glass.transform.position.x, height, Glass.transform.position.z);
        }
    }

    private void OnMouseUp()
    {
        if (Done && gameInitialized)
            WindowDone();
    }
    #endregion
    private void WindowDone()
    {
        ExitInteraction();
        CheckDoneTask();
        GameManager.GetManager().Autocontrol.AddAutoControl(MinAutoControl);
        GameManager.GetManager().StartThirdPersonCamera();
        minigameCanvas.SetActive(false);
        isOpen = true;
        Done = true;
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
    //    if (PhrasesVoiceOff.Length >= 2)
    //    {
    //        yield return new WaitForSeconds(2);
    //        GameManager.GetManager().Dialogue.SetDialogue(PhrasesVoiceOff[0]);
    //        yield return new WaitWhile(() => GameManager.GetManager().Dialogue.CheckDialogueIsPlaying());
    //        GameManager.GetManager().Dialogue.SetDialogue(AnswersToVoiceOff[0]);
    //        yield return new WaitForSeconds(3);
    //        GameManager.GetManager().Dialogue.SetDialogue(PhrasesVoiceOff[1]);
    //        yield return new WaitWhile(() => GameManager.GetManager().Dialogue.CheckDialogueIsPlaying());
    //        GameManager.GetManager().Dialogue.StopDialogue();
    //    }
    //}

    //private IEnumerator GoodInteraction()
    //{
    //    if (PhrasesVoiceOff.Length >= 2)
    //    {
    //        yield return new WaitForSeconds(2);
    //        GameManager.GetManager().Dialogue.SetDialogue(PhrasesVoiceOff[2]);
    //        yield return new WaitWhile(() => GameManager.GetManager().Dialogue.CheckDialogueIsPlaying());
    //        GameManager.GetManager().Dialogue.SetDialogue(AnswersToVoiceOff[1]);
    //        yield return new WaitForSeconds(1.25f);
    //        GameManager.GetManager().Dialogue.StopDialogue();

    //        StartCoroutine(NextAction());
    //    }
    //}

    //private IEnumerator BadInteraction()
    //{
    //    if (PhrasesVoiceOff.Length >= 4)
    //    {
    //        yield return new WaitForSeconds(2);
    //        GameManager.GetManager().Dialogue.SetDialogue(PhrasesVoiceOff[3]);
    //        yield return new WaitWhile(() => GameManager.GetManager().Dialogue.CheckDialogueIsPlaying());
    //        GameManager.GetManager().Dialogue.SetDialogue(AnswersToVoiceOff[2]);
    //        yield return new WaitForSeconds(2);
    //        GameManager.GetManager().Dialogue.StopDialogue();

    //        StartCoroutine(NextAction());
    //    }
    //}

    //private IEnumerator NextAction()
    //{
    //    if (PhrasesVoiceOff.Length >= 6)
    //    {
    //        yield return new WaitForSeconds(2);
    //        GameManager.GetManager().Dialogue.SetDialogue(PhrasesVoiceOff[4]);
    //        yield return new WaitWhile(() => GameManager.GetManager().Dialogue.CheckDialogueIsPlaying());
    //        //GameManager.GetManager().Dialogue.SetDialogue(AnswersToVoiceOff[3]);
    //        //yield return new WaitForSeconds(2);
    //        //GameManager.GetManager().Dialogue.SetDialogue(PhrasesVoiceOff[5]);
    //        //yield return new WaitWhile(() => GameManager.GetManager().Dialogue.CheckDialogueIsPlaying());
    //        GameManager.GetManager().Dialogue.StopDialogue();
    //    }
    //}
    #endregion
}
