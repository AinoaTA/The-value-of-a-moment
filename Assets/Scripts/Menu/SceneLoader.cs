using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private string[] LevelNames;
    [SerializeField] private string[] IntroLevelsNames;
    private string LoadingSceneName;
    private Slider sliderLoading;
    private bool sliderMoving;
    private void Awake()
    {
        if (GameManager.GetManager().sceneLoader == null)
        {
            GameManager.GetManager().sceneLoader = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (GameManager.GetManager().sceneLoader != this)
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        Time.timeScale = 1;

        GameManager.GetManager().sceneLoader = this;
        LevelNames = GameManager.GetManager().levelData.sceneNames;
        IntroLevelsNames = GameManager.GetManager().levelData.sceneIntroNames;
    }
    /// <summary>
    /// Load desired scene with out loading scene.
    /// </summary>
    /// <param name="level"></param>
    /// 
    public void LoadLevel(int level)
    {
        if (LoadingSceneName != LevelNames[level] && LevelNames.Length > level)
        {
            LoadingSceneName = LevelNames[level];
            LoadSceneAsync(LoadingSceneName);
        }
        else
            Debug.Log(LevelNames[level] + "scene doesn't exit. Cant be loaded.");
    }

    private void LoadSceneAsync(string name)
    {
        SceneManager.LoadSceneAsync(name);
    }

    /// <summary>
    /// Load Scene with a loading scene to charge.
    /// When It finish the desired scene has been charged.
    /// </summary>
    /// <param name="scene"></param>
    public void LoadWithLoadingScene(int level, bool fromMenu = false)
    {
        if (fromMenu && (LoadingSceneName != IntroLevelsNames[level] && IntroLevelsNames.Length > level))
        {
            LoadingSceneName = IntroLevelsNames[level];

            StartCoroutine(LoadLoadingSceneFromMenu(LoadingSceneName));
            sliderLoading = FindObjectOfType<MenuController>().loadingSlider;
        }
        else if (LoadingSceneName != LevelNames[level] && LevelNames.Length > level)
        {
            LoadingSceneName = LevelNames[level];
            StartCoroutine(LoadLoadingScene(LoadingSceneName));
        }
    }
    IEnumerator LoadLoadingScene(string scene)
    {
        AsyncOperation l_LoadLevel = SceneManager.LoadSceneAsync(scene);
        l_LoadLevel.allowSceneActivation = false;
        yield return new WaitForSeconds(1f);
        while (!l_LoadLevel.isDone)
        {
            if (l_LoadLevel.progress >= 0.9f)
            {
                yield return new WaitForSeconds(1f);
                l_LoadLevel.allowSceneActivation = true;
            }
            yield return null;
        }
    }

    IEnumerator LoadLoadingSceneFromMenu(string scene)
    {
        yield return new WaitForSeconds(0.5f);
        AsyncOperation l_LoadLevel = SceneManager.LoadSceneAsync(scene);
        l_LoadLevel.allowSceneActivation = false;
        yield return new WaitForSeconds(1f);

        StartCoroutine(ForcedSlider(0.3f));
        yield return new WaitUntil(() => !sliderMoving);
        yield return new WaitForSeconds(1);
        StartCoroutine(ForcedSlider(0.8f));
        yield return new WaitUntil(() => !sliderMoving);
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => l_LoadLevel.progress >= 0.9f);
        StartCoroutine(ForcedSlider(1f));
        yield return new WaitUntil(() => !sliderMoving);
        yield return new WaitForSeconds(0.75f);
        l_LoadLevel.allowSceneActivation = true;

        //while (!l_LoadLevel.isDone)
        //{
        //    if (l_LoadLevel.progress >= 0.9f)
        //    { 
        //        yield return new WaitForSeconds(2f);
        //        l_LoadLevel.allowSceneActivation = true;
        //    }
        //    yield return null;
        //}
        //l_LoadLevel.completed += (asyncOperation) =>
        //{
        //    GameManager.GetManager().GetLevelData().GameStarted = true;
        //    StartCoroutine(GameManager.GetManager().StartGame());
        //};

        IEnumerator ForcedSlider(float val)
        {
            sliderMoving = true;
            float t = 0;
            float curr = sliderLoading.value;
            while (t < 1f)
            {
                t += Time.deltaTime;
                sliderLoading.value = Mathf.Lerp(curr, val, t / 1f);

                yield return null;
            }
            sliderMoving = false;
        }
    }
}
