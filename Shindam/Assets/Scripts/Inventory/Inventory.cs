using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    //public List<TestItem> items;
    [SerializeField]
    private Transform slotParent;
    [SerializeField]
    private Slot[] slots;

    private void Start()
    {
        slots = slotParent.GetComponentsInChildren<Slot>();
        //SetItems();
    }
    /*public void SetItems()
    {
        int i = 0;
        for (; i < items.Count && i < slots.Length; i++)
            slots[i].item = items[i];
        for (; i < slots.Length; i++)
            slots[i].item = null;
    }*/
    public void AddItem(TestItem item)
    {
        bool hasItem = false;
        for (int i = 0; i < slots.Length; i++)
        {
            if (item == slots[i].item)
            {
                hasItem = true;
                slots[i].item.itemCount++;
                break;
            }
        }
        if (!hasItem)
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i].item == null) 
                {
                    slots[i].item = item;
                    break;
                }
            }
        }
    }
    public void CloseInventory()
    {
        gameObject.SetActive(false);
    }
    public void OpenInventory()
    {
        gameObject.SetActive(true);
    }
}
