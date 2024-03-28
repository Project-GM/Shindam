using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor.Search;
using UnityEngine.Rendering;
using System;

public class InputNumber : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField inputField; //입력 창
    [SerializeField]
    private TMP_Text previewText; //최댓값 텍스트
    [SerializeField]
    private TMP_Text inputText; //입력 텍스트

    private bool activated = true; //입력 창 켜져있는지 확인

    private int itemIndex; //버릴 아이템 인덱스

    private String preview, input;

    public event Action<int, int> OnEndInput; //입력 종료 이벤트
    private void Start()
    {
        Hide();
    }
    private void Update()
    {
        if (activated) //켜져있으면
        {
            if (Input.GetKeyDown(KeyCode.Escape)) //ESC로 취소
            {
                Cancel();
            }
            else if (Input.GetKeyDown(KeyCode.Return)) //Enter로 확인
            {
                OK();
            }
        }
    }

    public void Show() //입력 창 열기
    {
        gameObject.SetActive(true);
    }
    public void Hide() //입력 창 닫기
    {
        gameObject.SetActive(false);
    }
    public void InitializeInputField(int index, string countText) //입력 창 초기화
    {
        Show();
        activated = true;
        inputField.text = "";
        previewText.text = countText;
        itemIndex = index;
        preview = previewText.text;
    }
    public void OK() //확인 함수
    {
        int throwCount;
        input = inputField.text; //TMP 텍스트는 무조건 공백문자 하나가 들어가기 때문에 String으로 변환
        if(input != "") //입력이 존재하면
        {
            if (CheckNumber(input)) //입력이 숫자면
            {
                throwCount = int.Parse(input); //버리기 개수 = 입력
                if (throwCount > int.Parse(preview)) //버리기 개수가 최대값보다 크면
                {
                    throwCount = int.Parse(preview); //버리기 개수 = 최대값
                }
            }
            else throwCount = 1; //입력이 숫자가 아니면 버리기 개수 = 1
        }
        else throwCount = int.Parse(preview); //입력 없으면 버리기 개수 = 최대값
        OnEndInput?.Invoke(itemIndex, throwCount); //입력 종료 이벤트 발동
        Hide(); //입력 창 닫기
    }
    public void Cancel() //취소 함수
    {
        activated = false;
        Hide();
    }
    public bool CheckNumber(string text) //숫자 판별 함수
    {
        char[] charArr = text.ToCharArray(); //입력을 char형 배열로
        bool isNumber = true;
        for(int i = 0; i < charArr.Length; i++) //입력 길이 만큼 반복
        {
            if (charArr[i] >= 48 && charArr[i] <= 57) continue; //숫자면 건너뛰기
            isNumber = false; //숫자 아니면 false
        }
        return isNumber;
    }
}
