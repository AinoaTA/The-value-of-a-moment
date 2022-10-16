using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionController : MonoBehaviour
{
    public GameObject canvas;
    public FinalSceneBar bar;
    public Animator transitor;
    public float duration = 1.0f;
    public GameObject text;

    void Awake()
    {
        GameManager.GetManager().transitionController = this;
    }

    void Start()
    {
        text.SetActive(false);
        canvas.SetActive(false);
        transform.GetChild(0).gameObject.SetActive(true);
    }

    public void LoadFinalScene()
    {
        StartCoroutine(LoadScene());
    }

    IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(duration);
        // Fundido a negro
        transitor.SetTrigger("Start");
        // Activamos canvas final
        canvas.SetActive(true);
        // Bloqueamos los interactables
        GameManager.GetManager().blockController.BlockAll(true);
        // Esperamos a que termine el fundido
        yield return new WaitForSeconds(duration);

        // Activamos animacion de la barra
        bar.ActivateAnim();

        yield return new WaitForSeconds(4f);
        text.SetActive(true);
    }
}
