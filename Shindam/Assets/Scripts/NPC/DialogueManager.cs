using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
/// <summary>
/// 대화창 ON/OFF용
/// </summary>
public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;
    public GameObject dialogueUi;
    public TextMeshProUGUI nameText; // 화자 이름을 표시할 Text 컴포넌트
    public TextMeshProUGUI dialogueText;    // 대사 내용을 표시할 Text 컴포넌트
    public static InteractionEvent speakerNpcInfo = null;     //플레이어가 선택한 NPC 정보

    int dialogueIndex = 0;
    int contextIndex = 0;
    DialogueEvent dialogue = new DialogueEvent(); //표시할 대화
    bool isNext = false;    //키 입력 대기

    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        dialogueUi.SetActive(false);
    }

    void Update()
    {
        if (dialogueUi.activeSelf && Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine("TypeWriter");
            if (contextIndex >= dialogue.dialogues.Length + 1)
            {
                EndDialogue();
                dialogueIndex++;
            }
        }
    }

    public void StartDialogue()
    {
        SetDialogue();
        dialogueUi.SetActive(true);
        StartCoroutine(TypeWriter());
    }

    public void EndDialogue()
    {
        dialogueUi.SetActive(false);
        PlayerAction.s_Instance.isInteracting = false;
    }

    void SetDialogue()
    {
        dialogue.dialogues = speakerNpcInfo.GetDialogues();
    }

    IEnumerator TypeWriter()
    {
        if (dialogue.dialogues[dialogueIndex].speakerName != "")
        {
            nameText.text = dialogue.dialogues[dialogueIndex].speakerName;
        }
        string replaceText = dialogue.dialogues[dialogueIndex].contexts[contextIndex++];
        replaceText = replaceText.Replace("#", ",");    //# into ,

        dialogueText.text = replaceText;
        yield return null;
    }
}