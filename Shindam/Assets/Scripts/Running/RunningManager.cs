using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RunningManager : MonoBehaviour
{
    public static RunningManager instance;
    public bool isOpen = false;
    public TextMeshProUGUI runningText;
    // Start is called before the first frame update
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    void Start()
    {
        runningText.text = "영업 전";
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.instance.timeManager.GetTime() == GameTime.night)
        {
            CloseTeaHouse();
        }
    }

    public void OpenTeaHouse()
    {
        if(GameManager.instance.timeManager.GetTime() != GameTime.night)
        {
            isOpen = true;
            runningText.text = "영업 중";
        }
        else
        {
            Debug.Log("저녁엔 개업할 수 없습니다.");   //추후 UI 알림창에 띄울예정
        }
    }

    public void CloseTeaHouse()
    {
        isOpen = false;
        CustomerManager.instance.isSpawning = false;
        runningText.text = "영업 끝";
    }
}
