using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Strainer : MonoBehaviour, IDropHandler
{
    public List<Image> itemImages = new List<Image>();
    [Range(0,3)]public int ingredientCount = 0;
    public void OnDrop(PointerEventData eventData)
    {
        if(ingredientCount == 3)
        {
            Debug.Log("재료가 가득 찼습니다");
            return;
        }
        if(DragSlot.instance.dragSlot != null)
        {
            DragSlot.instance.dragSlot.itemCount--;
            DragSlot.instance.dragSlot.item = DragSlot.instance.dragSlot.item;
            SetColor(1, itemImages[ingredientCount]);
            itemImages[ingredientCount].sprite = DragSlot.instance.itemImage.sprite;
            ingredientCount++;
        }
    }
    public void SetColor(float alpha, Image itemImage)
    {
        Color color = itemImage.color;
        color.a = alpha;
        itemImage.color = color;
    }
}
