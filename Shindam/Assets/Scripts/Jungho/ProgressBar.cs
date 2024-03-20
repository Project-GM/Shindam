using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ProgressBar : MonoBehaviour
{
    public Slider slider;

    void OnEnable()
    {
        slider.value = 0f;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // 랜덤하게 값을 설정하여 업데이트
            SetProgress(0.033f);
        }
    }
    public void SetProgress(float value)
    {
        // 프로그레스 바의 값을 변경
        slider.value += value;
        if (slider.value == 0.99f)
        {
            slider.value = 1f;
        }
    }
}
