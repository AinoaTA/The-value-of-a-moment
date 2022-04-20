using UnityEngine;

public class Interactables : MonoBehaviour
{
    public bool m_Done;
    public VoiceOff[] m_HelpPhrases;
    public string[] m_InteractPhrases;
    public float m_MaxAutoControl, m_MiddleAutoControl, m_MinAutoControl; 

    public GameObject OptionsCanvas;
    public Animator anim;

    public virtual bool GetDone() { return m_Done; }
    public virtual VoiceOff[] GetPhrases() { return m_HelpPhrases; }
    public virtual void Interaction() { }

    public bool open;

    public virtual void ShowCanvas()
    {
        if (GameManager.GetManager().m_CurrentStateGame == GameManager.StateGame.GamePlay)
        {
            Debug.Log("showing");
            // anim.SetTrigger("Show");
            OptionsCanvas.SetActive(!OptionsCanvas.activeSelf);
            
            open = true;
        }
    }

    public virtual void HideCanvas()
    {
        if (GameManager.GetManager().m_CurrentStateGame == GameManager.StateGame.GamePlay)
        {
            anim.SetTrigger("Hide");
            open = false;
        }
    }

    public virtual void ResetInteractable()
    {
        m_Done = false;
    }

    private void OnMouseOver()
    {
        //If your mouse hovers over the GameObject with the script attached, output this message
        Debug.Log($"Mouse is over {this.gameObject}");

        if(this.gameObject.GetComponent<Renderer>() != null)
        {
            this.gameObject.GetComponent<Renderer>().material.SetFloat("_EmissiveExposureWeight", 0.95f);
        }
        else {
            Debug.Log("Mouse is not attached to the scene");
        }
    }

    private void OnMouseExit()
    {
        if (this.gameObject.transform.childCount > 0 && this.gameObject.transform.GetChild(0).GetComponent<Renderer>() != null) {

            foreach (var material in this.gameObject.transform.GetChild(0).GetComponent<Renderer>().materials) {
                material.SetFloat("_EmissiveExposureWeight", 1f);
            }
        }
        else {
            Debug.Log("Mouse is not attached to the scene");
        }
    }
}
