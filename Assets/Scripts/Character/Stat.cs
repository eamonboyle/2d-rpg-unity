using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stat : MonoBehaviour
{
    public float MyMaxValue { get; set; }
    public float MyCurrentValue
    {
        get => currentValue;
        set
        {
            if (value > MyMaxValue)
            {
                currentValue = MyMaxValue;
            }
            else if (value < 0)
            {
                currentValue = 0;
            }
            else
            {
                currentValue = value;
            }

            currentFill = currentValue / MyMaxValue;

            if (statValue != null)
            {
                statValue.text = currentValue + "/" + MyMaxValue;
            }
        }
    }

    private Image content;
    [SerializeField]
    private Text statValue;

    private float currentFill;
    private float currentValue;
    [SerializeField]
    private float lerpSpeed;

    public void Initialize(float currentValue, float maxValue)
    {
        if (content == null)
        {
            content = GetComponent<Image>();
        }

        MyMaxValue = maxValue;
        MyCurrentValue = currentValue;
        content.fillAmount = MyCurrentValue / MyMaxValue;
    }

    // Start is called before the first frame update
    void Start()
    {
        content = GetComponent<Image>();
        currentFill = content.fillAmount;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentFill != content.fillAmount)
        {
            content.fillAmount = Mathf.Lerp(content.fillAmount, currentFill, Time.deltaTime * lerpSpeed);
        }
    }
}
