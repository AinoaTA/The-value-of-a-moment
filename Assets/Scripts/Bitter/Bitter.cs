using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class Bitter : MonoBehaviour
{
    public Image profilePic;
    public TMP_Text arroba;
    public TMP_Text message;
    public TMP_Text favs;
    public TMP_Text no_favs;
    public bool isBad;
    public ParticleSystem good, bad;

    public Vector2 isBadNumber, isGoodNumber;

    public void Fav()
    {
        //good.Play();
    }

    public void NoFav()
    {
      //  bad.Play();
    }


    public void UpdateFavs()
    {
        int good = (int)Random.Range(isGoodNumber.x,isGoodNumber.y);
        int bad = (int)Random.Range(isBadNumber.x, isBadNumber.y);
        if (isBad)
        {
            favs.text = (good / (int)Random.Range(2, 5)).ToString();
            no_favs.text = bad.ToString();
        }
        else
        {
            favs.text = good.ToString();
            no_favs.text =( bad/(int)Random.Range(2, 5)).ToString();
        }
    }
}
