using System.Collections;
using UnityEngine;

public class Window : Interactables
{
    public GameObject m_Glass;

    private float mOffset;
    private float zWorldCoord;
    private float minHeight;
    private float maxHeight = 7.35f;
    private bool isOpen = false;
    private bool gameInitialized = false;

    public float distance;
 
    private void Awake()
    {
        print(m_Glass);
        print(m_Glass.transform.position);
        print(m_Glass.transform.position.y);
       

        GameManager.GetManager().Window = this;
        minHeight = m_Glass.transform.position.y;
    }

    void OnMouseDown()
    {
        zWorldCoord = Camera.main.WorldToScreenPoint(m_Glass.transform.position).z;

        // offset = World pos - Mouse World pos
        mOffset = m_Glass.transform.position.y - GetMouseYaxisAsWorldPoint();
    }

    void OnMouseDrag()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            print("Temporal code");
            StartCoroutine(GoodInteraction());
            
            GameManager.GetManager().PlayerController.ExitInteractable();
            GameManager.GetManager().m_CurrentStateGame = GameManager.StateGame.GamePlay;
            GameManager.GetManager().CanvasManager.Lock();
            GameManager.GetManager().OpenDoor();
        }

        if (gameInitialized && !isOpen)
        {
            float height = m_Glass.transform.position.y;
            float displacement = GetMouseYaxisAsWorldPoint() + mOffset;

            if (displacement < minHeight)
                height = minHeight;

            else if (displacement < maxHeight)
                height = displacement;

            else if (displacement > maxHeight)
            {
                height = maxHeight;
                isOpen = true;
                
                GameManager.GetManager().PlayerController.ExitInteractable();
                GameManager.GetManager().m_CurrentStateGame = GameManager.StateGame.GamePlay;
                GameManager.GetManager().CanvasManager.Lock();
                GameManager.GetManager().OpenDoor();
                StartCoroutine(GoodInteraction());
            }
            m_Glass.transform.position = new Vector3(m_Glass.transform.position.x, height, m_Glass.transform.position.z);
        }
    }

    private float GetMouseYaxisAsWorldPoint()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = zWorldCoord; // set z coord

        return Camera.main.ScreenToWorldPoint(mousePoint).y;
    }

    #region Inherit Interactable methods

    public override void Interaction()
    {
        if (!isOpen)
            gameInitialized = true;

        // Inicia minijuego
        GameManager.GetManager().m_CurrentStateGame = GameManager.StateGame.MiniGame;
        GameManager.GetManager().CanvasManager.UnLock();
        GameManager.GetManager().PlayerController.SetInteractable("Window");
    }

    public void ResetWindow()
    {
        isOpen = false;
        gameInitialized = false;
        m_Glass.transform.position = new Vector3(m_Glass.transform.position.x, minHeight, m_Glass.transform.position.z);
    }

    #endregion
    public void StartVoiceOffDialogueWindow()
    {
        StartCoroutine(StartWindows());
    }

    private IEnumerator StartWindows()
    {
        if (m_PhrasesVoiceOff.Length == 2)
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
        yield return new WaitForSeconds(2);
        GameManager.GetManager().Dialogue.SetDialogue(m_PhrasesVoiceOff[2]);
        yield return new WaitWhile(() => GameManager.GetManager().Dialogue.CheckDialogueIsPlaying());
        GameManager.GetManager().Dialogue.SetDialogue(m_AnswersToVoiceOff[1]);
        yield return new WaitForSeconds(1.25f);
        GameManager.GetManager().Dialogue.StopDialogue();

        StartCoroutine(NextAction());
    }

    private IEnumerator BadInteraction()
    {
        yield return new WaitForSeconds(2);
        GameManager.GetManager().Dialogue.SetDialogue(m_PhrasesVoiceOff[3]);
        yield return new WaitWhile(() => GameManager.GetManager().Dialogue.CheckDialogueIsPlaying());
        GameManager.GetManager().Dialogue.SetDialogue(m_AnswersToVoiceOff[2]);
        yield return new WaitForSeconds(2);
        GameManager.GetManager().Dialogue.StopDialogue();

        StartCoroutine(NextAction());
    }

    private IEnumerator NextAction()
    {
        yield return new WaitForSeconds(2);
        GameManager.GetManager().Dialogue.SetDialogue(m_PhrasesVoiceOff[4]);
        yield return new WaitWhile(() => GameManager.GetManager().Dialogue.CheckDialogueIsPlaying());
        GameManager.GetManager().Dialogue.SetDialogue(m_AnswersToVoiceOff[3]);
        yield return new WaitForSeconds(2);
        GameManager.GetManager().Dialogue.SetDialogue(m_PhrasesVoiceOff[5]);
        yield return new WaitWhile(() => GameManager.GetManager().Dialogue.CheckDialogueIsPlaying());
        GameManager.GetManager().Dialogue.StopDialogue();
    }

}
