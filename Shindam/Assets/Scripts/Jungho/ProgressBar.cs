using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ProgressBar : MonoBehaviour
{
    [SerializeField]
    private Slider slider;
    private int mashCount;

    public int maxMashCount;

    public void Reset()
    {
        slider.maxValue = maxMashCount;
        slider.value = 0;
        mashCount = 0;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ButtonMash();
        }
    }
    public void ButtonMash()
    {
        if (mashCount < maxMashCount)
        {
            mashCount++;
            slider.value = mashCount;
        }
    }
    public int GetMashCount()
    {
        return mashCount;
    }
}
