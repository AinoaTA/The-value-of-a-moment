using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class WriteBitter : MonoBehaviour
{
    [SerializeField] BitterControl bitterControler;
    public TMP_Text[] texts;
    public TMP_Text genericText;

    public BitDay[] BitDays;
    [System.Serializable]
    public struct BitDay
    {
        public string name;
        public int numDay;
        public Bitters[] bits;
    }
    private List<Bitters> currList;
    [SerializeField] private GameObject acceptButton;

    int index;
    bool select;
    DayController.DayTime previus;
    private void Start()
    {        //temp
        previus = GameManager.GetManager().dayController.GetTimeDay();

        Debug.LogWarning("change for number day (another script will be manage this)");
        genericText.text = "";
    }

    bool wrote;
    public void WriteBit()
    {
        if (previus != GameManager.GetManager().dayController.GetTimeDay()) NewStateDay();
        if (wrote) return;
        wrote = true;
        UpdateList();

        index = CheckCondition(currList);
        for (int i = 0; i < 3; i++)
        {
            if (i >= currList[index].possibleBitters.Length) return;
            texts[i].transform.parent.gameObject.SetActive(true);
            texts[i].text = currList[index].possibleBitters[i].resumeName;
        }
        //currList.RemoveAt(index);
    }

    /// <summary>
    /// return Priority bitter if exist.
    /// </summary>
    /// <param name="list"></param>
    /// <returns></returns>
    private int CheckCondition(List<Bitters> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i].conditionComplete)
                return i;
        }
        return (int)GameManager.GetManager().dayController.GetTimeDay();
    }

    public void SelectPossibleAnswer(int i)
    {
        select = true;
        genericText.text = currList[index].possibleBitters[i].bitter;
        acceptButton.SetActive(true);
    }

    public void SendBritter()
    {
        if (!select) return;
        bitterControler.SendBitter(genericText.text);

        ClearBitter();
    }

    void ClearBitter()
    {
        for (int i = 0; i < 3; i++)
        {
            texts[i].transform.parent.gameObject.SetActive(false);
            texts[i].text = "";
        }

        genericText.text = "No tengo nada que escribir por el momento. Tal vez más tarde.";
        acceptButton.SetActive(false);
    }

    public void NewDay()
    {
        NewStateDay();
        ClearBitter();
    }

    public void NewStateDay()
    {
        select = false;
        wrote = false;
        genericText.text = "";
    }

    void UpdateList()
    {
        int val = (int)GameManager.GetManager().dayController.currentDay;
        currList = BitDays[val].bits.ToList();
    }
}
