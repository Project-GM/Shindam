using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using TMPro.EditorUtilities;
using UnityEngine;

/// <summary>
/// 인벤토리 UI를 관리하는 스크립트
/// </summary>
public class UIInventoryPage : MonoBehaviour
{
    [SerializeField]
    private UIInventoryItem itemPrefab; //아이템 슬롯 프리팹
    [SerializeField]
    private RectTransform contentPanel;
    [SerializeField]
    private UIInventoryDescription itemDescription; //아이템 정보 UI
    [SerializeField]
    private MouseFollower mouseFollower;
    [SerializeField]
    private InputNumber inputNumber;

    private Rect baseRect;

    public UIBrewingTea brewingTeaUI;

    List<UIInventoryItem> listOfUIItems = new List<UIInventoryItem>(); //아이템 슬롯 리스트

    private int currentlyDraggedItemIndex = -1; //드래그 중인 슬롯 인덱스


    public event Action<int> OnDescriptionRequested, OnStartDragging, OnUseItem; //아이템 설명창, 드래그 이벤트 변수
    
    public event Action<int, int> OnSwapItems, OnThrowItem; //드래그 드롭 시 아이템 스왑 이벤트 변수

    private void Awake()
    {
        Hide();
        mouseFollower.Toggle(false);
        itemDescription.ResetDescription();
        itemDescription.gameObject.SetActive(false);
        baseRect = GetComponent<RectTransform>().rect;
    }

    public void InitalizeInventoryUI(int inventorysize) //인벤토리 UI 초기화 함수
    {
        brewingTeaUI.OnDropItem += HandleUse; //제조 시스템 드롭 이벤트에 아이템 사용 핸들러 함수 할당
        inputNumber.OnEndInput += HandleThrow; //아이템 버리기 개수 입력 종료 이벤트에 아이템 버리기 핸들러 함수 할당
        for (int i = 0; i < inventorysize; i++)
        {
            UIInventoryItem uiItem = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity);
            uiItem.transform.SetParent(contentPanel);
            listOfUIItems.Add(uiItem);
            uiItem.OnItemPointerEnter += HandleShowItemInfo; //아이템 슬롯 포인터 진입 이벤트에 아이템 설명창 띄우는 함수 할당
            uiItem.OnItemPointerExit += HandleHideItemInfo; //아이템 슬롯 포인터 이탈 이벤트에 아이템 설명창 숨기는 함수 할당
            uiItem.OnItemBeginDrag += HandleBeginDrag; //아이템 슬롯 드래그 시작 이벤트에 드래그 시작 함수 할당
            uiItem.OnItemEndDrag += HandleEndDrag; //아이템 슬롯 드래그 끝 이벤트에 드래그 끝 함수 할당
            uiItem.OnItemDroppedOn += HandleSwap; //아이템 슬롯 드랍 이벤트에 스왑 핸들러 함수 할당
        }
    }

    private void HandleThrow(int itemIndex, int throwCount) //아이템 버리기 핸들러 함수
    {
        OnThrowItem?.Invoke(itemIndex, throwCount);
    }

    public void UpdateData(int itemIndex, Sprite itemImage, int itemQauntity) //아이템 슬롯에 등록하는 함수
    {
        if (listOfUIItems.Count > itemIndex)
        {
            listOfUIItems[itemIndex].SetData(itemImage, itemQauntity);
        }
    }

    private void HandleBeginDrag(UIInventoryItem item) //드래그 시작 이벤트 함수
    {
        int index = listOfUIItems.IndexOf(item);
        if (index == -1) return;
        currentlyDraggedItemIndex = index;
        OnStartDragging?.Invoke(index);
    }
    public void CreatDraggedItem(Sprite sprite, int quantity) //드래그 슬롯 활성화 함수
    {
        mouseFollower.Toggle(true);
        mouseFollower.SetData(sprite, quantity);
    }
    private void HandleEndDrag(UIInventoryItem item) //드래그 끝 이벤트 함수
    {
        if (mouseFollower.transform.position.x < baseRect.xMin
            || mouseFollower.transform.position.x > baseRect.xMax
            || mouseFollower.transform.position.y < baseRect.yMin
            || mouseFollower.transform.position.y > baseRect.yMax)
        {
            if (currentlyDraggedItemIndex == -1) return;
            inputNumber.InitializeInputField(currentlyDraggedItemIndex, listOfUIItems[currentlyDraggedItemIndex].GetQauntity());
        }
        ResetDraggedItem();
    }

    private void HandleSwap(UIInventoryItem item) //아이템 스왑 핸들러 함수
    {
        int index = listOfUIItems.IndexOf(item);
        if (currentlyDraggedItemIndex == -1) return;
        OnSwapItems?.Invoke(currentlyDraggedItemIndex, index);
    }

    private void HandleUse() //아이템 사용 핸들러 함수
    {
        if (currentlyDraggedItemIndex == -1) return;
        OnUseItem?.Invoke(currentlyDraggedItemIndex);
    }
    private void ResetDraggedItem() //드래그 슬롯 비활성화 함수
    {
        mouseFollower.Toggle(false);
        currentlyDraggedItemIndex = -1;
    }

    private void HandleShowItemInfo(UIInventoryItem item) //아이템 설명창 띄우는 함수
    {
        int index = listOfUIItems.IndexOf(item);
        if (index == -1) return;
        OnDescriptionRequested?.Invoke(index);
        itemDescription.gameObject.SetActive(true);
    }

    private void HandleHideItemInfo(UIInventoryItem item) //아이템 설명창 숨기는 함수
    {
        itemDescription.ResetDescription();
        itemDescription.gameObject.SetActive(false);
    }

    public void Show() //인벤토리 UI 띄우는 함수
    {
        gameObject.SetActive(true);
        itemDescription.ResetDescription();
    }
    public void Hide() //인벤토리 UI 숨기는 함수
    {
        gameObject.SetActive(false);
        ResetDraggedItem() ;
    }

    internal void UpdateDescription(Sprite itemImage, string name, string description) //설명창에 아이템 등록하는 함수
    {
        itemDescription.SetDescription(itemImage, name, description);
    }

    internal void ResetAllItems() //아이템 초기화 함수
    {
        foreach (var item in listOfUIItems)
        {
            item.ResetData();
        }
    }
}
