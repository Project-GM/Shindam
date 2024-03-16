using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]
public class CraftDBItem
{
    public int ID;
    public int type;
    public string name;
    public int ingredientID;
    public int ingredientPriority;
    public Sprite ingredientImage;
    public int ingredientQuantity;
    public int waterQuantity;
    public int rewardGold;
    public int rewardFame;
    public int failGold;
    public int failFame;
}
[CreateAssetMenu(fileName = "Craft DataBase",menuName ="Scriptable Object/CraftDB")]
public class CraftDB : ScriptableObject
{
    public List<CraftDBItem> items = new List<CraftDBItem>();
}
