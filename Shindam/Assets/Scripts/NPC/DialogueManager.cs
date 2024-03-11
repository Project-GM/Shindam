using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 대화창 관리용
/// </summary>
public class DialogueManager : MonoBehaviour
{
    public GameObject dialogueUi;
    public static bool isTalking;

    void Start()
    {
        dialogueUi.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isTalking)
        {
            dialogueUi.SetActive(true);
        }
    }
}
