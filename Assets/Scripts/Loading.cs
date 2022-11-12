using UnityEngine.UI;
using UnityEngine;
using TMPro;
using System.Collections;

public class Loading : MonoBehaviour
{
    public Slider fillSlider;
    public float loaderTime = 5;
    public TMP_Text loadingText;
    public string[] loadingTextChange;

    public bool m_Bubble = false;
    public float m_Multiplayer = 1.7f;
    public Animator fade;


    private void Start()
    {
        GameManager.GetManager().levelData.isLoadingSceneActive = true;
        StartCoroutine(ChangeText());
        StartCoroutine(LoadSlider());
    }

    void Update()
    {
        loadingText.ForceMeshUpdate();
        var textInfo = loadingText.textInfo;
        for (int i = 0; i < textInfo.characterCount; ++i)
        {
            var charInfo = textInfo.characterInfo[i];

            if (!charInfo.isVisible)
            {
                continue;
            }

            var vertexs = textInfo.meshInfo[charInfo.materialReferenceIndex].vertices;
            int idx = charInfo.vertexIndex;
            if (m_Bubble)
            {
                //vertexs[charInfo.vertexIndex + j] = ;
                Vector3 change = Wobble(Time.time + i);
                vertexs[idx] += change;
            }
            else
            {
                Vector3 offset = Move(Time.time + i);
                vertexs[idx] += offset;
                vertexs[idx + 1] += offset;
                vertexs[idx + 2] += offset;
                vertexs[idx + 3] += offset;
            }
        }

        for (int k = 0; k < textInfo.meshInfo.Length; ++k)
        {
            var meshInfo = textInfo.meshInfo[k];
            meshInfo.mesh.vertices = meshInfo.vertices;
            loadingText.UpdateGeometry(meshInfo.mesh, k);
        }
    }

    IEnumerator LoadSlider()
    {
        float timer = 0;
        yield return new WaitForSeconds(0.5f);
        while (timer < loaderTime)
        {
            timer += Time.deltaTime;
            fillSlider.value = Mathf.Lerp(0, 1, timer / 5);

            yield return null;
        }
        yield return new WaitForSeconds(0.25f);
        fade.Play("Fade");
        yield return new WaitForSeconds(0.5f);
        GameManager.GetManager().levelData.isLoadingSceneActive = false;
    }

    IEnumerator ChangeText()
    {
        int index = 0;
        while (true)
        {
            loadingText.text = loadingTextChange[index];
            yield return new WaitForSeconds(1f);
            index++;
            if (index >= loadingTextChange.Length) index = 0;
        }
    }

    Vector2 Move(float time)
    {
        return new Vector2(Mathf.Sin(time * m_Multiplayer), Mathf.Cos(time * 0.9f));
    }

    Vector3 Wobble(float time)
    {
        return new Vector3(0, Mathf.Sin(Time.time * 2f) * 2f, 1);
    }
}
