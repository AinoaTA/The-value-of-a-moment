
using UnityEngine;

public class Computer : MonoBehaviour, Iinteract
{
    [HideInInspector]private string m_NameObject="Encender";
    private bool m_Done;
    public void Interaction()
    {
        //m_Done = true;
        Debug.Log("I'm in");
        //GameManager.GetManager().GetCanvasManager().ComputerScreenIn();
    }

    public string NameAction()
    {
        return m_NameObject;
    }


    public bool GetDone()
    {
        return m_Done;
    }

    public string[] GetPhrases()
    {
        throw new System.NotImplementedException();
    }
}
