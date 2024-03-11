using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// ��ȭâ ������
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
