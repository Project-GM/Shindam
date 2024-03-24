using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
/// <summary>
/// 스토리 대화 시스템
/// </summary>
public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;
    public GameObject dialogueUi;    // 대사를 표시할 패널
    public TextMeshProUGUI nameText; // 화자 이름
    public TextMeshProUGUI dialogueText;    // 대사 내용
    public GameObject option1Panel; // 선택지1을 표시할 패널
    public GameObject option2Panel; //선택지2를 표시할 패널
    public TextMeshProUGUI option1Text; //선택지1 텍스트
    public TextMeshProUGUI option2Text; //선택지2 텍스트
    public static InteractionEvent speakerNpcInfo = null;     //플레이어가 선택한 NPC 정보

    private int dialogueIndex = 0;  //DialougeEvent클래스의 dialogues 배열의 인덱스
    private DialogueEvent dialogue = new DialogueEvent(); //표시할 대화
    public bool isDialogueFinish = false;  //대화 종료 여부 확인용
    private bool isMiniGamePlaying = false;
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
        if (!isMiniGamePlaying)
        {
            if (dialogueUi.activeSelf && !option1Panel.activeSelf && (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)))
            {
                if (dialogueIndex >= dialogue.dialogues.Length)
                {
                    Debug.Log("End of Dialogue");
                    EndDialogue();
                }
                else if (!dialogue.dialogues[dialogueIndex].hasMiniGame)  //이번 대사에 미니게임이 포함되어 있지 않으면
                {
                    StartCoroutine(TypeWriter());
                }
                else
                {
                    isMiniGamePlaying = true;
                    StartCoroutine(StoryMiniGame());
                }
            }
        }
    }

    IEnumerator StoryMiniGame()
    {
        CraftingSystem craftingSystem = FindAnyObjectByType<CraftingSystem>();
        craftingSystem.StartCrafting(dialogue.dialogues[dialogueIndex].miniGameTeaId);
        
        while (craftingSystem.isCrafting)
        {
            yield return null;
        }

        if (craftingSystem.isSuccess)
        {
            Debug.Log("isSuccess");
            isMiniGamePlaying = false;
            StartCoroutine(TypeWriter());
        }
        else
        {
            Debug.Log("Fail");
            isMiniGamePlaying= false;
            dialogueIndex -= 2;
            StartCoroutine(TypeWriter());
        }
        
        yield return null;
    }

    public void StartDialogue()
    {
        if (!isDialogueFinish)
        {
            SetDialogue();
            dialogueUi.SetActive(true);
            StartCoroutine(TypeWriter());
        }
    }

    public void EndDialogue()
    {
        Debug.Log("EndDialogue");
        dialogueUi.SetActive(false);
        isDialogueFinish = true;
        PlayerAction.s_Instance.isInteracting = false;
    }

    void SetDialogue()
    {
        dialogue.dialogues = speakerNpcInfo.GetDialogues();
    }
    //대사 띄우는 코루틴
    IEnumerator TypeWriter()
    {
        Debug.Log("Start Coroutine");
        string replaceText; //#을 ,으로 바꿔주기위한 스트링 임시 컨테이너

        if (dialogue.dialogues[dialogueIndex].isOptionExist) //이번 대사에 선택지가 포함되어있으면
        {
            option1Panel.SetActive(true);       //선택지 버튼 활성화
            option2Panel.SetActive(true);
            
            replaceText = dialogue.dialogues[dialogueIndex].option1Text;
            replaceText = replaceText.Replace("#", ",");    //# to ,
            option1Text.text = replaceText;    //선택지1 대사 넣기

            replaceText = dialogue.dialogues[dialogueIndex].option2Text;
            replaceText = replaceText.Replace("#", ",");    //# to ,
            option2Text.text = replaceText;    //선택지2 대사 넣기
        }
        else    //없으면 선택지 버튼 비활성화
        {
            option1Panel.SetActive(false);
            option2Panel.SetActive (false);
        }

        nameText.text = dialogue.dialogues[dialogueIndex].speakerName; //화자 이름 넣기
        replaceText = dialogue.dialogues[dialogueIndex++].context;
        replaceText = replaceText.Replace("#", ",");    //# to ,

        dialogueText.text = replaceText;    //대사 넣기
        yield return null;
    }

    public void Option1Selected()   //선택지1 버튼 OnClick
    {
        StartCoroutine(TypeWriter());
        dialogueIndex++;
    }

    public void Option2Selected()   //선택지2 버튼 OnClick
    {
        dialogueIndex++;
        StartCoroutine(TypeWriter());
    }
}