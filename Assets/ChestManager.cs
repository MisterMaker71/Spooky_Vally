using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestManager : MonoBehaviour
{
    public bool inventoryIsVisibel = false;
    Vector3 ChestPoint = new Vector3(1000, 1000, 1000);
    public Inventory Inventory;
    public PlacebelIStoragent storage;
    void Update()
    {
        if (Vector3.Distance(ChestPoint, PlayerMovement.PlayerInstance.transform.position) > Interactor.interactionDistance / 2 && ChestPoint != new Vector3(1000, 1000, 1000))
        {
            inventoryIsVisibel = false;
            InventoryManager.MainInstance.InventoryIsVisibel = false;
            ChestPoint = new Vector3(1000, 1000, 1000);
            UpdateChest();
        }
        if ((Input.GetKeyDown(KeyCode.I) || Input.GetKeyDown(KeyCode.Tab) || Input.GetKeyDown(KeyCode.Escape)) && /*InventoryManager.MainInstance.InventoryIsVisibel &&*/ inventoryIsVisibel)
        {
            inventoryIsVisibel = false;
            ChestPoint = new Vector3(1000, 1000, 1000);
            UpdateChest();
        }
        Inventory.gameObject.SetActive(inventoryIsVisibel);
    }
    public void UpdateChest()
    {
        int i = 0;
        storage.items.Clear();
        foreach (var slo in Inventory.slots)
        {
            if(slo.Item != null)
            {
                i++;
                storage.items.Add(new PlacebelIStoragent.PlItem(slo.Item.Name, slo.Item.count, slo.index));
            }
        }
        storage.canBeRemoved = (i <= 0);
    }
    public void Open(Vector3 Position, PlacebelIStoragent stor)
    {
        //print("Open");

        foreach (var slo in Inventory.slots)
        {
            if(slo.Item != null)
                Destroy(slo.Item.gameObject);
        }
        storage = stor;
        for (int i = 0; i < Inventory.slots.Count; i++)
        {
            if(i < storage.items.Count)
            {
                GameObject g = Resources.Load<GameObject>("items/" + storage.items[i].Name);
                if (g != null && storage.items[i].index < Inventory.slots.Count)
                {
                    GameObject lg = Instantiate(g, Inventory.slots[storage.items[i].index].transform);
                    Item it = lg.GetComponent<Item>();
                    Inventory.slots[storage.items[i].index].Item = it;
                    Inventory.slots[storage.items[i].index].Item.count = storage.items[i].count;
                }
            }
        }
        ChestPoint = Position;
        if (!inventoryIsVisibel)
        {
            InventoryManager.MainInstance.SetOpenState(true);
            inventoryIsVisibel = true;
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(ChestPoint, 0.1f);
    }
}
