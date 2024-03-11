using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 아이템 구조
/// </summary>
[CreateAssetMenu]
public class Item : ScriptableObject
{
    public int itemCode;
    public string itemName;
    [TextArea] public string itemDescription;
    public Sprite itemImage;
    public bool itemAcquire;
}
