
using UnityEngine;

public class Computer : Interactables, IntfInteract
{
    [HideInInspector]private string m_NameObject="Encender";
    private bool m_Done;
    public float distance;

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, distance);
    }
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

    public float GetDistance()
    {
        return distance;
    }
}
