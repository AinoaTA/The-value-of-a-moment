using UnityEngine.UI;
using UnityEngine;
using TMPro;
using System.Collections;

public class Bitter : MonoBehaviour
{
    public Image profilePic;
    public TMP_Text arroba;
    public TMP_Text message;
    public TMP_Text favs;
    public TMP_Text no_favs;
    public bool isBad;

    public Vector2 isBadNumber, isGoodNumber;
    public Vector2 numbersFavsPlayer;

    public void Fav()
    {
        if (routine2 == null)
            StartCoroutine(routine2 = BitUpdateFavs(favs,true));
    }
    public void NoFav()
    {
        if (routine == null)
            StartCoroutine(routine = BitUpdateFavs(no_favs,false));
    }


    public void UpdateFavs()
    {
        int good = (int)Random.Range(isGoodNumber.x,isGoodNumber.y);
        int bad = (int)Random.Range(isBadNumber.x, isBadNumber.y);
        if (isBad)
        {
            favs.text = good.ToString();
            no_favs.text = (bad / (int)Random.Range(2, 5)).ToString();
        }
        else
        {
            favs.text = (good / (int)Random.Range(2, 5)).ToString();
            no_favs.text = bad.ToString();
        }
    }

    public void UpdateFavsPlayer() 
    {
        int good = (int)Random.Range(numbersFavsPlayer.x, numbersFavsPlayer.y);
        int bad = (int)Random.Range(numbersFavsPlayer.x, numbersFavsPlayer.y);

        favs.text = good.ToString();
        no_favs.text = bad.ToString();
    }

    IEnumerator routine, routine2;
    IEnumerator BitUpdateFavs(TMP_Text text, bool add) 
    {
        int times = (int)Random.Range(1,10);
        int number = int.Parse(text.text);
        int cur;
        float t=0;
        int desireValue = add ? number + times : number - times;
        while (t<1)
        {
            t += Time.deltaTime;
            cur = (int)Mathf.Lerp(number, desireValue, t);
            text.text = cur.ToString();
            yield return null;
        }
        yield return null;

        if (add) routine = null;
        else routine2 = null;
    }
}
