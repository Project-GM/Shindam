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

    private int dialogueIndex = 0;
    private DialogueEvent dialogue = new DialogueEvent(); //표시할 대화
    private bool isDialogueFinish = false;

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
            if (dialogueIndex >= dialogue.dialogues.Length)
            {
                Debug.Log("End of Dialogue");
                EndDialogue();
            }
            else
            {
                StartCoroutine("TypeWriter");
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
        isDialogueFinish = true;
        PlayerAction.s_Instance.isInteracting = false;
    }

    void SetDialogue()
    {
        dialogue.dialogues = speakerNpcInfo.GetDialogues();
    }
    //선택지 시스템 구현에서 멈췄습니다..^^
    IEnumerator TypeWriter()
    {
        Debug.Log("Start Coroutine");
        if (dialogue.dialogues[dialogueIndex].speakerName != "")
        {
            nameText.text = dialogue.dialogues[dialogueIndex].speakerName;
        }
        string replaceText = dialogue.dialogues[dialogueIndex++].context;
        replaceText = replaceText.Replace("#", ",");    //# into ,

        dialogueText.text = replaceText;
        yield return null;
    }
}