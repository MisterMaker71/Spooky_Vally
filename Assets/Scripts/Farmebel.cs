using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farmebel : MonoBehaviour
{
    [Header("Don't forget to add a new Farmebel\n to the FarmManager rCrops List.")]
    public ItemPropertys[] itemNames = { new ItemPropertys(null, Vector2Int.one), new ItemPropertys(null, Vector2Int.one) };
    public ItemPropertys[] unGrowenItemNames = { new ItemPropertys(null, Vector2Int.one) };
    //[SerializeField] Vector2 dropRange = Vector2.one;
    public bool canColect;

    void Update()
    {
        canColect = GetComponent<GrowCrop>().IsFullyGrowen();
    }
    public Farmebel Destroy()
    {
        if(GetComponent<GrowCrop>() != null)
        {
            //print("test crop: " + canColect);

            if(canColect)
                foreach (ItemPropertys item in itemNames)
                {
                    InventoryManager.MainInstance.AddItem(item.item, Random.Range(item.countRange.x, item.countRange.y + 1));
                }
            else
                foreach (ItemPropertys item in unGrowenItemNames)
                {
                    InventoryManager.MainInstance.AddItem(item.item, Random.Range(item.countRange.x, item.countRange.y + 1));
                }
        }
        else
            foreach (ItemPropertys item in itemNames)
            {
                InventoryManager.MainInstance.AddItem(item.item, Random.Range(item.countRange.x, item.countRange.y + 1));
            }
        Destroy(gameObject);
        return this;
    }
    [System.Serializable]
    public class ItemPropertys
    {
        public Item item;
        public Vector2Int countRange;
        public ItemPropertys(Item _item, Vector2Int _countRange)
        {
            item = _item;
            countRange = _countRange;
        }
    }
}
