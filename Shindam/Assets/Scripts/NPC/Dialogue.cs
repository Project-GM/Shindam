using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    [Tooltip("화자 이름")]
    public string speakerName;
    [Tooltip("대사 내용")]
    public string[] contexts;
}

[System.Serializable]
public class DialogueEvent
{
    public string eventName;

    public Vector2 line;    //x부터 y까지의 대사 추출
    public Dialogue[] dialogues;
}