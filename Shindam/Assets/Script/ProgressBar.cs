using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ProgressBar : MonoBehaviour
{
    public Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        slider.value = 0f;
    }
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // 랜덤하게 값을 설정하여 업데이트
            SetProgress(0.03f);
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
