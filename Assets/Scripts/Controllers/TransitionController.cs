using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionController : MonoBehaviour
{
    public Animator transitor;
    public float duration = 1.0f;

    void Start()
    {
        transform.GetChild(0).gameObject.SetActive(true);
    }

    public void LoadNextScene(String sceneName)
    {
        StartCoroutine(LoadScene(sceneName));
    }

    IEnumerator LoadScene(String sceneName)
    {
        transitor.SetTrigger("Start");
        yield return new WaitForSeconds(duration);
        SceneManager.LoadScene(sceneName);
    }
}
