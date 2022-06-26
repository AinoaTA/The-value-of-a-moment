using UnityEngine;

public class LevelData : MonoBehaviour
{
    public enum Characters { Elle, Zoe, Ari}
    public Characters character;
    public string[] sceneNames;
    public string[] sceneIntroNames;
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
