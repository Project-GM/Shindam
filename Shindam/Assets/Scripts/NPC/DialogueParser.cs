using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// CSV파일 파싱하는 클래스 
/// </summary>
public class DialogueParser : MonoBehaviour
{
    public Dialogue[] Parse(string csvFileName)
    {
        List<Dialogue> dialogueList = new List<Dialogue>(); //대화 모음집 컨테이너
        TextAsset csvData = Resources.Load<TextAsset>(csvFileName);

        string[] data = csvData.text.Split(new char[] { '\n' });

        for (int i = 1; i < data.Length; i++)
        {
            string[] row = data[i].Split(new char[] { ',' });

            Dialogue dialogue = new Dialogue(); //'대화' 컨테이너

            dialogue.speakerName = row[1];  //화자 이름 넣기
            dialogue.context = row[3];
            if (row[4] == "0") { dialogue.isOptionExist = false; }
            else { dialogue.isOptionExist = true; }
            dialogue.option1Text = row[5];
            dialogue.option2Text = row[6];
            if (row[9] == "TRUE") { dialogue.hasMiniGame = true; }
            else { dialogue.hasMiniGame = false; }

            dialogueList.Add(dialogue); //대화 모음집에 대화 추가
        }
        return dialogueList.ToArray();  //대화 모음집 배열로 반환
    }

    private void Start() 
    {
        Parse("DialogueTestText");
    }
}
