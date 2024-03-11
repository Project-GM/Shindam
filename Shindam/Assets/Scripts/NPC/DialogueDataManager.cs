using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueDataManager : MonoBehaviour
{
    public static DialogueDataManager instance;

    [SerializeField] string csvFileName;

    Dictionary<int, Dialogue> dialogueDictionary = new Dictionary<int, Dialogue>();

    public static bool isStoringFinish = false;

    void Awake()
    {
        //데이터의 내용을 dialogueDictionary에 저장
        if(instance == null)
        {
            instance = this;
            DialogueParser theParser = GetComponent<DialogueParser>();
            Dialogue[] dialogues = theParser.Parse(csvFileName);
            for(int i = 0; i< dialogues.Length; i++)
            {
                dialogueDictionary.Add(i+1, dialogues[i]);
            }
            isStoringFinish = true;
        }
    }

    public Dialogue[] GetDialogue(int startNum, int endNum)
    {
        List<Dialogue> dialogueList = new List<Dialogue>();

        for(int i=0; i<= endNum - startNum; i++)
        {
            dialogueList.Add(dialogueDictionary[startNum + i]);
        }

        return dialogueList.ToArray();
    }
}
 