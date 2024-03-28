using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum GameTime
{
    morning, afternoon, night
}

public class TimeManager : MonoBehaviour
{
    public static TimeManager instance;
    public float oneDayTime = 900f;
    public TextMeshProUGUI timeText;
    [SerializeField]
    private float currentTime;

    /*    private void Awake()
        {
            if(instance == null)
            {
                instance = this;
            }
            else if(instance != this)
            {
                Destroy(gameObject);
            }
            DontDestroyOnLoad(gameObject);
        }*/

    void Start()
    {
        currentTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //if (timeText == null) timeText = GameObject.FindGameObjectWithTag("TimeText").GetComponent<TextMeshProUGUI>();
        currentTime += Time.deltaTime;

        if (GetTime() == GameTime.morning) { timeText.text = "아침"; }
        else if (GetTime() == GameTime.afternoon) { timeText.text = "점심"; }
        else { timeText.text = "저녁"; }
    }

    public GameTime GetTime()
    {
        if (currentTime <= oneDayTime * 0.33f)
        {
            return GameTime.morning;
        }
        else if (currentTime <= oneDayTime * 0.66f)
        {
            return GameTime.afternoon;
        }
        else
        {
            return GameTime.night;
        }
    }
}
