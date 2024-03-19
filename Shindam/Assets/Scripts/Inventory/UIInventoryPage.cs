using System;
using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

public class UIInventoryPage : MonoBehaviour
{
    [SerializeField]
    private UIInventoryItem itemPrefab;
    [SerializeField]
    private RectTransform contentPanel;
    [SerializeField]
    private UIInventoryDescription itemDescription;
    [SerializeField]
    private MouseFollower mouseFollower;
    List<UIInventoryItem> listOfUIItems = new List<UIInventoryItem>();

    private int currentlyDraggedItemIndex = -1;

    public event Action<int> OnDescriptionRequested, OnStartDragging;
    public event Action<int, int> OnSwapItems;
    
    private void Awake()
    {
        Hide();
        mouseFollower.Toggle(false);
        itemDescription.ResetDescription();
        itemDescription.gameObject.SetActive(false);
    }

    public void InitalizeInventoryUI(int inventorysize)
    {
        for (int i = 0; i < inventorysize; i++)
        {
            UIInventoryItem uiItem = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity);
            uiItem.transform.SetParent(contentPanel);
            listOfUIItems.Add(uiItem);
            uiItem.OnItemPointerEnter += HandleShowItemInfo;
            uiItem.OnItemPointerExit += HandleHideItemInfo;
            uiItem.OnItemBeginDrag += HandleBeginDrag;
            uiItem.OnItemEndDrag += HandleEndDrag;
            uiItem.OnItemDroppedOn += HandleSwap;
        }
    }

    public void UpdateData(int itemIndex, Sprite itemImage, int itemQauntity)
    {
        if (listOfUIItems.Count > itemIndex)
        {
            listOfUIItems[itemIndex].SetData(itemImage, itemQauntity);
        }
    }

    private void HandleBeginDrag(UIInventoryItem item)
    {
        int index = listOfUIItems.IndexOf(item);
        if (index == -1) return;
        currentlyDraggedItemIndex = index;
        OnStartDragging?.Invoke(index);
    }
    public void CreatDraggedItem(Sprite sprite, int quantity)
    {
        mouseFollower.Toggle(true);
        mouseFollower.SetData(sprite, quantity);
    }
    private void HandleEndDrag(UIInventoryItem item)
    {
        ResetDraggedItem();
    }

    private void HandleSwap(UIInventoryItem item)
    {
        int index = listOfUIItems.IndexOf(item);
        if (currentlyDraggedItemIndex == -1) return;
        OnSwapItems?.Invoke(currentlyDraggedItemIndex, index);
    }

    private void ResetDraggedItem()
    {
        mouseFollower.Toggle(false);
        currentlyDraggedItemIndex = -1;
    }

    private void HandleShowItemInfo(UIInventoryItem item)
    {
        int index = listOfUIItems.IndexOf(item);
        if (index == -1) return;
        OnDescriptionRequested?.Invoke(index);
        itemDescription.gameObject.SetActive(true);
    }

    private void HandleHideItemInfo(UIInventoryItem item)
    {
        itemDescription.ResetDescription();
        itemDescription.gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);
        itemDescription.ResetDescription();
    }
    public void Hide()
    {
        gameObject.SetActive(false);
        ResetDraggedItem() ;
    }

    internal void UpdateDescription(Sprite itemImage, string name, string description)
    {
        itemDescription.SetDescription(itemImage, name, description);
    }

    internal void ResetAllItems()
    {
        foreach (var item in listOfUIItems)
        {
            item.ResetData();
        }
    }
}
