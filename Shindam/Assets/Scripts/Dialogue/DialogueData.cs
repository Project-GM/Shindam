using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueLine
{
    [Tooltip("ID")]
    public int ID;
    [Tooltip("화자 이름")]
    public string speakerName;
    [Tooltip("초상화 이미지")]
    public Sprite portrait;
    [Tooltip("대사 내용")]
    public string context;
    [Tooltip("선택지 유무")]
    public bool isOptionExist;
    [Tooltip("선택지 1 텍스트")]
    public string option1Text;
    [Tooltip("선택지 2 텍스트")]
    public string option2Text;
    [Tooltip("선택지 1 다음 대사 ID")]
    public int option1Id;
    [Tooltip("선택지 2 다음 대사 ID")]
    public int option2Id;
    [Tooltip("미니게임 유무")]
    public bool hasMiniGame;
    [Tooltip("미니게임 차 ID")]
    public int miniGameTeaId;
}

public class DialogueData : ScriptableObject
{
    public List<DialogueLine> dialogueDBs = new List<DialogueLine>();
}
