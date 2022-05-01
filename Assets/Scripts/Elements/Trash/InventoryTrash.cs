using UnityEngine;
using TMPro;
public class InventoryTrash : MonoBehaviour
{
    public TMP_Text counterTrash;
    private int trashCollected;

    private void Start()
    {
        counterTrash.text = trashCollected.ToString() + "x Ropa sucia";
    }

    public void AddTrash()
    {
        trashCollected++;
        counterTrash.text = trashCollected.ToString();
    }

    public void RemoveTrash()
    {
        trashCollected = 0;
        counterTrash.text = trashCollected.ToString() + "x Ropa sucia";
    }

    public int CurrentTrash() { return trashCollected; }
}
