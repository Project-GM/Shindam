using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueOption
{
    public string text;
    public DialogueNode nextNode;
}

[System.Serializable]
public class DialogueNode
{
    public string npcName;
    public Sprite portrait;
    public string dialogueText;
    public List<DialogueOption> options = new List<DialogueOption>();
    public bool isEndNode = false;
}

public class Dialogue_Old
{
    public List<DialogueNode> dialoguePages = new List<DialogueNode>();
}
