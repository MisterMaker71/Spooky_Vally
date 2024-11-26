using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotMashine : MonoBehaviour
{
    public bool inventoryIsVisibel = false;
    Vector3 GambelPoint = new Vector3(1000, 1000, 1000);
    public Inventory Inventory;
    public void spin(bool win)
    {
        if(Inventory.slots[0].Item != null)
        {
            if (win)
                Inventory.slots[0].Item.count *= 2;
            else
                Destroy(Inventory.slots[0].Item.gameObject);
        }
    }
    void Update()
    {
        if (Vector3.Distance(GambelPoint, FindObjectOfType<PlayerMovement>().transform.position) > 2 && GambelPoint != new Vector3(1000, 1000, 1000))
        {
            inventoryIsVisibel = false;
            InventoryManager.MainInstance.InventoryIsVisibel = false;
            GambelPoint = new Vector3(1000, 1000, 1000);
        }
        if ((Input.GetKeyDown(KeyCode.I) || Input.GetKeyDown(KeyCode.Tab) || Input.GetKeyDown(KeyCode.Escape)) && /*InventoryManager.MainInstance.InventoryIsVisibel &&*/ inventoryIsVisibel)
        {
            inventoryIsVisibel = false;
            GambelPoint = new Vector3(1000, 1000, 1000);
        }
        Inventory.gameObject.SetActive(inventoryIsVisibel);
    }
    public void Open(Vector3 Position)
    {
        GambelPoint = Position;
        if (!inventoryIsVisibel)
        {
            InventoryManager.MainInstance.SetOpenState(true);
            inventoryIsVisibel = true;
        }
    }
}
