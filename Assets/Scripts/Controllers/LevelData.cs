using UnityEngine;

public class LevelData : MonoBehaviour
{
    public string[] sceneNames;
    public bool startedGame;
    private void Awake()
    {
        if (GameManager.GetManager().levelData == null)
        {
            GameManager.GetManager().levelData = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (GameManager.GetManager().levelData != this)
        {
            Destroy(gameObject);
        }
    }
}
