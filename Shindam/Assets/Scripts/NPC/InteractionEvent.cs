using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// NPC�� dialogue ����ִ� ��ũ��Ʈ
/// </summary>
public class InteractionEvent : MonoBehaviour
{
    [SerializeField] DialogueEvent dialogue;

    public Dialogue[] GetDialogues()
    {
        dialogue.dialogues = DialogueDataManager.instance.GetDialogue((int)dialogue.line.x, (int)dialogue.line.y);
        return dialogue.dialogues;
    }
}
