using UnityEngine;
using TMPro;
using System.Collections;

public class InventoryTrash : MonoBehaviour
{
    public TMP_Text dirtyClothesCounter;
    public TMP_Text trashCounter;
    private string dirtyClothesPhrase = "";
    private string trashPhrase = "";

    private int trashCollected;
    private int dirtyClothes;
    Trash current;
    TrashBucket currentbucket;

    private void Start()
    {
        GameManager.GetManager().InventoryTrash = this;
        dirtyClothesCounter.gameObject.SetActive(false);
        trashCounter.gameObject.SetActive(false);
        dirtyClothesCounter.text = "";
        trashCounter.text = "";
    }

    public void AddDirtyClothes(Trash t)
    {
        dirtyClothesCounter.gameObject.SetActive(true);
        dirtyClothes++;
        current = t;
        dirtyClothesCounter.text = dirtyClothes.ToString() + dirtyClothesPhrase;
        GameManager.GetManager().m_CurrentStateGame = GameManager.StateGame.GamePlay;
    }

    public void AddTrash(Trash t)
    {
        trashCounter.gameObject.SetActive(true);
        trashCollected++;
        current = t;
        trashCounter.text = trashCollected.ToString() + trashPhrase;
        GameManager.GetManager().m_CurrentStateGame = GameManager.StateGame.GamePlay;
    }

    public void RemoveTrash()
    {
        StartCoroutine(RemoveTrashDelay(current,trashCollected, trashCounter, trashPhrase));
        trashCollected = 0;
    }

    public void ResetInventory()
    {
        trashCollected = 0;
        // dirtyClothes = 0;
    }

    public void RemoveDirtyClothes(TrashBucket bucket)
    {
        currentbucket = bucket;
        StartCoroutine(RemoveTrashDelay(current,dirtyClothes, dirtyClothesCounter, dirtyClothesPhrase));
        dirtyClothes = 0;
    }

    public int CurrentDirtyClothes() { return dirtyClothes; }
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
