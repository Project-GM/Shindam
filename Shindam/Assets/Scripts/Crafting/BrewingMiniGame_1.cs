﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BrewingMiniGame_1 : MonoBehaviour
{
    public int craftID;
    public TeaPot teaPot;
    public Strainer strainer;
    public Button finishButton;
    public Inventory inventory;
    [SerializeField]
    private Transform slotParent;
    [SerializeField]
    private Slot[] slots;
    public CraftDB craftDB;

    private void Start()
    {
        slots = slotParent.GetComponentsInChildren<Slot>();
        finishButton.interactable = false;
        for (int i = 0; i < inventory.slots.Length; i++)
        {
            AddItem(inventory.slots[i].item, inventory.slots[i].itemCount);
        }
    }
    public void AddItem(Item item, int count = 1) //아이템 습득 함수 (아이템, 개수) 형식
    {
        bool hasItem = false; //이미 있는 아이템인지 확인
        for (int i = 0; i < slots.Length; i++) //이미 있는 아이템인지 확인하는 반복문
        {
            if (item == slots[i].item) //아이템이 인벤토리에 존재하면
            {
                hasItem = true;
                slots[i].itemCount += count;
                slots[i].item = item;
                break;
            }
        }
        if (!hasItem) //인벤토리에 같은 아이템이 존재하지 않으면
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i].item == null) //빈 슬롯이 있으면
                {
                    slots[i].itemCount += count;
                    slots[i].item = item;
                    break; //반복문 탈출
                }
            }
        }
    }
    private void Update()
    {
        if (teaPot.waterCount > 0 || strainer.ingredientCount > 0) finishButton.interactable = true;
    }
    public void IsSuccess()
    {
        CraftDBItem item = new CraftDBItem();
        for(int i =0; i<craftDB.items.Count; i++)
        {
            if (craftDB.items[i].ID == craftID) item = craftDB.items[i];
        }
        transform.parent.GetComponent<BrewingTea>().successOrFailure.Add(item.ingredientQuantity == strainer.ingredientCount && item.waterQuantity == teaPot.waterCount);
        transform.parent.GetComponent<BrewingTea>().isMiniGame1Finished = true;
        gameObject.SetActive(false);
    }
}
