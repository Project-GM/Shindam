using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// 인벤토리 슬롯 스크립트
/// 아이템 및 아이템 개수 관리
/// </summary>

public class Slot : MonoBehaviour
{
    [SerializeField] Image image; //아이템 이미지
    [SerializeField] TMP_Text itemCountText; //아이템 개수 텍스트
    private Item _item; //슬롯에 할당된 아이템
    public int itemCount = 0; //아이템 개수
    public Item item
    {
        get { return _item; } //아이템 반환
        set //슬롯 설정
        {
            _item = value;
            if (_item != null) //아이템이 할당되면
            {
                image.sprite = item.itemImage;
                image.color = new Color(1, 1, 1, 1);
                itemCountText.gameObject.SetActive(true);
                itemCountText.text = itemCount.ToString();
            }
            else //아이템이 할당 안되면
            {
                image.color = new Color(1, 1, 1, 0);
                itemCountText.gameObject.SetActive(false);
            }
        }
    }
}
