using System.Collections;
using UnityEngine;

public class BedMinigame : MonoBehaviour
{

    private int ValueConfident = 5; //
    private Camera cam;
    private bool Completed;
    private Vector3 finalLimit;
    private Vector3 initPos;
    public GameObject Sheet;
    public GameObject Limit;
    public LayerMask LayerMask;
    [HideInInspector] public bool GameActive = false;
    public bool GameActive() => GameActive;
    private void Start()
    {
        cam = Camera.main;
        initPos = Sheet.transform.position;
        finalLimit = Limit.transform.position;
        finalLimit += new Vector3(0, 0, 0);
    }
    private void Update()
    {
        Ray l_Ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Input.GetMouseButton(0) && Input.GetAxisRaw("Mouse X") > 0 && !Completed)
        {
            if (Physics.Raycast(l_Ray, out RaycastHit l_Hit, LayerMask))
            {
                Sheet.transform.position += new Vector3(5, 0, 0);
                if (Sheet.transform.position.x >= finalLimit.x)
                    MinigameCompleted();
            }
        }
    }

    private void MinigameCompleted()
    {
        Completed = true;
        //cualquier cosa que el jgador entienda que la hizo bien
        StartCoroutine(DelayCompleted());
    }

    private IEnumerator DelayCompleted()
    {
        yield return new WaitForSeconds(1f);
        //GameManager.GetManager().Bed.BedDone();
        // GameManager.GetManager().CanvasManager.DesctiveBedCanvas();
        GameManager.GetManager().Autocontrol.AddAutoControl(ValueConfident);
        GameManager.GetManager().gameStateController.ChangeGameState(1);
        GameActive = false;
        ResetMinigame();

    }
    private void ResetMinigame()
    {
        Sheet.transform.position = initPos;
        Completed = false;
    }
}
