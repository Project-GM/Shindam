﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ItemSO : ScriptableObject
{
    public int ID => GetInstanceID();

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
