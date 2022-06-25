using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class Mobile : Interactables
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
    public override void Interaction(int options)
    {
        switch (options)
        {
            case 1:
                if (!getMobile)
                {
                    GetMobile();
                }
                break;
            default:
                break;
        }
    }

    private void GetMobile()
    {
        realMobile.SetActive(false);
        m_Done = getMobile = true;
        col.enabled = false;
        GameManager.GetManager().ChangeGameState(GameManager.StateGame.GamePlay);

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftAlt) && getMobile && !GameManager.GetManager().CanvasManager.m_activated)
        {
            if (mobileCanvas.alpha == 0 && GameManager.GetManager().m_CurrentStateGame == GameManager.StateGame.GamePlay)
            {
                GameManager.GetManager().ChangeGameState(GameManager.StateGame.MiniGame);
                GameManager.GetManager().CanvasManager.UnLock();
                CanvasMobile(true);
            }
            else
            {
                GameManager.GetManager().StartThirdPersonCamera();
                CanvasMobile(false);
                CanvasMultiple(false);
            }
        }
    }

    public void CanvasMultiple(bool val)
    {
        for (int i = 0; i < canvasFunctions.Length; i++)
        {
            canvasFunctions[i].SetActive(val);
        }
    }

    public void CanvasMobile(bool val)
    {
        mobileCanvas.alpha = val ? 1 : 0;
        mobileCanvas.blocksRaycasts = val ? true : false;
        mobileCanvas.interactable = val ? true : false;
        stateDriven.enabled = val ? false : true;
    }
}
