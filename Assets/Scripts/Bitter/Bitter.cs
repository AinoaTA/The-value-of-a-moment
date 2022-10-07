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

    public void Fav()
    {
        good.Play();
    }

    public void NoFav()
    {
        bad.Play();
    }
}
