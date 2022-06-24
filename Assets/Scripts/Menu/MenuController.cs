using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public AudioSource MusicAudio;
    public CanvasGroup loading, menuButtons;
    public Slider loadingSlider;
   // public Animator m_Anim;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        StartCoroutine(IcreaseAudioCo());
    }
    public void StartGame()
    {
        
        StartCoroutine(LoadScene());
    }

    public void OptionsMenu()
    {
       // m_Anim.SetTrigger("Options");
        
    }
    public void OptionsBack()
    {
       // m_Anim.SetTrigger("Menu");

    }
    public void ExitGame()
    {
        Application.Quit();
    }

    private IEnumerator LoadScene()
    {
        menuButtons.gameObject.SetActive(false);
        StartCoroutine(CanvasGroupLoading(loading,1));
        StartCoroutine(DecreaseAudioCo());
        yield return new WaitForSeconds(0.35f);
        GameManager.GetManager().sceneLoader.LoadWithLoadingScene(1, true);
        yield return null;
    }

    private IEnumerator DecreaseAudioCo()
    {
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

        while (counter < 5f)
        {
            counter += Time.deltaTime;

            MusicAudio.volume = Mathf.Lerp(0f, 1f, counter / 1.5f);

            yield return null;
        }
    }

    IEnumerator CanvasGroupLoading(CanvasGroup canvas, float alpha)
    {
        float t = 0;
        float currAlpha = canvas.alpha;
        while (t < 0.3f)
        {
            t += Time.deltaTime;
            canvas.alpha = Mathf.Lerp(currAlpha, alpha, t / 0.3f);
            yield return null;
        }
    }
}
