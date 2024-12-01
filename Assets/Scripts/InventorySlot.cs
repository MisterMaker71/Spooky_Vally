using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlot : MonoBehaviour
{
    [HideInInspector]
    public int index = 0;
    public Item Item;
    //[SerializeField] TMP_Text ItemCount;
    Color c1;
    [SerializeField]Color SelecktedCollor = Color.gray;
    // Start is called before the first frame update
    void Start()
    {
        if (Item == null)
            GetComponentInChildren<Item>();
        c1 = GetComponent<Image>().color;
        if (Item == null)
            Item = GetComponentInChildren<Item>();
        if (Item != null)
            Item.transform.localPosition = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        //if(ItemCount != null)
        //{
        //    if(Item != null)
        //    {
        //        ItemCount.text = Item.count.ToString();
        //        ItemCount.gameObject.SetActive(true);
        //    }
        //    else
        //    {
        //        ItemCount.gameObject.SetActive(false);
        //    }
        //}

        if (IsSelected() && InventoryManager.MainInstance.InventoryIsVisibel)
        {
            GetComponent<Image>().color = SelecktedCollor;
            if(Input.GetMouseButtonDown(0) && !Input.GetMouseButton(1))
            {
                if (Item != null)
                {
                    InventoryManager.MainInstance.StartDragging(Item);
                    Item = null;
                }

            }
            else if(Input.GetMouseButtonDown(1) && !Input.GetMouseButton(0))
            {
                if (Item != null)
                {
                    if (Item.count > 1)
                    {
                        Item.count -= 1;
                        GameObject ItemClone = Instantiate(Item.gameObject, Item.transform);
                        ItemClone.GetComponent<Item>().count = 1;
                        InventoryManager.MainInstance.StartDragging(ItemClone.GetComponent<Item>(), Item.GetComponentInParent<InventorySlot>());
                    }
                    else
                    {
                        InventoryManager.MainInstance.StartDragging(Item);
                        Item = null;
                    }
                }

            }
            if (Input.GetMouseButtonUp(0) && !Input.GetMouseButton(1))
            {
                if(Item == null)
                    AddToSlot(InventoryManager.MainInstance.StopDragging(this));
                else if(Item.Name == InventoryManager.MainInstance.draggingName && InventoryManager.MainInstance.lastSlot != this && InventoryManager.MainInstance.dragging != null)
                {
                    Item.count += InventoryManager.MainInstance.dragging.count;
                    Destroy(InventoryManager.MainInstance.dragging.gameObject);
                }
                else
                {
                    InventoryManager.MainInstance.StopDragging(this);
                    //InventoryManager.MainInstance.Cancle();
                }
            }
            if (Input.GetMouseButtonUp(1) && !Input.GetMouseButton(0))
            {
                if(Item == null)
                    //InventoryManager.MainInstance.Cancle();
                    AddToSlot(InventoryManager.MainInstance.StopDragging(this));
                else if(Item.Name == InventoryManager.MainInstance.draggingName && InventoryManager.MainInstance.lastSlot != this && InventoryManager.MainInstance.dragging != null)
                {
                    Item.count += InventoryManager.MainInstance.dragging.count;
                    Destroy(InventoryManager.MainInstance.dragging.gameObject);
                }
                else
                {
                    print("canc");
                    InventoryManager.MainInstance.StopDragging(this);
                    //InventoryManager.MainInstance.Cancle();
                }
            }
        }
        else
        {
            if(!InventoryManager.MainInstance.InventoryIsVisibel&& InventoryManager.MainInstance.isDragging)
            {
                print("cancle");
                InventoryManager.MainInstance.StopDragging(this);
            }
            GetComponent<Image>().color = c1;
        }
    }

    /// <returns>Returns if item could be added</returns>
    public bool AddToSlot(Item item)
    {
        if (Item == null)
        {
            Item = item;
            return true;
        }
        else
            return false;


    }

    public bool IsSelected()
    {
        return
          (Input.mousePosition.x < transform.position.x + 50f && Input.mousePosition.y < transform.position.y + 50f &&
           Input.mousePosition.x > transform.position.x - 50f && Input.mousePosition.y > transform.position.y - 50f);
    }
}
