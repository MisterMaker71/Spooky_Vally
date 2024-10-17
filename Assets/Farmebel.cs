using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farmebel : MonoBehaviour
{
    public Farmebel Destroy()
    {
        InventoryManager.MainInstance.AddItem(new Crop());
        Destroy(gameObject);
        return this;
    }
}
