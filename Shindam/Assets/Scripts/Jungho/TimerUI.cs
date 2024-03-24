using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimerUI : MonoBehaviour
{
    public TextMeshProUGUI timeText;
    public bool isGaming;
    public float timeLimit = 7f;
    private float time; // 초기값은 7초
    int sec, msec;

    void OnEnable()
    {
        // 초기값 설정 (7초)
        time = timeLimit;
        timeText.text = "7.0";
        isGaming = true;
    }

    void Update()
    {
        if (isGaming)
        {
            time -= Time.deltaTime;
            sec = (int)time;
            msec = (int)((time - sec) * 10); // 둘째 자리까지 표시

            if (time <= 0)
            {
                timeText.text = "0.0";
                isGaming = false; // 시간 종료 시 isGaming을 false로 설정
                time = 7;
            }
            else
            {
                timeText.text = sec.ToString() + "." + msec.ToString();
            }
        }
    }
}

