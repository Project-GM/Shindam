using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCs : MonoBehaviour
{
    public GameObject actionMark;
    public float detectionRange;

    private Transform playerTransform;

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        actionMark.SetActive(false);
        DialogueManager.isTalking = false;
    }

    void Update()
    {
        ActionMark();
        OnDialogue();
    }

    private void OnDialogue()
    {
        if(IsinRange() && Input.GetKeyDown(KeyCode.E))
        {
            DialogueManager.isTalking = true;
        }
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