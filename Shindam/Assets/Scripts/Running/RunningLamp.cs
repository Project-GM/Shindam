using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningLamp : MonoBehaviour
{
    public Transform playerTransform;
    public float detectionRange = 3f;
    public GameObject actionMark;
    public GameObject openLamp;
    public GameObject closeLamp;
    
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        ActionMark();
        if (actionMark.activeSelf && Input.GetKeyDown(KeyCode.E))
        {
            OnInteract();
        }
        if(TimeManager.instance.GetTime() == "저녁")
        {
            OffLamp();
            RunningManager.instance.CloseTeaHouse();
        }
    }

    private void OnInteract()
    {
        if (openLamp.activeSelf)
        {
            OffLamp();
            RunningManager.instance.CloseTeaHouse();
        }
        else if (closeLamp.activeSelf && TimeManager.instance.GetTime() != "저녁")
        {
            OnLamp();
            RunningManager.instance.OpenTeaHouse();
        }
    }

    private void OnLamp()
    {
        closeLamp.SetActive(false);
        openLamp.SetActive(true);
    }

    private void OffLamp()
    {
        openLamp.SetActive(false);
        closeLamp.SetActive(true);
    }

    private void ActionMark()
    {
        actionMark.SetActive(IsinRange());
    }

    private bool IsinRange()
    {
        if (Vector2.Distance(transform.position, playerTransform.position) <= detectionRange)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
