using System.Collections;
using TMPro;
using UnityEngine;

public class InventoryTrashUI : MonoBehaviour
{
    public TMP_Text dirtyClothesCounter;
    public TMP_Text trashCounter;
    private string dirtyClothesPhrase = "";
    private string trashPhrase = "";

    [HideInInspector] public int trashCollected;
    [HideInInspector] public int dirtyClothesCollected;
    Trash current;
    BucketController currentbucket;

    private void Start()
    {
        GameManager.GetManager().trashInventory = this;
        dirtyClothesCounter.gameObject.SetActive(false);
        trashCounter.gameObject.SetActive(false);
        dirtyClothesCounter.text = "";
        trashCounter.text = "";
    }

    public void AddDirtyClothes(Trash t)
    {
        dirtyClothesCounter.gameObject.SetActive(true);
        dirtyClothesCollected++;
        current = t;
        dirtyClothesCounter.text = dirtyClothesCollected.ToString() + dirtyClothesPhrase;
       // GameManager.GetManager().gameStateController.ChangeGameState(1);
    }

    public void AddTrash(Trash t)
    {
        trashCounter.gameObject.SetActive(true);
        trashCollected++;
        current = t;
        trashCounter.text = trashCollected.ToString() + trashPhrase;
        GameManager.GetManager().gameStateController.ChangeGameState(1);
    }

    public void RemoveTrash()
    {
        StartCoroutine(RemoveTrashDelay(current, trashCollected, trashCounter, trashPhrase));
        trashCollected = 0;
    }

    public void ResetInventory()
    {
        trashCollected = 0;
        dirtyClothesCollected = 0;
        trashCounter.text = "";
    }
    public void RemoveDirtyClothes(BucketController bucket)
    {
        currentbucket = bucket;
        StartCoroutine(RemoveTrashDelay(current, dirtyClothesCollected, dirtyClothesCounter, dirtyClothesPhrase));
        dirtyClothesCollected = 0;
    }

    public int CurrentDirtyClothes() { return dirtyClothesCollected; }
    public int CurrentTrash() { return trashCollected; }

    IEnumerator RemoveTrashDelay(Trash type, int trash, TMP_Text text, string finalText)
    {
        int curr = trash;
        for (int i = 0; i < trash; i++)
        {
            curr -= 1;
            text.text = curr + finalText;
            //  
            currentbucket.SomethingCleaned();
            yield return new WaitForSeconds(0.1f);
        }

        yield return new WaitUntil(() => curr <= 0);
        text.text = "";
        currentbucket = null;
        yield return null;

        dirtyClothesCounter.gameObject.SetActive(false);
        trashCounter.gameObject.SetActive(false);
    }
}
