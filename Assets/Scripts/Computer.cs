
using UnityEngine;

public class Computer : MonoBehaviour, Iinteract
{
    [HideInInspector]private string m_NameObject="Encender";
    public void Interaction()
    {
        GameManager.GetManager().GetCanvasManager().ComputerScreenIn();
    }

    public string NameAction()
    {
        return m_NameObject;
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
