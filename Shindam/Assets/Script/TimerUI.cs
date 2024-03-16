using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimerUI : MonoBehaviour
{
    public TextMeshProUGUI[] timeText;
    public bool isGaming;
    [SerializeField]
    private float time = 7; // 초기값은 7초
    [SerializeField]
    private Image timer;
    int sec, msec;

    void Start()
    {
        // 초기값 설정 (7초)
        timeText[0].text = "07";
        timeText[1].text = "00";
        isGaming = false;
        timer.gameObject.SetActive(false); // 초기에는 비활성화 상태로 시작
    }

    void Update()
    {
        if (isGaming)
        {
            timer.gameObject.SetActive(true); // isGaming이 true일 때 활성화

            time -= Time.deltaTime;
            sec = (int)time;
            msec = (int)((time - sec) * 100); // 둘째 자리까지 표시

            if (time <= 0)
            {
                timeText[0].text = "00";
                timeText[1].text = "00";
                isGaming = false; // 시간 종료 시 isGaming을 false로 설정
                timer.gameObject.SetActive(false); // 이미지 비활성화
                time = 7;
            }
            else
            {
                timeText[0].text = sec.ToString("00");
                timeText[1].text = msec.ToString("00");
            }
        }
    }
}

