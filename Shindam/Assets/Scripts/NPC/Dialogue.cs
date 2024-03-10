using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    [Tooltip("ȭ�� �̸�")]
    public string speakerName;
    [Tooltip("��� ����")]
    public string[] contexts;
}

[System.Serializable]
public class DialogueEvent
{
    public string eventName;

    public Vector2 line;    //x���� y������ ��� ����
    public Dialogue[] dialogues;
}