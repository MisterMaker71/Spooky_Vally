using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farmebel : MonoBehaviour
{
    public ItemPropertys[] itemNames = { new ItemPropertys("new crop", Vector2Int.one), new ItemPropertys("new seed", Vector2Int.one) };
    public ItemPropertys[] unGrowenItemNames = { new ItemPropertys("new seed", Vector2Int.one) };
    //[SerializeField] Vector2 dropRange = Vector2.one;
    public bool canColect;

    public Farmebel Destroy()
    {
        if(GetComponent<GrowCrop>() != null)
        {
            canColect = GetComponent<GrowCrop>().IsFullyGrowen();
            //print("test crop: " + canColect);

            if(canColect)
                foreach (ItemPropertys item in itemNames)
                {
                    InventoryManager.MainInstance.AddItem(item.name, Random.Range(item.countRange.x, item.countRange.y + 1));
                }
            else
                foreach (ItemPropertys item in unGrowenItemNames)
                {
                    InventoryManager.MainInstance.AddItem(item.name, Random.Range(item.countRange.x, item.countRange.y + 1));
                }
        }
        else
            foreach (ItemPropertys item in itemNames)
            {
                InventoryManager.MainInstance.AddItem(item.name, Random.Range(item.countRange.x, item.countRange.y + 1));
            }
        Destroy(gameObject);
        return this;
    }
    [System.Serializable]
    public class ItemPropertys
    {
        public string name;
        public Vector2Int countRange;
        public ItemPropertys(string _name, Vector2Int _countRange)
        {
            name = _name;
            countRange = _countRange;
        }
    }
}
