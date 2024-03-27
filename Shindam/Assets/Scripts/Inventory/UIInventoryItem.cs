using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.EventSystems;

/// <summary>
/// 아이템 슬롯 관리하는 스크립트
/// </summary>
public class UIInventoryItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IEndDragHandler, IDropHandler, IDragHandler
{
    [SerializeField]
    private Image itemImage; //아이템 이미지
    [SerializeField]
    private TMP_Text quantityText; //아이템 개수 텍스트

    public event Action<UIInventoryItem> OnItemDroppedOn, OnItemBeginDrag, OnItemEndDrag, OnItemPointerEnter, OnItemPointerExit; //이벤트 변수

    private bool empty = true; //빈 슬롯 bool 변수
    public void Awake()
    {
        ResetData(); //슬롯 초기화
    }
    public string GetQauntity() //아이템 개수 가져오는 함수
    {
        return quantityText.text;
    }
    public void ResetData() //슬롯 초기화 함수
    {
        this.itemImage.gameObject.SetActive(false);
        this.empty = true;
    }
    public void SetData(Sprite sprite, int quantity) //슬롯 설정 함수
    {
        this.itemImage.gameObject.SetActive(true);
        this.itemImage.sprite = sprite;
        this.quantityText.text = quantity+"";
        this.empty = false;
    }

    public void OnPointerEnter(PointerEventData eventData) //마우스 포인터 진입 함수
    {
        if (empty) return;
        OnItemPointerEnter?.Invoke(this);
    }

    public void OnPointerExit(PointerEventData eventData) //마우스 포인터 이탈 함수
    {
        OnItemPointerExit?.Invoke(this);
    }

    public void OnBeginDrag(PointerEventData eventData) //드래그 시작 함수
    {
        if (empty) return;
        OnItemBeginDrag?.Invoke(this);
    }

    public void OnEndDrag(PointerEventData eventData) //드래그 끝 함수
    {
        OnItemEndDrag?.Invoke(this);
    }

    public void OnDrop(PointerEventData eventData) //드롭 함수
    {
        OnItemDroppedOn?.Invoke(this);
    }

    public void OnDrag(PointerEventData eventData)
    {
        
    }
}
