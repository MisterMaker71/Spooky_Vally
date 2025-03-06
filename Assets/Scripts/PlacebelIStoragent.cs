using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacebelIStoragent : Buildebel
{   
    [Space(20)]
    public List<PlItem> items = new List<PlItem>();
    bool open = false;
    void Start()
    {
        canBeRemoved = items.Count <= 0;
    }
    public void Open()
    {
        InventoryManager.MainInstance.InventoryIsVisibel = true;
        open = true;
    }
    public void Add(PlItem item)
    {
        items.Add(new PlItem(item.Name, item.count, item.index));
    }
    [System.Serializable]
    public class PlItem
    {
        public string Name = "item_name";
        public int count = 1;
        [Space(3)]
        public int index;

        public PlItem(string _Name, int _count, int _index)
        {
            Name = _Name;
            count = _count;
            index = _index;
        }
    }
}
