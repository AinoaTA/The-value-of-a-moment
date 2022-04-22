using UnityEngine;

public class Interactables : MonoBehaviour
{
    public bool m_Done;
    public VoiceOff[] m_PhrasesVoiceOff;
    public VoiceOff[] m_HelpPhrasesVoiceOff;
    public string[] m_AnswersToVoiceOff;
    public string[] m_InteractPhrases;
    public float m_MaxAutoControl, m_MiddleAutoControl, m_MinAutoControl; 

    public GameObject OptionsCanvas;
    public Animator anim;

    private Material[] m_Material;

    public virtual bool GetDone() { return m_Done; }
    public virtual VoiceOff[] GetPhrasesVoiceOff() { return m_HelpPhrasesVoiceOff; }
    public virtual void Interaction() {}


    private void Start()
    {
        if (this.gameObject.GetComponent<Renderer>() != null)
        {
            m_Material = this.gameObject.GetComponent<Renderer>().materials;
        }
        else
        {
            Debug.Log("Mouse is not attached to the scene");
        }
    }

    public virtual void ShowCanvas()
    {
        if (GameManager.GetManager().m_CurrentStateGame == GameManager.StateGame.GamePlay)
        {
            Debug.Log("showing");
            anim.SetTrigger("Show");
            //OptionsCanvas.SetActive(!OptionsCanvas.activeSelf);
        }
    }

    public virtual void HideCanvas()
    {
        if (GameManager.GetManager().m_CurrentStateGame == GameManager.StateGame.GamePlay)
        {
            anim.SetTrigger("Hide");
        }
    }

    public virtual void ResetInteractable()
    {
        m_Done = false;
    }

    private void OnMouseOver()
    {
        //If your mouse hovers over the GameObject with the script attached, output this message
        // Debug.Log($"Mouse is over {this.gameObject}");

        if(m_Material != null && m_Material.Length > 0)
        {
            foreach (var material in this.gameObject.GetComponent<Renderer>().materials)
            {
                material.color = Color.red;
            }
        }
        else {
            Debug.Log("Mouse is not attached to the scene");
        }
    }

    private void OnMouseExit()
    {
        if (m_Material != null && m_Material.Length > 0)
        {
            for (int i = 0; i < m_Material.Length; i++)
            {
                print(m_Material[i].color);
                print(Color.black);
                // TODO: need to recover the m_Material[i].color as such
                this.gameObject.GetComponent<Renderer>().materials[i].color = Color.black;
            }
        }
    }
}
