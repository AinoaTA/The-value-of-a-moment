using UnityEngine;

public class ButtonTrigger : MonoBehaviour
{
    public float Counter;
    //private float maxDistanceX=50f;
    //private float minDistanceX=10f;
    //private float maxDistanceY=50f;
    //private float mindistanceY=10f;
    //private ButtonTrigger current;
    //public ButtonTrigger other;

    //private float randomPos;

    public float ScaleValue = 0.004f;
    private Vector3 InitialScale;
    private Vector3 ScaleWakeUpButton;
    private Vector3 NewScaleWakeUpButton;
    private Vector3 WakeScale;


    private void Awake()
    {
        //current = GetComponent<ButtonTrigger>();
    }
    private void Start()
    {
        ScaleWakeUpButton = transform.localScale;
        InitialScale = transform.localScale;

        NewScaleWakeUpButton = new Vector3(ScaleValue, ScaleValue, ScaleValue);
        NewScaleWakeUpButton += ScaleWakeUpButton;
        
    }

    //WAKE UP BUTTON    
    public void LessEscaleWakeUp()
    {
        transform.localScale = ScaleWakeUpButton;
        
        gameObject.transform.localScale -= new Vector3(ScaleValue, ScaleValue, ScaleValue);
        ScaleWakeUpButton = gameObject.transform.localScale;

        if (Counter < 3)
            NewScaleWakeUpButton = ScaleWakeUpButton + new Vector3(0.001f, 0.001f, 0.001f);
        
        Counter++;
    }

    public void RandomWakeUp()
    {

    }

    //BOTH
    public void ButtonEnterBoth()
    {
        transform.localScale = NewScaleWakeUpButton;
    }

    public void ButtonExitBoth()
    {
        if (Counter >= 4)
        {
            ScaleWakeUpButton = InitialScale;
            NewScaleWakeUpButton= ScaleWakeUpButton + new Vector3(ScaleValue, ScaleValue, ScaleValue);
        }
                
        else
          transform.localScale = ScaleWakeUpButton;
    }
}
