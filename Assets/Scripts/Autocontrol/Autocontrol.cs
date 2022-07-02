using UnityEngine.UI;
using UnityEngine;
using System.Collections;

public class Autocontrol : MonoBehaviour
{
    private float maxValue = 100f;
    CanvasGroup canvasGroup;
    public float m_currentValue { get; private set; }
    public Slider m_Slider;

    public Image stateImage;
    public Image backgroundBar;
    public Sprite[] statesColor;
    public Sprite[] barBackGroundColor;

    public ParticleSystem particles;
    public RawImage rawImage;
    public RenderTexture renderTexture;
    [SerializeField] private Vector2Int renderTextureResolution;
    public Camera particlesCamera;
    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }
    private void Start()
    {
        m_currentValue = 10;
        GameManager.GetManager().Autocontrol = this;
        renderTextureResolution = new Vector2Int(Screen.currentResolution.width, Screen.currentResolution.height);
        renderTexture = new RenderTexture(renderTextureResolution.x, renderTextureResolution.y, 32);
        particlesCamera.targetTexture = renderTexture;
        rawImage.texture = renderTexture;
        m_Slider.value = m_currentValue / maxValue;
        UpdateAutcontrol();

    }

    public void AddAutoControl(float value)
    {
        StartCoroutine(AddC(value));
    }

    public void RemoveAutoControl(float value)
    {
        StartCoroutine(RemoveC(value));
    }

    IEnumerator RemoveC(float value)
    {
        particles.Play();

        for (int i = 0; i < value; i++)
        {
            if (m_currentValue > 0)
                m_currentValue -= 1;

            m_Slider.value = m_currentValue / maxValue;

            UpdateAutcontrol();


            yield return null;
        }
        yield return new WaitForSeconds(1);
        particles.Stop();


    }

    IEnumerator AddC(float value)
    {
        particles.Play();
        for (int i = 0; i < value; i++)
        {
            if (m_currentValue < maxValue)
                m_currentValue += 1;

            m_Slider.value = m_currentValue / maxValue;
            UpdateAutcontrol();
            yield return null;
        }
        yield return new WaitForSeconds(1);
        particles.Stop();
    }

    public void UpdateAutcontrol()
    {
        if (m_Slider.value <= 0.3f)
        {
            stateImage.sprite = statesColor[0];
            backgroundBar.sprite = barBackGroundColor[0];
            print(GameManager.GetManager().SoundController);
            GameManager.GetManager().SoundController.ChangeMusicMood(0);
        }
        else if (m_Slider.value > 0.3f && m_Slider.value <= 0.5f)
        {
            stateImage.sprite = statesColor[1];
            backgroundBar.sprite = barBackGroundColor[1];
            GameManager.GetManager().SoundController.ChangeMusicMood(1);
        }
        else if (m_Slider.value > 0.5f && m_Slider.value <= 0.8f)
        {
            stateImage.sprite = statesColor[2];
            backgroundBar.sprite = barBackGroundColor[2];

            GameManager.GetManager().SoundController.ChangeMusicMood(2);

        }
        else if (m_Slider.value > 0.8f && m_Slider.value <= 1f)
        {
            stateImage.sprite = statesColor[3];
            backgroundBar.sprite = barBackGroundColor[3];

        }

    }
    /// <summary>
    /// 0 or 1 value.
    /// </summary>
    /// <param name="val"></param>
    public void ShowAutocontroler(int val = 0)
    {
        canvasGroup.alpha = val;
    }

    public void AutocontrolSleep()
    {
        StartCoroutine(RemoveC(m_currentValue * 0.4f));
    }

}
