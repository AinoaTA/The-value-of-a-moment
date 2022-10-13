using UnityEngine;
using UnityEngine.UI;
public class Mobile : GeneralActions
{
    [SerializeField] private bool getMobile;
    [SerializeField] private GameObject realMobile;
    [SerializeField] private CanvasGroup mobileCanvas;
    [SerializeField] private GameObject[] canvasFunctions;
    private BoxCollider col;
    public GameObject cursor;
    bool active = false;
    private void Start()
    {
        col = GetComponent<BoxCollider>();
        GameManager.GetManager().playerInputs._Mobile += OpenMobile;

        //GameManager.GetManager().playerInputs._Clics += Click;
    }

    private void OnDisable()
    {
        GameManager.GetManager().playerInputs._Mobile -= OpenMobile;
        //GameManager.GetManager().playerInputs._Clics -= Click;
    }
    public override void EnterAction()
    {
        if (!getMobile) GetMobile();
    }

    private void GetMobile()
    {
        realMobile.SetActive(false);
        getMobile = true;
        col.enabled = false;

    }
    private void OpenMobile()
    {
        if (!getMobile) return;

        if (!active)
        {
            cursor.SetActive(true);
            active = true;
            GameManager.GetManager().gameStateController.ChangeGameState(2);
            GameManager.GetManager().canvasController.UnLock(false);
            GameManager.GetManager().cameraController.Block3DMovement(false);
            CanvasMobile(true);
            FMODUnity.RuntimeManager.PlayOneShot("event:/Env/UI/Phone Unlock");
            if (GameManager.GetManager().dayController.GetDayNumber() == DayController.Day.two)
            {
                GameManager.GetManager().dialogueManager.SetDialogue("D2AccTelef_Chat");
                GameManager.GetManager().IncrementInteractableCount();
            }
        }
        else
        {
            cursor.SetActive(false);
            active = false;
            GameManager.GetManager().StartThirdPersonCamera();
            GameManager.GetManager().gameStateController.ChangeGameState(1);
            GameManager.GetManager().canvasController.Lock();
            GameManager.GetManager().cameraController.Block3DMovement(true);
            CanvasMobile(false);
            CanvasMultiple(false);
            FMODUnity.RuntimeManager.PlayOneShot("event:/Env/UI/Phone Lock");
        }
    }

    public void CanvasMultiple(bool val)
    {
        for (int i = 0; i < canvasFunctions.Length; i++)
            canvasFunctions[i].SetActive(val);
    }

    public void CanvasMobile(bool val)
    {
        mobileCanvas.alpha = val ? 1 : 0;
        mobileCanvas.blocksRaycasts = val;
        mobileCanvas.interactable = val;
    }

    private void Update()
    {
        if (active && !GameManager.GetManager().programmed)
        {
            cursor.transform.position = Input.mousePosition;
        }
    }

    //int clics;
    //public void Click()
    //{
    //    if (active)
    //    {
    //        clics++;
    //        print(clics);
    //        if (clics >= 5)
    //            GameManager.GetManager().dialogueManager.SetDialogue("PonerseATrabajar");
    //    }
    //}
}
