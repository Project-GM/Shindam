using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 제조 시스템 관리하는 스크립트
/// </summary>
public class CraftingSystem : MonoBehaviour
{
    [SerializeField]
    private UIBrewingTea brewingTeaUI; //차 우리기 시스템
    public GameObject craftingUIBase; //제조 시스템 UI
    public int craftID; //제조 번호
    public bool isCrafting; //제조 중인지 아닌지
    public bool isSuccess = false; //제조 성공 여부
    private void Awake()
    {
        brewingTeaUI.OnEndBrewingTea += FinishCrafting; //차 우리기 시스템 차 우리기 종료 이벤트에 제조 종료 함수 할당
    }
    public void StartCrafting(int craftID) //제조 시작 함수
    {
        PlayerAction.s_Instance.isInteracting = true; //플레이어 이동 불가
        isCrafting = true; //제조 시작
        craftingUIBase.SetActive(true); //제조 UI 활성화
        brewingTeaUI.InitializeBrewingTea(craftID); //차 우리기 시작
        if (!brewingTeaUI.gameObject.activeSelf) brewingTeaUI.gameObject.SetActive(true); //주전자 비활성화 시 활성화
    }
    public void FinishCrafting(bool isSuccess) //제조 종료 함수
    {
        PlayerAction.s_Instance.isInteracting = false; //플레이어 이동 가능
        isCrafting = false; //제조 종료
        this.isSuccess = isSuccess; //성공 여부
        Debug.Log(isSuccess + "제조 끝");
        craftingUIBase.SetActive(false); //제조 UI 비활성화
    }
}
