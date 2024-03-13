using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public GameObject inventory;
    [SerializeField]
    private Transform slotParent;
    [SerializeField]
    private Slot[] slots;
    [SerializeField]
    CanvasGroup inventoryFullFloating;
    
    /// <summary>
    /// 인벤토리 스크립트
    /// 아이템 습득 및 분배 기능
    /// </summary>

    private void Start()
    {
        slots = slotParent.GetComponentsInChildren<Slot>();
    }
    private void Update()
    {
        TryOpenInventory(); // I키를 통해 열거나 닫거나
        TryCloseInventory();// esc키를 통해 닫는다
    }
    public void TryOpenInventory()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if(!inventory.activeSelf) OpenInventory();
            else CloseInventory();
        }
    }
    public void TryCloseInventory()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (inventory.activeSelf) CloseInventory();
        }
    }
    public void AddItem(Item item, int count = 1) //아이템 습득 함수 (아이템, 개수) 형식
    {
        bool hasItem = false; //이미 있는 아이템인지 확인
        bool full = false;    //인벤토리가 가득찼는지 확인
        for (int i = 0; i < slots.Length; i++) //이미 있는 아이템인지 확인하는 반복문
        {
            if (item == slots[i].item) //아이템이 인벤토리에 존재하면
            {
                hasItem = true;
                slots[i].itemCount += count; //아이템 개수 더함
                if (slots[i].itemCount > 30) //아이템 개수가 30개를 넘으면
                {
                    hasItem = false; //이미 하나의 슬롯이 가득 찼으므로 인벤토리에 아이템이 존재하지 않는다 표시(= 존재하지만 더 넣을 공간이 없다)
                    count = slots[i].itemCount - 30; //초과된 개수만큼 다시 할당
                    slots[i].itemCount = 30; //아이템 개수 30개로 설정
                    slots[i].item = item; //슬롯 설정
                    continue; //중복되는 아이템이 더 있는지 확인
                }
                slots[i].item = item; //슬롯 설정
                break; //반복문 탈출
            }
        }
        if (!hasItem) //인벤토리에 같은 아이템이 존재하지 않으면
        {
            for (int i = 0; i < slots.Length; i++)
            {
                full = false;
                if (slots[i].item == null) //빈 슬롯이 있으면
                {
                    slots[i].itemCount += count; //아이템 개수 더함
                    if (slots[i].itemCount > 30) //아이템 개수가 30개를 넘으면
                    {
                        count = slots[i].itemCount - 30; //초과된 개수만큼 다시 할당
                        slots[i].itemCount = 30; //아이템 개수 30개로 설정
                        slots[i].item = item; //슬롯 설정
                        if(i == slots.Length - 1 && count > 0) //마지막 슬롯인데 아직 습득할 아이템이 존재하면
                        {
                            full = true; //가득찼다 표시
                            break; //반복문 탈출
                        }
                        continue; //빈 슬롯이 더 있는지 확인
                    }
                    slots[i].item = item; //슬롯 설정
                    break; //반복문 탈출
                }
                full = true; //아이템 중복, 빈 슬롯 전부 확인했는데 그 무엇도 안나왔을 경우 가득찼다 표시
            }
        }
        if (full) StartCoroutine(EnableInventoryFullFloating());
    }
    public void CloseInventory() //인벤토리 닫는 함수
    {
        inventory.SetActive(false);
    }
    public void OpenInventory() //인벤토리 여는 함수
    {
        inventory.SetActive(true);
    }
    IEnumerator EnableInventoryFullFloating()
    {
        inventoryFullFloating.gameObject.SetActive(true);
        while (inventoryFullFloating.alpha < 1)
        {
            inventoryFullFloating.alpha += Time.deltaTime * 1.5f;
            yield return null;
        }
        yield return new WaitForSeconds(1);
        while (inventoryFullFloating.alpha > 0)
        {
            inventoryFullFloating.alpha -= Time.deltaTime * 1.5f;
            yield return null;
        }
        inventoryFullFloating.gameObject.SetActive(false) ;
    }
}
