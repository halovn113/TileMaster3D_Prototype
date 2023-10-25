using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode()]
public class UIProgressBar : MonoBehaviour
{
    public float min;
    public float max;
    public float current;
    public Image fillImage;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        GetCurrent();
    }

    // get current progress
    public void GetCurrent()
    {
        float currentOffset = current - min;
        float maxOffset = max - min;
        float fillAmount = currentOffset / maxOffset;
        fillImage.fillAmount = fillAmount;
    }
}
