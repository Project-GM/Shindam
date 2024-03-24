using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragSlot : MonoBehaviour
{
    static public DragSlot instance;
    public Slot dragSlot;
    public Image itemImage;

    void Start()
    {
        instance = this;
    }

    public void DragSetItem(Image _itemImage)
    {
        itemImage.sprite = _itemImage.sprite;
        SetColor(1);
    }
    public void SetColor(float alpha)
    {
        Color color = itemImage.color;
        color.a = alpha;
        itemImage.color = color;
    }
}
