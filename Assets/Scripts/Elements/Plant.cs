using UnityEngine;

public class Plant : Interactables
{
    public bool water;

    //[HideInInspector] public string m_NameObject;
    //public string[] m_HelpPhrases;
    [SerializeField] private float distance;
    public WaterCan waterCan;
    Vector3 wateringInitialPos;

    private float timer;
    [SerializeField] private float maxTimer = 3f;
    private void Start()
    {
        m_NameObject = "Regar";
        waterCan.GetComponent<WaterCan>();
        waterCan.gameObject.SetActive(false);
    }



    public override void Interaction()
    {
        if (!m_Done)
        {
            GameManager.GetManager().PlayerController.SetInteractable("Plant");
            timer = 0;
            GameManager.GetManager().m_CurrentStateGame = GameManager.StateGame.MiniGame;

            waterCan.gameObject.SetActive(true);
        }
        else
            FinishInteraction();

    }


    private void FinishInteraction()
    {
        m_Done = true;
        GameManager.GetManager().m_CurrentStateGame = GameManager.StateGame.GamePlay;
        GameManager.GetManager().PlayerController.ExitInteractable();
        m_NameObject = "";
        waterCan.gameObject.SetActive(false);
    }


    //public override bool GetDone()
    //{
    //    return m_Done;
    //}

    //public override string[] GetPhrases()
    //{
    //    return m_HelpPhrases;
    //}


    //public override string NameAction()
    //{
    //    return m_NameObject;
    //}

    private void OnTriggerStay(Collider other)
    {
        if (!waterCan.dragg)
            return;

        print(timer);
        if (timer <= maxTimer)
            timer += Time.deltaTime;
        else
        {
            FinishInteraction();
        }
    }
}
