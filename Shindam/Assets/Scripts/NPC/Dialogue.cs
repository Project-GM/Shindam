using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    [Tooltip("화자 이름")]
    public string speakerName;
    [Tooltip("대사 내용")]
    public string context;
    [Tooltip("선택지 유무")]
    public bool isOptionExist;
    [Tooltip("선택지 1 텍스트")]
    public string option1Text;
    [Tooltip("선택지 2 텍스트")]
    public string option2Text;
    [Tooltip("미니게임 유무")]
    public bool hasMiniGame;
    [Tooltip("미니게임 유무")]
    public int miniGameTeaId;
}

[System.Serializable]
public class DialogueEvent
{
    public Vector2 line;    //x부터 y까지의 대사 추출
    public Dialogue[] dialogues;
} 