using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingManager : MonoBehaviour
{
    public bool CraftingIsVisibel = false;
    //public GameObject CraftingUI;
    public static CraftingManager MainInstance;
    public Inventory Crafting;
    Vector3 crafterPosition = new Vector3(1000, 1000, 1000);

    // Start is called before the first frame update
    void Start()
    {
        if (MainInstance != null)
            throw new System.Exception("Only one Crafting Manager in one scene!");
        MainInstance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(crafterPosition, FindObjectOfType<PlayerMovement>().transform.position) > 2 && crafterPosition != new Vector3(1000, 1000, 1000))
        {
            CraftingIsVisibel = false;
            InventoryManager.MainInstance.InventoryIsVisibel = false;
            crafterPosition = new Vector3(1000, 1000, 1000);
        }
        if ((Input.GetKeyDown(KeyCode.I) || Input.GetKeyDown(KeyCode.Tab) || Input.GetKeyDown(KeyCode.Escape)) && /*InventoryManager.MainInstance.InventoryIsVisibel &&*/ CraftingIsVisibel)
        {
            CraftingIsVisibel = false;
            crafterPosition = new Vector3(1000, 1000, 1000);
        }
        Crafting.gameObject.SetActive(CraftingIsVisibel);
    }
    public void Open(Vector3 Position)
    {
        crafterPosition = Position;
        if (!CraftingIsVisibel)
        {
            InventoryManager.MainInstance.SetOpenState(true);
            CraftingIsVisibel = true;
        }
    }
}
