using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// CSV���� �Ľ��ϴ� Ŭ����
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

            Dialogue dialogue = new Dialogue(); //��� ����Ʈ

            dialogue.speakerName = row[1];
            List<string> contextList = new List<string>();

            //csv���Ͽ� �����ִ� ��縦 contextList�� �ִ� �۾�
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