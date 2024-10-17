using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public bool InventoryIsVisibel = false;
    [SerializeField] GameObject inventory;
    public static InventoryManager MainInstance;
    public Item dragging;
    public bool isDragging;
    InventorySlot lastSlot;
    // Start is called before the first frame update
    void Start()
    {
        if (MainInstance != null)
            throw new System.Exception("Only one Inventory Manager in one scene!");
        MainInstance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
            InventoryIsVisibel = !InventoryIsVisibel;
        inventory.SetActive(InventoryIsVisibel);
        if (dragging != null)
        {
            dragging.transform.position = Input.mousePosition;

            if (Input.GetMouseButtonUp(0) && !IsOverSlot())
            {
                Cancle();
            }
        }
    }

    bool IsOverSlot()
    {
        //bool b = false;
        foreach (InventorySlot slot in FindObjectsOfType<InventorySlot>())
        {
            if (slot.IsSelected())
                return true;
        }
        return false;
    }

    public void StartDragging(Item i)
    {
        print(i);
        if (i != null)
        {
            lastSlot = i.transform.parent.GetComponent<InventorySlot>();
            if (i.GetComponentInParent<Canvas>() != null)
                i.transform.parent = i.GetComponentInParent<Canvas>().transform;
            isDragging = true;
            dragging = i;
        }
    }
    public Item StopDragging(InventorySlot target)
    {
        Item i = dragging;

        if(i != null)
        {
            i.transform.parent = target.transform;
            i.transform.localPosition = Vector3.zero;
        }

        dragging = null;

        isDragging = false;
        return i;
    }
    public void Cancle()
    {
        if (dragging != null)
        {
            dragging.transform.parent = lastSlot.transform;
            lastSlot.Item = dragging;
            dragging.transform.localPosition = Vector3.zero;
            dragging = null;
        }

        isDragging = false;
    }
    public void AddItem(Item i)
    {
        //To do
    }
}
