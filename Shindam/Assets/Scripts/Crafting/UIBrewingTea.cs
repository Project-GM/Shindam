using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Linq;

public class UIBrewingTea : MonoBehaviour, IDropHandler
{
    [SerializeField]
    private CraftDB craftDB; //제조 DB
    [SerializeField]
    private TimingMiniGame miniGame; //타이밍 미니게임
    [SerializeField]
    private int craftID; //제조 번호
    [SerializeField]
    private Slider waterQuantitySlider; //물 채움 슬라이더
    [SerializeField]
    private Slider ingredientQuantitySlider; //재료 채움 슬라이더
    public event Action OnDropItem; //드롭 이벤트
    public event Action<bool> OnEndBrewingTea; //차 우리기 종료 이벤트
    [SerializeField]
    private List<ItemSO> ingredientList = new List<ItemSO>(); //재료 리스트
    [SerializeField]
    private int waterQuantity; //물 양
    private bool isSuccess; //성공 여부
    private bool isFinished; //미니게임 이전 행동 종료 여부(= 미니게임 시작 여부)
    private void Awake()
    {
        miniGame.OnEndMiniGame += EndBrewingTea; //미니게임 종료 이벤트에 차 우리기 종료 함수 할당
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) PourWater(); //물 붓기
    }
    public void InitializeBrewingTea(int craftID) //차 우리기 시작 함수
    {
        this.craftID = craftID;
        ResetData();
    }

    private void EndBrewingTea(bool isMiniGameSuccess) //차 우리기 종료 함수
    {
        if (isMiniGameSuccess == false) //미니게임 실패 시
        {
            OnEndBrewingTea?.Invoke(false); //바로 종료 후 리턴
            return;
        }
        CraftDBItem item = new CraftDBItem();
        for (int i = 0; i < craftDB.items.Count; i++) //제조 번호에 해당하는 재료 번호 탐색
        {
            if (craftDB.items[i].ID == craftID)
            {
                item = craftDB.items[i];
                break;
            }
        }
        isSuccess = item.ingredientQuantity == ingredientList.Count && item.waterQuantity == waterQuantity; //제조 번호에 해당하는 물 양과 재료 양이 일치하면 성공
        for (int i = 0; i < ingredientList.Count; i++)
        {
            if (ingredientList[i].ID != item.ingredientID) isSuccess = false; //재료 중에 하나라도 제조 번호에 해당하는 재료와 다른 재료가 있으면 실패
        }
        Debug.Log(isSuccess + "차 우리기 종료");
        OnEndBrewingTea?.Invoke(isSuccess); //차 우리기 종료 이벤트 발생
    }

    public void SetData(ItemSO item) //재료 리스트에 아이템 추가하는 함수
    {
        ingredientList.Add(item);
    }
    public void ResetData() //데이터 초기화
    {
        isFinished = false;
        isSuccess = false;
        ingredientList.Clear();
        ingredientQuantitySlider.value = 0;
        waterQuantitySlider.value = 0;
        waterQuantity = 0;
        GetComponent<Button>().interactable = false;
    }
    public void OnDrop(PointerEventData eventData) //드롭 이벤트 함수 (재료 넣기 함수)
    {
        if (ingredientList.Count == 0) ingredientQuantitySlider.gameObject.SetActive(true); //재료 하나 들어오면 슬라이더 활성화
        if (isFinished) return; //미니게임 중엔 작동 안함
        Debug.Log("드롭");
        if (ingredientList.Count == 3) //재료 3개면 더 이상 추가 못함
        {
            Debug.Log("재료가 가득찼습니다");
            return;
        }
        OnDropItem?.Invoke(); //드롭 이벤트 발생
        ingredientQuantitySlider.value = 30 * ingredientList.Count; //슬라이더 값 변경
        IsInteractable(); //재료 한 개 이상부터 (미니게임 시작)버튼 활성화
    }
    public void PourWater() //물 붓기 함수
    {
        if(waterQuantity == 0) waterQuantitySlider.gameObject.SetActive(true); //물 한 번 부으면 슬라이더 활성화
        if (isFinished) return; //미니게임 중엔 작동 안함
        if(waterQuantity == 3) //물 3번 이상 추가 못함
        {
            Debug.Log("물이 가득찼습니다");
            return;
        }
        waterQuantity++; //물 양 +1
        waterQuantitySlider.value = 70 * waterQuantity; //슬라이더 값 변경
        IsInteractable(); //물 한 번 이상부터 (미니게임 시작)버튼 활성화
    }
    public void IsInteractable() //버튼 활성화 함수
    {
        if (ingredientList.Count == 0 && waterQuantity == 0) GetComponent<Button>().interactable = false; //재료 0, 물 0 버튼 비활성화
        else GetComponent<Button>().interactable = true; //이외 전부 활성화
    }
    public void StartMiniGame() //미니게임 시작 함수
    {
        waterQuantitySlider.gameObject.SetActive(false); //물 채움 슬라이더 비활성화
        ingredientQuantitySlider.gameObject.SetActive(false); //재료 채움 슬라이더 비활성화
        isFinished = true; //미니게임 시작 여부
        miniGame.StartMiniGame(); //미니게임 시작
    }
}
