using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class TimerUI : MonoBehaviour
{
    public TextMeshProUGUI timeText;
    public float timeLimit = 7f;
    private float time; // 초기값은 7초
    int sec, msec;

    public event Action OnEndTimer;

    public void Reset()
    {
        time = timeLimit;
        timeText.text = "7.0";
    }
    public void StartTimer()
    {
        StartCoroutine(Timer());
    }
    public IEnumerator Timer()
    {
        while (time > 0)
        {
            time -= Time.deltaTime;
            sec = (int)time;
            msec = (int)((time - sec) * 10);
            timeText.text = sec.ToString() + "." + msec.ToString();
            yield return null;
        }
        OnEndTimer?.Invoke();
    }
}

