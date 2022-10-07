using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drum : Interactables
{
    public DrumRhythm rhythm;
    public List<DrumInstrument> instruments;
    public float delayStart = 1f;
    public float delayNextInstrument = 0.5f;
    public float delayFinish = 1f;
    public string finalPlayCameraName;

    int rhythmPosition = 0;
    bool playingDrum = false;
    DrumInstrument pointedInstrument;

    public override void Interaction(int optionNumber)
    {
        base.Interaction(optionNumber);
        switch (optionNumber)
        {
            case 1:
                GameManager.GetManager().gameStateController.ChangeGameState(2);
                // Inicia minijuego
                GameManager.GetManager().cameraController.StartInteractCam(nameInteractable);
                GameManager.GetManager().canvasController.UnLock();

                playingDrum = false;
                StartCoroutine(StartActivity());
                break;
        }
    }

    public override void ExitInteraction()
    {
        StopPlayingDrum();
        GameManager.GetManager().StartThirdPersonCamera();
        base.ExitInteraction();
    }

    void Update()
    {
        if (playingDrum) DetectPlayingDrum();
    }

    IEnumerator StartActivity()
    {
        yield return new WaitForSeconds(delayStart);
        rhythmPosition = 0;
        ShowNextInstrument();
    }

    void ShowNextInstrument()
    {
        if (rhythmPosition >= rhythm.instrumentsOrder.Length) {
            StartPlayerPractice();
            return;
        }

        instruments[rhythm.instrumentsOrder[rhythmPosition]].SetRight();
        StartCoroutine(WaitNextInstrument());
    }
    
    IEnumerator WaitNextInstrument()
    {
        yield return new WaitForSeconds(delayNextInstrument);
        instruments[rhythm.instrumentsOrder[rhythmPosition]].Restore();
        rhythmPosition++;
        ShowNextInstrument();
    }

    void StartPlayerPractice()
    {
        rhythmPosition = 0;
        playingDrum = true;

        foreach (DrumInstrument instrument in instruments)
            instrument.Enable(true);
    }

    void DetectPlayingDrum() {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (!Physics.Raycast(ray, out hit))
        {
            RestorePointedInstrument();
            return;
        }

        LookForInstrumentPointed(hit.collider.gameObject);
        if (pointedInstrument && Input.GetMouseButtonDown(0)) PlayInstrument();
    }

    void LookForInstrumentPointed(GameObject objectPointed)
    {
        if (pointedInstrument && pointedInstrument.gameObject == objectPointed) return;

        RestorePointedInstrument();
        foreach (DrumInstrument instrument in instruments)
            if (instrument.gameObject == objectPointed)
            {
                instrument.MouseOver(true);
                pointedInstrument = instrument;
                return;
            }

        pointedInstrument = null;
    }

    void RestorePointedInstrument()
    {
        if (!pointedInstrument) return;
        pointedInstrument.MouseOver(false);
        pointedInstrument = null;
    }

    void PlayInstrument()
    {
        if (pointedInstrument != instruments[rhythm.instrumentsOrder[rhythmPosition]])
        {
            StopPlayingDrum();
            foreach (DrumInstrument instrument in instruments)
                instrument.Restore();
            pointedInstrument.SetWrong();
            StartCoroutine(FailPerformance());
            return;
        }

        rhythmPosition++;
        if (rhythmPosition >= rhythm.instrumentsOrder.Length)
        {
            StopPlayingDrum();
            StartCoroutine(PerformedSuccessfully());
        }

        RestoreAllInstruments();
        pointedInstrument.SetRight();
    }

    IEnumerator FailPerformance()
    {
        yield return new WaitForSeconds(delayStart);

        RestoreAllInstruments();
        rhythmPosition = 0;
        ShowNextInstrument();
    }

    IEnumerator PerformedSuccessfully()
    {
        yield return new WaitForSeconds(delayFinish);
        RestoreAllInstruments();
        GameManager.GetManager().cameraController.StartInteractCam(finalPlayCameraName);
    }

    void StopPlayingDrum() {
        if (playingDrum) {
            playingDrum = false;
            foreach (DrumInstrument instrument in instruments)
                instrument.Enable(false);
        }
    }

    void RestoreAllInstruments()
    {
        foreach (DrumInstrument instrument in instruments)
            instrument.Restore();
    }
}
