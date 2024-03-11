﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// CSV파일 파싱하는 클래스 
/// </summary>
public class DialogueParser : MonoBehaviour
{
    public Dialogue[] Parse(string csvFileName)
    {
        List<Dialogue> dialogueList = new List<Dialogue>();
        TextAsset csvData = Resources.Load<TextAsset>(csvFileName);

        string[] data = csvData.text.Split(new char[] { '\n' });

        for(int i=1; i < data.Length;)
        {
            string[] row = data[i].Split(new char[] { ',' });

            Dialogue dialogue = new Dialogue(); //대사 리스트

            dialogue.speakerName = row[1];
            List<string> contextList = new List<string>();

            //csv파일에 적혀있는 대사를 contextList에 넣는 작업
            do
            {
                contextList.Add(row[3]);

                if (++i < data.Length)
                {
                    row = data[i].Split(new char[] { ',' });
                }
                else
                {
                    break;
                }
            } while (row[0].ToString() == "");

            dialogue.contexts = contextList.ToArray();

            dialogueList.Add(dialogue);
        }

        return dialogueList.ToArray();
    }

    private void Start()
    {
        Parse("DialogueTestText");
    }
}
