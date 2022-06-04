using System.Collections;
using UnityEngine;

public class Window : Interactables
{
    public GameObject m_Glass;
    public GameObject m_Tutorial;
    private GameObject minigameCanvas = null;
    private Vector3 initPos;
    private float mOffset;
    private float zWorldCoord;
    private float minHeight;
    private float maxHeight = 1.912f;
    private bool isOpen = false;
    private bool gameInitialized = false;
    private bool tutorialShowed = false;

    public float distance;
    bool temp = false;

    private void Start()
    {
        minigameCanvas = m_Tutorial.transform.parent.gameObject;
        GameManager.GetManager().Window = this;
        minHeight = m_Glass.transform.position.y;
        initPos = m_Glass.transform.position;
    }

    private void Update()
    {
        if(gameInitialized)
        {
            if(!tutorialShowed)
                InitTutorial();

            if (gameInitialized && Input.GetKeyDown(KeyCode.Escape))
			{
				minigameCanvas.SetActive(false);
				GameManager.GetManager().EndMinigame();
			}
        }

    }

    #region OnMouse Region
    void OnMouseDown()
    {
        zWorldCoord = Camera.main.WorldToScreenPoint(m_Glass.transform.position).z;
        // offset = World pos - Mouse World pos
        mOffset = m_Glass.transform.position.y - GetMouseYaxisAsWorldPoint();
    }

    void OnMouseDrag()
    {
        if (gameInitialized && !isOpen)
        {
            if(tutorialShowed) m_Tutorial.SetActive(false);

            float height = m_Glass.transform.position.y;
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
            m_Glass.transform.position = new Vector3(m_Glass.transform.position.x, height, m_Glass.transform.position.z);
        }
    }

    private void OnMouseUp()
    {
        if (m_Done)
            WindowDone();
    }
    #endregion
    private void WindowDone()
    {
        GameManager.GetManager().Autocontrol.AddAutoControl(m_MinAutoControl);
        GameManager.GetManager().EndMinigame();
        GameManager.GetManager().OpenDoor();
        minigameCanvas.SetActive(false);
        if (!temp)
        {
            temp = true;
            StartCoroutine(GoodInteraction());
        }
    }

    private float GetMouseYaxisAsWorldPoint()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = zWorldCoord; // set z coord

        return Camera.main.ScreenToWorldPoint(mousePoint).y;
    }

    #region Inherit Interactable methods

    public override void Interaction(int optionsSelected)
    {
        switch (optionsSelected)
        {
            case 1:
                if (!isOpen)
                    gameInitialized = true;
                    m_Done = false;
                    // Inicia minijuego
                    GameManager.GetManager().m_CurrentStateGame = GameManager.StateGame.MiniGame;
                    GameManager.GetManager().CanvasManager.UnLock();
                    GameManager.GetManager().PlayerController.SetInteractable("Window");
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
        m_Done = false;
        isOpen = m_Done;
        gameInitialized = false;
        m_Glass.transform.position = initPos;
    }

    private void InitTutorial()
    {
        StartCoroutine(ActivateMinigameCanvas());
        StartCoroutine(HideTutorial());
        Animator animator = m_Tutorial.GetComponent<Animator>();
        if(animator != null) animator.SetBool("show", true);
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

    #endregion

    #region Dialogues Region

    public void StartVoiceOffDialogueWindow()
    {
        StartCoroutine(StartWindows());
    }

    private IEnumerator StartWindows()
    {
        if (m_PhrasesVoiceOff.Length >= 2)
        {
            yield return new WaitForSeconds(2);
            GameManager.GetManager().Dialogue.SetDialogue(m_PhrasesVoiceOff[0]);
            yield return new WaitWhile(() => GameManager.GetManager().Dialogue.CheckDialogueIsPlaying());
            GameManager.GetManager().Dialogue.SetDialogue(m_AnswersToVoiceOff[0]);
            yield return new WaitForSeconds(3);
            GameManager.GetManager().Dialogue.SetDialogue(m_PhrasesVoiceOff[1]);
            yield return new WaitWhile(() => GameManager.GetManager().Dialogue.CheckDialogueIsPlaying());
            GameManager.GetManager().Dialogue.StopDialogue();
        }
    }

    private IEnumerator GoodInteraction()
	{
		if (m_PhrasesVoiceOff.Length >= 2)
		{
			yield return new WaitForSeconds(2);
			GameManager.GetManager().Dialogue.SetDialogue(m_PhrasesVoiceOff[2]);
			yield return new WaitWhile(() => GameManager.GetManager().Dialogue.CheckDialogueIsPlaying());
			GameManager.GetManager().Dialogue.SetDialogue(m_AnswersToVoiceOff[1]);
			yield return new WaitForSeconds(1.25f);
			GameManager.GetManager().Dialogue.StopDialogue();

			StartCoroutine(NextAction());
		}
    }

    private IEnumerator BadInteraction()
    {
		if (m_PhrasesVoiceOff.Length >= 4)
		{
			yield return new WaitForSeconds(2);
			GameManager.GetManager().Dialogue.SetDialogue(m_PhrasesVoiceOff[3]);
			yield return new WaitWhile(() => GameManager.GetManager().Dialogue.CheckDialogueIsPlaying());
			GameManager.GetManager().Dialogue.SetDialogue(m_AnswersToVoiceOff[2]);
			yield return new WaitForSeconds(2);
			GameManager.GetManager().Dialogue.StopDialogue();

			StartCoroutine(NextAction());
		}
    }

    private IEnumerator NextAction()
    {
		if (m_PhrasesVoiceOff.Length >= 6)
		{
			yield return new WaitForSeconds(2);
			GameManager.GetManager().Dialogue.SetDialogue(m_PhrasesVoiceOff[4]);
			yield return new WaitWhile(() => GameManager.GetManager().Dialogue.CheckDialogueIsPlaying());
			//GameManager.GetManager().Dialogue.SetDialogue(m_AnswersToVoiceOff[3]);
			//yield return new WaitForSeconds(2);
			//GameManager.GetManager().Dialogue.SetDialogue(m_PhrasesVoiceOff[5]);
			//yield return new WaitWhile(() => GameManager.GetManager().Dialogue.CheckDialogueIsPlaying());
			GameManager.GetManager().Dialogue.StopDialogue();
		}
    }
    #endregion
}
