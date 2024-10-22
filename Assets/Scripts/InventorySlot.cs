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


        if(IsSelected())
        {
            GetComponent<Image>().color = SelecktedCollor;
            if(Input.GetMouseButtonDown(0))
            {
                if (Item != null)
                {
                    InventoryManager.MainInstance.StartDragging(Item);
                    Item = null;
                }

            }
            if (Input.GetMouseButtonUp(0))
            {
                if(Item == null)
                    AddToSlot(InventoryManager.MainInstance.StopDragging(this));
                else if(Item.Name == InventoryManager.MainInstance.dragging.Name)
                {
                    Item.count += InventoryManager.MainInstance.dragging.count;
                    Destroy(InventoryManager.MainInstance.dragging.gameObject);
                }
                else
                    InventoryManager.MainInstance.Cancle();
            }
        }
        else
            GetComponent<Image>().color = c1;
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
