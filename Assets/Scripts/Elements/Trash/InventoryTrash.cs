using UnityEngine;
using TMPro;
using System.Collections;

public class InventoryTrash : MonoBehaviour
{
    public TMP_Text dirtyClothesCounter;
    public TMP_Text trashCounter;
    private string dirtyClothesPhrase = "x ";
    private string trashPhrase = "x ";

    private int trashCollected;
    private int dirtyClothes;

    private void Start()
    {
        GameManager.GetManager().InventoryTrash = this;
        dirtyClothesCounter.gameObject.SetActive(false);
        trashCounter.gameObject.SetActive(false);
        dirtyClothesCounter.text = "";
        trashCounter.text = "";
    }

    public void AddDirtyClothes()
    {
        dirtyClothesCounter.gameObject.SetActive(true);
        dirtyClothes++;
        dirtyClothesCounter.text = dirtyClothes.ToString() + dirtyClothesPhrase;
    }

    public void AddTrash()
    {
        trashCounter.gameObject.SetActive(true);
        trashCollected++;
        trashCounter.text = trashCollected.ToString() + trashPhrase;
    }

    public void RemoveTrash()
    {
        StartCoroutine(RemoveTrashDelay(trashCollected, trashCounter, trashPhrase));
        trashCounter.gameObject.SetActive(false);
        trashCollected = 0;
    }

    public void RemoveDirtyClothes()
    {
        StartCoroutine(RemoveTrashDelay(dirtyClothes, dirtyClothesCounter, dirtyClothesPhrase));
        dirtyClothesCounter.gameObject.SetActive(false);
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
