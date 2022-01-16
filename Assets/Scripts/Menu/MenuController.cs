using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public AudioSource MusicAudio;
    public Animator m_Anim;

    private void Start()
    {
        StartCoroutine(IcreaseAudioCo());
    }
    public void StartGame()
    {
        
        StartCoroutine(LoadScene());
    }

    public void OptionsMenu()
    {
        m_Anim.SetTrigger("Options");
        
    }
    public void OptionsBack()
    {
        m_Anim.SetTrigger("Menu");

    }

    private IEnumerator LoadScene()
    {

        StartCoroutine(DecreaseAudioCo());
        int i = SceneManager.GetActiveScene().buildIndex + 1;
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(i);
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
}
