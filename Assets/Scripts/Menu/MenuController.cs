using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public AudioSource MusicAudio;
    public CanvasGroup loading, menuButtons, optionsButtons;
    public Slider loadingSlider;
    private IEnumerator routine;
    public Animator anim;
    // public Animator m_Anim;

    private void Start()
    {
        Screen.fullScreen = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        StartCoroutine(routine = IcreaseAudioCo());
    }
    public void StartGame()
    {
        StartCoroutine(LoadScene());
    }

    public void OptionsMenu()
    {
        StartCoroutine(HideCanvasGroup(menuButtons, 0.25f, 0));
        StartCoroutine(ShowCanvasGroup(optionsButtons, 1,1.5f, true));
        anim.Play("Show");
    }
   
    public void OptionsBack()
    {
        StartCoroutine(HideCanvasGroup(optionsButtons, 0.25f,0));
        StartCoroutine(ShowCanvasGroup(menuButtons, 1 , 1.5f, true));
        anim.Play("Hide");
    }
    public void ExitGame()
    {
        Application.Quit();
    }

    private IEnumerator LoadScene()
    {
        menuButtons.gameObject.SetActive(false);
        StartCoroutine(ShowCanvasGroup(loading, 1));
        StartCoroutine(DecreaseAudioCo());
        yield return new WaitForSeconds(0.35f);
        GameManager.GetManager().sceneLoader.LoadWithLoadingScene(0, true);
        yield return null;
    }

    private IEnumerator DecreaseAudioCo()
    {
        StopCoroutine(routine);
        float counter = 0f;

        while (counter < 0.5f)
        {
            counter += Time.deltaTime;

            MusicAudio.volume = Mathf.Lerp(1f, 0f, counter / 0.5f);

            yield return null;
        }
    }
    private IEnumerator IcreaseAudioCo()
    {
        float counter = 0f;

        while (counter < 4f)
        {
            counter += Time.deltaTime;

            MusicAudio.volume = Mathf.Lerp(0f, 1f, counter / 4f);

            yield return null;
        }
    }

    IEnumerator ShowCanvasGroup(CanvasGroup canvas, float alpha, float time=1, bool interactable = false)
    {
        yield return new WaitForSeconds(0.5f);
        float t = 0;
        float currAlpha = canvas.alpha;
        while (t < time)
        {
            t += Time.deltaTime;
            canvas.alpha = Mathf.Lerp(currAlpha, alpha, t / time);
            yield return null;
        }

        if (interactable)
        {
            canvas.interactable = true;
            canvas.blocksRaycasts = true;
        }
    }

    IEnumerator HideCanvasGroup(CanvasGroup canvas, float time=1, float alpha = 0)
    {
        float t = 0;
        float currAlpha = canvas.alpha;
        while (t < time)
        {
            t += Time.deltaTime;
            canvas.alpha = Mathf.Lerp(currAlpha, alpha, t / time);
            yield return null;
        }

        canvas.interactable = false;
        canvas.blocksRaycasts = false;

    }
}
