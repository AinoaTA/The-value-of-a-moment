using UnityEngine;

public class BitterMinigame : MonoBehaviour
{
    [SerializeField]
    private GameObject beetParent;
    private GameObject[] beets;
    private GameObject activeBeet;
    private int counter;

    void Start()
    {
        counter = 0;
        int numBeets = beetParent.transform.childCount;
        beets = new GameObject[numBeets];
        for (int i = 0; i < numBeets; ++i)
        {
            GameObject beat = beetParent.transform.GetChild(i).gameObject;
            beat.SetActive(false);
            beets[i] = beat;
        }

        activeBeet = beets[counter];
        activeBeet.SetActive(true);
    }

    public void ManageLikeButton()
    {
        if (activeBeet.name.Contains("troll"))
        {
            // Disminuir confianza
        }
        else if (activeBeet.name.Contains("amig"))
        {
            // Aumentar confianza
        }
        ActivateNextBeet();
    }

    public void ManageDiscardButton()
    {
        if (activeBeet.name.Contains("troll"))
        {
            // Aumentar confianza
        }
        else if (activeBeet.name.Contains("amig"))
        {
            // Disminuir confianza
        }
        ActivateNextBeet();
    }

    private void ActivateNextBeet()
    {
        activeBeet.SetActive(false);
        counter++;
        activeBeet = beets[counter];
        activeBeet.SetActive(true);
    }
}
