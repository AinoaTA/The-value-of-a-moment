using UnityEngine;
using TMPro;
using System.Collections;

public class InventoryTrash : MonoBehaviour
{
    public TMP_Text dirtyClothesCounter;
    public TMP_Text trashCounter;
    private string dirtyClothesPhrase = " x Ropa sucia";
    private string trashPhrase = " x basura";

    private int trashCollected;
    private int dirtyClothes;

    private void Start()
    {
        GameManager.GetManager().InventoryTrash = this;
        dirtyClothesCounter.text = "";
        trashCounter.text = "";
    }

    public void AddDirtyClothes()
    {
        dirtyClothes++;
        dirtyClothesCounter.text = dirtyClothes.ToString() + dirtyClothesPhrase;
    }

    public void AddTrash() 
    {
        trashCollected++;
        trashCounter.text = trashCollected.ToString() + trashPhrase;
    }

    public void RemoveTrash()
    {
        StartCoroutine(RemoveTrashDelay(trashCollected, trashCounter, trashPhrase));
        trashCollected = 0;
    }

    public void RemoveDirtyClothes()
    {
        StartCoroutine(RemoveTrashDelay(dirtyClothes, dirtyClothesCounter, dirtyClothesPhrase));
        dirtyClothes = 0;
    }

    public int CurrentDirtyClothes() { return dirtyClothes; }
    public int CurrentTrash() { return trashCollected; }

    IEnumerator RemoveTrashDelay(int trash, TMP_Text text, string finalText)
    {
        int curr = trash;
        for (int i = 0; i < trash; i++)
        {
            curr -= 1;
            text.text = curr + finalText;
            yield return new WaitForSeconds(0.1f);
        }

        yield return new WaitUntil(() => curr <= 0);
        text.text = "";
    }
}
