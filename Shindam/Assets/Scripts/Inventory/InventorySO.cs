using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// 인벤토리 SO 스크립트
/// </summary>
[CreateAssetMenu]
public class InventorySO : ScriptableObject
{
    [SerializeField]
    private List<InventoryItem> inventoryItems; //아이템 리스트

    [field: SerializeField]
    public int Size { get; private set; } = 10; //인벤토리 크기

    public event Action<Dictionary<int, InventoryItem>> OnInventoryUpdated; //인벤토리 업데이트 이벤트
    public void Initialize() //인벤토리 데이터 초기화
    {
        inventoryItems = new List<InventoryItem>();
        for (int i = 0; i < Size; i++)
        {
            inventoryItems.Add(InventoryItem.GetEmptyItem()); //빈 슬롯으로 채우기
        }
    }
    public int AddItem(ItemSO item, int quantity) //아이템 획득
    {
        quantity = AddStackableItem(item, quantity); //중첩 가능한 아이템 획득 후 가방이 가득차서 획득 못하고 남은 아이템 개수 반환
        InformAboutChange(); //인벤토리 변경사항이 생겼다고 보냄
        return quantity;
    }

    private bool isInventoryFull() => inventoryItems.Where(item => item.IsEmpty).Any() == false; //인벤토리 가득찼는지 확인하는 함수

    private int AddStackableItem(ItemSO item, int quantity) //중첩 가능한 아이템 획득 함수
    {
        for (int i = 0; i < inventoryItems.Count; i++) 
        {
            if (inventoryItems[i].IsEmpty) continue; //빈 슬롯 건너뛰기
            if (inventoryItems[i].item.ID == item.ID) //같은 아이템이 존재하면
            {
                if (inventoryItems[i].quantity == inventoryItems[i].item.MaxStackSize) continue; //이미 가득찬 슬롯이면 건너뛰기
                int amountPossibleToTake = inventoryItems[i].item.MaxStackSize - inventoryItems[i].quantity; //획득 가능한 개수 계산(중첩 최대 개수 - 현재 아이템 개수)
                if (quantity > amountPossibleToTake) //획득 가능한 개수보다 획득한 아이템이 많으면
                {
                    inventoryItems[i] = inventoryItems[i].ChangeQuantity(inventoryItems[i].item.MaxStackSize); //아이템 개수 최대 개수로 설정
                    quantity -= amountPossibleToTake; //획득하고 남은 아이템 개수
                }
                else //획득 가능한 개수보다 획득한 아이템이 적으면
                {
                    inventoryItems[i] = inventoryItems[i].ChangeQuantity(inventoryItems[i].quantity + quantity); //획득한 만큼 개수 추가
                    return 0;
                }
            }
        }
        while(quantity > 0 && isInventoryFull() == false) //획득 가능한 개수가 없어질 때까지 반복
        {
            int newQuantity = Mathf.Clamp(quantity, 0, item.MaxStackSize);
            quantity -= newQuantity;
            AddItemToFirstFreeSlot(item, newQuantity); //빈 슬롯에 아이템 
        }
        return quantity;
    }

    private int AddItemToFirstFreeSlot(ItemSO item, int quantity) //빈 슬롯에 아이템 추가하는 함수
    {
        InventoryItem newItem = new InventoryItem
        {
            item = item,
            quantity = quantity,
        };
        for (int i = 0; i< inventoryItems.Count; i++)
        {
            if (inventoryItems[i].IsEmpty) //빈 슬롯 탐색
            {
                inventoryItems[i] = newItem; //빈 슬롯에 아이템 할당
                return quantity;
            }
        }
        return 0;
    }

    public Dictionary<int, InventoryItem> GetCurrentInventoryState() //현재 인벤토리 상태 가져오는 함수
    {
        Dictionary<int, InventoryItem> returnValue = new Dictionary<int, InventoryItem>(); //아이템이 존재하는 슬롯의 인덱스와 해당하는 아이템을 저장하는 딕셔너리
        for(int i = 0;i < inventoryItems.Count;i++)
        {
            if (inventoryItems[i].IsEmpty) continue; //빈 슬롯은 건너뛰기
            returnValue[i] = inventoryItems[i]; //아이템이 존재하는 슬롯 등록
        }
        return returnValue; //딕셔너리 리턴
    }

    public InventoryItem GetItemAt(int itemindex) //특정 인덱스에 있는 아이템 가져오는 함수
    {
        return inventoryItems[itemindex];
    }

    public void AddItem(InventoryItem item) //아이템(구조체) 등록하는 함수
    {
        AddItem(item.item, item.quantity);
    }

    public void SwapItems(int itemIndex1, int itemIndex2) //아이템 리스트 내부에서 스왑하는 함수
    {
        Debug.Log(itemIndex1 + ", " + itemIndex2);
        InventoryItem item1 = inventoryItems[itemIndex1]; 
        inventoryItems[itemIndex1] = inventoryItems[itemIndex2];
        inventoryItems[itemIndex2] = item1;
        InformAboutChange(); //인벤토리 변경사항 알림
    }

    private void InformAboutChange() //인벤토리 변경사항 알림 함수
    {
        OnInventoryUpdated?.Invoke(GetCurrentInventoryState()); //현재 인벤토리 상태를 매개변수로 전달하여 인벤토리 업데이트
    }
}
[Serializable]
public struct InventoryItem //인벤토리 아이템 구조체
{
    public int quantity; //아이템 개수
    public ItemSO item; //아이템
    public bool IsEmpty => item == null; //빈 슬롯인지 아닌지
    public InventoryItem ChangeQuantity(int newQuantity) //아이템 개수 변경하는 함수
    {
        return new InventoryItem
        {
            item = this.item,
            quantity = newQuantity,
        };
    }
    public static InventoryItem GetEmptyItem() //빈 슬롯 가져오는 함수
        => new InventoryItem
        {
            item = null,
            quantity = 0,
        };
}
