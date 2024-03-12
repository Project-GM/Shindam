using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// NPC에 dialogue 집어넣는 스크립트 
/// </summary>
public class InteractionEvent : MonoBehaviour
{
    DialogueEvent dialogue = new DialogueEvent();
    public Vector2 line;

    public Dialogue[] GetDialogues()
    {
        dialogue.dialogues = DialogueDataManager.instance.GetDialogue((int)line.x, (int)line.y);
        return dialogue.dialogues;
    }
}
