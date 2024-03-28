using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static TimeManager instance;
    public float oneDayTime = 90f;
    public TextMeshProUGUI timeText;
    [SerializeField]
    private float currentTime;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(gameObject);
        }
        //DontDestroyOnLoad(gameObject);
    }

    void Start()
    {        
        currentTime = oneDayTime;
    }

    // Update is called once per frame
    void Update()
    {
        if(timeText==null) timeText = GameObject.FindGameObjectWithTag("TimeText").GetComponent<TextMeshProUGUI>();
        currentTime -= Time.deltaTime;
        if (currentTime <= oneDayTime * 0.33f)
        {
            timeText.text = "저녁";
        }
        else if (currentTime <= oneDayTime * 0.66f)
        {
            timeText.text = "점심";
        }
        else
        {
            timeText.text = "아침";
        }
    }

    public string GetTime()
    {
        return timeText.text;
    }
}
