using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 아이템 SO 스크립트
/// </summary>
[CreateAssetMenu]
public class ItemSO : ScriptableObject
{
    [field: SerializeField]
    public int ID { get; set; }

    [field: SerializeField]
    public int MaxStackSize { get; set; } = 30;

    [field: SerializeField]
    public string Name {  get; set; }

    [field: SerializeField]
    [field: TextArea]
    public string Description { get; set; }

    [field: SerializeField]
    public Sprite ItemImage { get; set; }
}
