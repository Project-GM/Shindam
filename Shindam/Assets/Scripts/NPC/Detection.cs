using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detection : MonoBehaviour
{
    public GameObject actionMark;
    public float detectionRange;

    private Transform playerTransform;

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        actionMark.SetActive(false);
    }

    void Update()
    {
        CheckPlayerDistance();
    }

    private void CheckPlayerDistance()
    {
        if (Vector2.Distance(transform.position, playerTransform.position) <= detectionRange)
        {
            actionMark.SetActive(true);
        }
        else
        {
            actionMark.SetActive(false);
        }
    }
}
