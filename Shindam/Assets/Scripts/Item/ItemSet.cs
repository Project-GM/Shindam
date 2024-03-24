using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 아이템 집합
/// 모든 아이템을 여기서 관리할 예정
/// </summary>
[CreateAssetMenu]
public class ItemSet : ScriptableObject
{
    public List<Item> items = new List<Item>();
    public Item GetItem(int itemCode)
    {
        for(int i = 0; i < items.Count; i++)
        {
            if (items[i].itemCode == itemCode) { return items[i]; }
        }
        return null;
    }
}
