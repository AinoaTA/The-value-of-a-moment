using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class Mobile : ActionObject
{
    public bool getMobile;
    public GameObject realMobile;
    public CanvasGroup mobileCanvas;
    public GameObject[] canvasFunctions;
    private BoxCollider col;
    public CinemachineStateDrivenCamera stateDriven;

    private void Start()
    {
        col = GetComponent<BoxCollider>();
    }
    public override void Interaction()
    {

        if (!getMobile)
        {
            GetMobile();

        }
    }

    private void GetMobile()
    {
        realMobile.SetActive(false);
        getMobile = true;
        col.enabled = false;

    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.LeftAlt) && getMobile && !GameManager.GetManager().CanvasManager.m_activated)
        //{
        //    if (mobileCanvas.alpha == 0 && GameManager.GetManager().gameStateController.m_CurrentStateGame == GameStateController.StateGame.GamePlay)
        //    {
        //        GameManager.GetManager().gameStateController.ChangeGameState(2);
        //        GameManager.GetManager().CanvasManager.UnLock();
        //        CanvasMobile(true);
        //    }
        //    else
        //    {
        //        //  GameManager.GetManager().StartThirdPersonCamera();
        //        GameManager.GetManager().gameStateController.ChangeGameState(1);
        //        GameManager.GetManager().CanvasManager.Lock();
        //        CanvasMobile(false);
        //        CanvasMultiple(false);
        //    }
        //}
    }

    public void CanvasMultiple(bool val)
    {
        for (int i = 0; i < canvasFunctions.Length; i++)
            canvasFunctions[i].SetActive(val);
    }

    public void CanvasMobile(bool val)
    {
        mobileCanvas.alpha = val ? 1 : 0;
        mobileCanvas.blocksRaycasts = val ? true : false;
        mobileCanvas.interactable = val ? true : false;
        stateDriven.enabled = val ? false : true;
    }
}
