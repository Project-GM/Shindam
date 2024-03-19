using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 인벤토리를 관리하는 스크립트
/// </summary>
public class InventoryController : MonoBehaviour
{
    [SerializeField]
    private UIInventoryPage inventoryUI; //인벤토리 UI

    [SerializeField]
    private InventorySO inventoryData; //인벤토리 SO 데이터

    public List<InventoryItem> initialItems = new List<InventoryItem>(); //초기 아이템

    private void Start()
    {
        PrepareUI(); //UI 초기화
        PrepareInventoryData(); //인벤토리 데이터 초기화
    }

    private void PrepareInventoryData() //인벤토리 데이터 초기화 함수
    {
        inventoryData.Initialize(); //인벤토리 데이터 초기화
        inventoryData.OnInventoryUpdated += UpdateInventoryUI; //인벤토리 업데이트 이벤트에 인벤토리UI 업데이트 함수 할당
        foreach (InventoryItem item in initialItems) //초기 아이템 셋 SO 데이터에 저장
        {
            if (item.IsEmpty) continue;
            inventoryData.AddItem(item);
        }
    }

    private void UpdateInventoryUI(Dictionary<int, InventoryItem> inventoryState) //인벤토리 UI 변경사항 업데이트하는 함수
    {
        inventoryUI.ResetAllItems(); //인벤토리 UI 초기화
        foreach (var item in inventoryState) //인벤토리 SO 데이터 기준으로 인벤토리 UI에 등록
        {
            inventoryUI.UpdateData(item.Key, item.Value.item.ItemImage, item.Value.quantity);
        }
    }

    private void PrepareUI() //인벤토리 UI 초기화 함수
    {
        inventoryUI.InitalizeInventoryUI(inventoryData.Size); //인벤토리 UI 초기화
        this.inventoryUI.OnDescriptionRequested += HandleDescriptionRequest; //설명창 띄우는 이벤트에 설명창 띄우는 핸들러 함수 할당
        this.inventoryUI.OnSwapItems += HandleSwapItems; //아이템 스왑 이벤트에 아이템 스왑 핸들러 함수 할당
        this.inventoryUI.OnStartDragging += HandleDragging; //드래그 시작 이벤트에 드래그 핸들러 함수 할당
    }

    private void HandleDragging(int itemIndex) //드래그 핸들러 함수
    {
        InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);
        if (inventoryItem.IsEmpty) return;
        inventoryUI.CreatDraggedItem(inventoryItem.item.ItemImage, inventoryItem.quantity); //드래그 슬롯 생성
    }

    private void HandleSwapItems(int itemIndex1, int itemIndex2) //아이템 스왑 핸들러 함수
    {
        inventoryData.SwapItems(itemIndex1, itemIndex2);//아이템 스왑
    }

    private void HandleDescriptionRequest(int itemIndex) //설명창 띄우는 핸들러 함수
    {
        InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);
        if (inventoryItem.IsEmpty) return;
        ItemSO item = inventoryItem.item;
        inventoryUI.UpdateDescription(item.ItemImage, item.name, item.Description); //설명창에 띄울 아이템 등록
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.I)) //인벤토리 키고 끄는 부분
        {
            if(inventoryUI.isActiveAndEnabled == false)
            {
                inventoryUI.Show();
                foreach (var item in inventoryData.GetCurrentInventoryState())
                {
                    inventoryUI.UpdateData(item.Key,
                        item.Value.item.ItemImage,
                        item.Value.quantity);
                }
            }
            else
            {
                inventoryUI.Hide();
            }
        }
        if(Input.GetKeyDown(KeyCode.Escape) && inventoryUI.isActiveAndEnabled == true)
            inventoryUI.Hide();
    }
}
