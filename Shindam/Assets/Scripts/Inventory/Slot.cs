using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// 인벤토리 슬롯 스크립트
/// 아이템 및 아이템 개수 관리
/// </summary>

public class Slot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Item _item; //슬롯에 할당된 아이템
    public int itemCount = 0; //아이템 개수
    public Image itemImage; //아이템 이미지
    [SerializeField] Text itemCountText; //아이템 개수 텍스트
    public Item item
    {
        get { return _item; } //아이템 반환
        set //슬롯 설정
        {
            _item = value;
            if (_item != null) //아이템이 할당되면
            {
                itemImage.sprite = item.itemImage;
                itemImage.color = new Color(1, 1, 1, 1);
                itemCountText.gameObject.SetActive(true);
                itemCountText.text = itemCount.ToString();
            }
            else //아이템이 할당 안되면
            {
                itemImage.color = new Color(1, 1, 1, 0);
                itemCountText.gameObject.SetActive(false);
            }
        }
    }

    public void SetItem(Item _item, int _itemCount)
    {
        item = _item;
        itemImage.sprite = _item.itemImage;
        itemCount += _itemCount;
        itemCountText.gameObject.SetActive(true);
        itemCountText.text = itemCount.ToString();
    }

    public void ClearSlot()
    {
        item = null;
        itemCount = 0;
        itemImage.sprite = null;
        itemCountText.text = itemCount.ToString();
        itemCountText.gameObject.SetActive(false);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (item != null)
        {
            DragSlot.instance.GetComponent<Image>().raycastTarget = false;
            DragSlot.instance.dragSlot = this;
            DragSlot.instance.DragSetItem(itemImage);
            DragSlot.instance.transform.position = eventData.position;
        }
    }
    public void OnDrag(PointerEventData eventData)
    {
        if (item != null)
            DragSlot.instance.transform.position = eventData.position;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        DragSlot.instance.transform.position = new Vector2(2000, 1100);
        DragSlot.instance.SetColor(0);
        DragSlot.instance.dragSlot = null;
        DragSlot.instance.GetComponent<Image>().raycastTarget = true;
    }
}
