using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<InventorySlot> slots = new List<InventorySlot>();
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < slots.Count; i++)
        {
            slots[i].index = i;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int itemCount()
    {
        int count = 0;
        foreach (InventorySlot slot in slots)
        {
            if (slot.Item != null)
            {
                count++;
            }
        }
        return count;
    }
    public int FirstFreeSlot()
    {
        return FirstFreeSlot("");
    }
    public int FirstFreeSlot(string name)
    {
        for (int i = 0; i < slots.Count; i++)
        {
            if(slots[i].Item != null)
            {
                if (slots[i].Item.Name == name)
                {
                    return i;
                }
            }
        }
        for (int i = 0; i < slots.Count; i++)
        {
            if (slots[i].Item == null)
            {
                return i;
            }
        }
        return -1;
    }
}
