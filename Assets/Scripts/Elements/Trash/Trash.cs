using UnityEngine;

public class Trash : Interactables
{
    
    public override void Interaction(int optionNumber)
    {
        switch (optionNumber)
        {
            case 1:
                gameObject.SetActive(false);
                break;
        }
    }
}
