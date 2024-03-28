using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimingMiniGame : MonoBehaviour
{
    [SerializeField]
    private int speed = 450; //슬라이더 바 이동 속도
    private Slider slider; //슬라이더
    [SerializeField]
    private float minPos; //성공 판정 최소 위치
    [SerializeField]
    private float maxPos; //성공 판정 최대 위치
    [SerializeField]
    private RectTransform successRange; //성공 판정
    [SerializeField]
    private bool isSuccess; //성공 여부
    public event Action<bool> OnEndMiniGame; //미니게임 종료 이벤트
    public void StartMiniGame() //미니게임 시작 함수
    {
        gameObject.SetActive(true); //미니게임 UI 활성화
        slider = GetComponent<Slider>(); 
        slider.interactable = false; //슬라이더 클릭 방지
        successRange.anchoredPosition = new Vector2(UnityEngine.Random.Range(0, slider.maxValue - successRange.sizeDelta.x), 0);
        slider.value = 1;
        minPos = successRange.anchoredPosition.x;
        maxPos = successRange.sizeDelta.x + minPos; //성공 판정 위치 설정
        isSuccess = true; //성공으로 시작
        StartCoroutine(MiniGame());// 미니게임 시작
    }
    IEnumerator MiniGame() //미니게임 코루틴
    {
        yield return null;
        int sign = 1; //음수 양수 지표
        while (!(Input.GetKey(KeyCode.Space))) //스페이스 입력 전까지
        {
            if (slider.value == slider.maxValue) sign = -1; //최대치 찍으면 음수로
            if (slider.value == 0) sign = 1; //최소치 찍으면 다시 양수로
            slider.value += Time.deltaTime * sign * speed; //슬라이더 왔다갔다
            yield return null;
        }
        if (slider.value < minPos || slider.value > maxPos) isSuccess = false; //판정 벗어날 시 실패
        Debug.Log(slider.value + "" + isSuccess + "미니게임 종료");
        OnEndMiniGame?.Invoke(isSuccess); //미니게임 종료 이벤트 발생
        gameObject.SetActive(false); //미니게임 UI 비활성화
    }
}
