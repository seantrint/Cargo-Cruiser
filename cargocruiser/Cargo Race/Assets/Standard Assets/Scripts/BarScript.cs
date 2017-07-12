using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarScript : MonoBehaviour {

    [SerializeField]
    private float fillAmount;
    [SerializeField]
    private Image content;
    [SerializeField]
    private float lerpSpeed;

    private float delay = 3f;
    private float nextUsage;

    public float maxValue { get; set; }

    public float Value
    {
        set
        {
            fillAmount = Map(value, 0, maxValue, 0, 1);
        }
    }
    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        updateBar();
	}
    private void updateBar()
    {

        if (fillAmount != content.fillAmount)
        {
            content.fillAmount = Mathf.Lerp(content.fillAmount, fillAmount, Time.deltaTime * lerpSpeed);
        }

    }
    private float Map(float value, float inmin, float inmax, float outmin, float outmax )
    {
        return (value - inmin) * (outmax - outmin) / (inmax - inmin) + outmin;
    }
}

