using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Linq;

public class WriteBitter : MonoBehaviour
{
    [SerializeField]BitterControl bitterControler;
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
    [SerializeField]private GameObject acceptButton;

    int index;
    bool select;
    private void Start()
    {        //temp
        Debug.LogWarning("change for number day (another script will be manage this)");
        genericText.text = "";
    }

    bool wrote;
    public void WriteBit()
    {
        if (wrote) return;
        wrote = true;
        UpdateList();

        index = CheckCondition(currList);
        for (int i = 0; i < 3; i++)
        {
            if (i>=currList[index].possibleBitters.Length) return;
            texts[i].transform.parent.gameObject.SetActive(true);
            texts[i].text = currList[index].possibleBitters[i].resumeName;
        }
        currList.RemoveAt(index);
    }

    private int CheckCondition(List<Bitters> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i].conditionComplete)
                return i;
        }
        return 0;
    }

    public void SelectPossibleAnswer(int i)
    {
        select = true;
        genericText.text = currList[i].possibleBitters[i].bitter;
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
        select = false;
        wrote = false;
        ClearBitter();
    }

    void UpdateList() 
    {
        int val = (int)GameManager.GetManager().dayController.currentDay;
        currList = BitDays[val].bits.ToList();
    }
}
