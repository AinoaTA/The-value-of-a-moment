using UnityEngine.UI;
using System.Collections;
using UnityEngine;

public class IntroScrollView : MonoBehaviour
{
    ScrollRect a;
    public Button button;
    private void OnEnable()
    {
        Tira.buttonEnd += ShowButton;
    }

    private void OnDisable()
    {
        Tira.buttonEnd -= ShowButton;
    }
    void Awake()
    {
        a=GetComponent<ScrollRect>();
        a.enabled = false;
    }

    IEnumerator Start()
    {
        yield return new WaitForSeconds(1.5f);
        a.enabled = true;
    }

    private void ShowButton()
    {
        button.GetComponent<Animator>().Play("Show");
    }

    public void SceneLoad()
    {
        GameManager.GetManager().sceneLoader.LoadWithLoadingScene(1, false);
    }
}
