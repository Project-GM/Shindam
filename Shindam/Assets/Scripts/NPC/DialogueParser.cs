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

        for(int i=1; i < data.Length;)
        {
            string[] row = data[i].Split(new char[] { ',' });

            Dialogue dialogue = new Dialogue(); //'대화' 컨테이너

            dialogue.speakerName = row[1];  //화자 이름 넣기

            List<string> contextList = new List<string>();  //'대사' 컨테이너

            //csv파일에 적혀있는 '대사'를 contextList에 넣는 작업
            do
            {
                contextList.Add(row[3]);    //'대사' 넣기

                if (++i < data.Length)
                {
                    row = data[i].Split(new char[] { ',' });
                }
                else
                {
                    break;
                }
            } while (row[0].ToString() == "");  //대화가 종료되지 않았을 경우 반복 진행

            dialogue.contexts = contextList.ToArray();  //dialogue의 대사 컨테이너에 대사 리스트 배열로 저장

            dialogueList.Add(dialogue); //대화 모음집에 대화 추가
        }
            
        return dialogueList.ToArray();  //대화 모음집 배열로 반환
    }

    private void Start()
    {
        Parse("DialogueTestText");
    }
}
