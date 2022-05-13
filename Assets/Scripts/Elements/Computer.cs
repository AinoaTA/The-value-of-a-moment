using UnityEngine;

public class Computer : Interactables
{
    public override void Interaction(int options)
    {
        switch (options)
        {
            case 1:
                //m_Done = true;
                Debug.Log("I'm in");
                GameManager.GetManager().CanvasManager.ComputerScreenIn();
                break;
        }
    }
}
