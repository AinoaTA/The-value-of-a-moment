using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
   [SerializeField] private string[] m_LevelNames;
    private string m_LoadingSceneName;
    private Slider sliderLoading;
    private bool sliderMoving;
    private void Awake()
    {
        if (GameManager.GetManager().sceneLoader == null)
        {
            GameManager.GetManager().sceneLoader=this;
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
        m_LevelNames = GameManager.GetManager().levelData.sceneNames;
    }
    /// <summary>
    /// Load desired scene with out loading scene.
    /// </summary>
    /// <param name="level"></param>
    /// 
    public void LoadLevel(int level)
    {
        if (m_LoadingSceneName != m_LevelNames[level] && m_LevelNames.Length > level)
        {
           // GameManager.GetManager().levelData().ResetTotalTime();
            m_LoadingSceneName = m_LevelNames[level];
           // GameManager.GetManager().levelData().m_CurrentLevelPlayed = level;
            LoadSceneAsync(m_LoadingSceneName);
        }
        else
            Debug.Log(m_LevelNames[level] + "scene doesn't exit. Cant be loaded.");
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
        if (m_LoadingSceneName != m_LevelNames[level] && m_LevelNames.Length > level)
        {
            m_LoadingSceneName = m_LevelNames[level];
           // GameManager.GetManager().levelData().m_CurrentLevelPlayed = level;
            if (fromMenu)
            {
                StartCoroutine(LoadLoadingSceneFromMenu(m_LoadingSceneName));
                sliderLoading = FindObjectOfType<MenuController>().loadingSlider;
            }
            else
            {
                StartCoroutine(LoadLoadingSceneFromMenu(m_LoadingSceneName));
            }
        }
        else
            Debug.Log(level + " level doesn't exit or is already loaded.");
    }
    //IEnumerator LoadLoadingScene(string scene)
    //{
    //    //First load loading scene and save in var
    //    //also load loading scene
    //    SceneManager.LoadSceneAsync("Loading");
    //    yield return new WaitForSecondsRealtime(0.5f);
    //    AsyncOperation l_LoadLevel = SceneManager.LoadSceneAsync(scene);
    //    l_LoadLevel.allowSceneActivation = false;
    //    yield return new WaitForSecondsRealtime(1f);
    //    while (!l_LoadLevel.isDone)
    //    {
           

    //        // Check if the load has finished
    //        if (l_LoadLevel.progress >= 0.9f)
    //        {
    //            yield return new WaitForSecondsRealtime(3.5f);
    //            yield return new WaitForSecondsRealtime(2);
    //            l_LoadLevel.allowSceneActivation = true;
    //        }
    //        yield return null;
    //    }
    //    l_LoadLevel.completed += (asyncOperation) =>
    //    {
    //      //  GameManager.GetManager().levelData().m_GameStarted = true;
    //    };
    //}

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
        //    GameManager.GetManager().GetLevelData().m_GameStarted = true;
        //    StartCoroutine(GameManager.GetManager().StartGame());
        //};

        IEnumerator ForcedSlider(float val)
        {
            sliderMoving = true;
            float t = 0;
            float curr=sliderLoading.value;
            while(t<1f)
            {
                t += Time.deltaTime;
                sliderLoading.value = Mathf.Lerp(curr, val, t / 1f);

                yield return null;
            }
            sliderMoving = false;
        }
    }
}
