using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// NPC에 dialogue 집어넣는 스크립트
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
