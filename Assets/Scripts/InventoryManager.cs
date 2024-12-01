using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public bool InventoryIsVisibel = false;
    public GameObject inventory;
    public static InventoryManager MainInstance;
    public static Item selectedItem;
    public Item dragging;
    public string draggingName;
    public bool isDragging;
    public InventorySlot lastSlot = null;
    public Inventory HB;
    public Inventory INV;
    public Transform HBSelect;
    public int selected;
    public ItemInfo itemInfo;
    // Start is called before the first frame update
    void Start()
    {
        if (MainInstance != null)
            throw new System.Exception("Only one Inventory Manager in one scene!");
        MainInstance = this;
        ChangeSlot();
    }

    // Update is called once per frame
    void Update()
    {
            if (HB.slots[selected].Item == null)
                selectedItem = null;
            else
                selectedItem = HB.slots[selected].Item;

        if (PlayerMovement.PlayerInstance.schlagType == 0 && Time.timeScale != 0)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                selected = 0;
                ChangeSlot();
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                selected = 1;
                ChangeSlot();
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                selected = 2;
                ChangeSlot();
            }
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                selected = 3;
                ChangeSlot();
            }
            if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                selected = 4;
                ChangeSlot();
            }
            if (Input.GetKeyDown(KeyCode.Alpha6))
            {
                selected = 5;
                ChangeSlot();
            }
            if (Input.GetKeyDown(KeyCode.Alpha7))
            {
                selected = 6;
                ChangeSlot();
            }
            if (Input.GetKeyDown(KeyCode.Alpha8))
            {
                selected = 7;
                ChangeSlot();
            }
            if (Input.GetKeyDown(KeyCode.Alpha9))
            {
                selected = 8;
                ChangeSlot();
            }

            if (Input.GetAxis("Mouse ScrollWheel") < 0f)
            {
                selected++;
                if (selected > HB.slots.Count - 1)
                    selected = 0;
                ChangeSlot();
            }
            else if (Input.GetAxis("Mouse ScrollWheel") > 0f)
            {
                selected--;
                if (selected < 0)
                    selected = HB.slots.Count - 1;
                ChangeSlot();
            }
        }

        if(HBSelect != null)
        {
            if(HB.slots[selected] != null)
                HBSelect.position = HB.slots[selected].transform.position;
        }

        if (dragging != null)
        {
            dragging.transform.position = Input.mousePosition;

            if (Input.GetMouseButtonUp(0) && !IsOverSlot())
            {
                Cancle();
            }
        }

        if(itemInfo != null)
        {
            if (SlotOver() != null)
            {
                if(SlotOver().Item != null)
                {
                    //print("test");
                    itemInfo.gameObject.SetActive(true);
                    itemInfo.Title = SlotOver().Item.displayName;
                    itemInfo.Description = SlotOver().Item.Description;
                }
                else
                    itemInfo.gameObject.SetActive(false);
                //itemInfo.transform.position = SlotOver().transform.position + new Vector3(0, itemInfo.GetComponent<RectTransform>().rect.height, 0);
            }
            else
                itemInfo.gameObject.SetActive(false);
        }
    }
    void LateUpdate()
    {
        if ((Input.GetKeyDown(KeyCode.I) || Input.GetKeyDown(KeyCode.Tab)) && Time.timeScale == 1)
            InventoryIsVisibel = !InventoryIsVisibel;
        if (Input.GetKeyDown(KeyCode.Escape))
            InventoryIsVisibel = false;
        inventory.SetActive(InventoryIsVisibel);
        if(!InventoryIsVisibel && isDragging)
        {
            Cancle();
        }
    }
    public void SetOpenState(bool state)
    {
        InventoryIsVisibel = state;
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
    InventorySlot SlotOver()
    {
        if (IsOverSlot())
        {
            foreach (InventorySlot slot in FindObjectsOfType<InventorySlot>())
            {
                if (slot.IsSelected())
                    return slot;
            }
        }
        return null;
    }

    public void StartDragging(Item i)
    {
        StartDragging(i, i.transform.parent.GetComponent<InventorySlot>());
    }
    public void StartDragging(Item i, InventorySlot last)
    {
        //print(i);
        if (i != null)
        {
            lastSlot = last;
            if (i.GetComponentInParent<Canvas>() != null)
                i.transform.SetParent(i.GetComponentInParent<Canvas>().transform);
            isDragging = true;
            dragging = i;
            draggingName = i.Name;
        }
        ChangeSlotHidden();
    }
    public Item StopDragging(InventorySlot target)
    {
        Item i = dragging;

        if(i != null)
        {
            if (target.Item == null)
            //if (target.Item == null || lastSlot.Item != null)
            {
                print("move");
                i.transform.SetParent(target.transform);
                i.transform.localPosition = Vector3.zero;
                target.Item = i;
            }
            else if (target.Item == null && lastSlot.Item == null)
            {
                print("switch");
                target.Item.transform.SetParent(lastSlot.transform);
                target.Item.transform.localPosition = Vector3.zero;
                lastSlot.Item = target.Item;


                i.transform.SetParent(target.transform);
                i.transform.localPosition = Vector3.zero;
                target.Item = i;
            }
            else
            {
                if(lastSlot.Item != null)
                {
                    print("cancel split");
                    lastSlot.Item.count += i.count;
                    Destroy(i.gameObject);
                }
                else
                {
                    print("cancel move");
                    i.transform.SetParent(lastSlot.transform);
                    i.transform.localPosition = Vector3.zero;
                    lastSlot.Item = i;
                }
            }
        }

        dragging = null;
        draggingName = "";

        isDragging = false;

        ChangeSlotHidden();
        return i;
    }
    public void Cancle()
    {
        if (dragging != null)
        {
            dragging.transform.SetParent(lastSlot.transform);
            lastSlot.Item = dragging;
            dragging.transform.localPosition = Vector3.zero;
            dragging = null;
        }

        isDragging = false;
    }
    public void ChangeSlotHidden()
    {
        if (GetComponentInChildren<ItemBehavier>() != null)
        {
            GetComponentInChildren<ItemBehavier>().HideItem();
        }
    }
    public void ChangeSlot()
    {
        if(GetComponentInChildren<ItemBehavier>() != null)
        {
            if (HB.slots[selected].Item != null)
                GetComponentInChildren<ItemBehavier>().ShowItem(HB.slots[selected].Item);
            else
                GetComponentInChildren<ItemBehavier>().HideItem();
        }
    }
    public void AddItem(string itemName, int count)
    {
        Item i = Resources.Load<Item>("items/" + itemName);
        if (i != null)
        {
            AddItem(i, count);
        }
        else
        {
            Debug.LogWarning("cant add missing Item");
        }
    }

    public void AddItem(Item item, int count)
    {
        if (Resources.Load<GameObject>("items/" + item.Name) != null)
        {
            if (HB.FirstFreeSlot() != -1)
            {
                int i = 0;
                //GameObject g = Instantiate(Resources.Load<GameObject>("items/" + item.Name), HB.slots[i = HB.FirstFreeSlot(item.Name)].transform);
                GameObject g = Instantiate(Resources.Load<GameObject>("items/" + item.Name), HB.slots[i = HB.FirstFreeSlot(item.Name)].transform);
                if(HB.slots[i].Item == null)
                {
                    if (INV.slots[INV.FirstFreeSlot(item.Name)].Item != null)
                    {
                        Destroy(g);
                        //g = Instantiate(Resources.Load<GameObject>("items/" + item.Name), INV.slots[i = INV.FirstFreeSlot(item.Name)].transform);
                        g = Instantiate(Resources.Load<GameObject>("items/" + item.Name), INV.slots[i = INV.FirstFreeSlot(item.Name)].transform);
                        if (INV.slots[i].Item == null)
                        {
                            INV.slots[i].Item = g.GetComponent<Item>();
                            INV.slots[i].Item.count = count;
                        }
                        else
                        {
                            //INV.slots[i].Item.count += g.GetComponent<Item>().count;
                            INV.slots[i].Item.count += count;
                            Destroy(g);
                        }
                        ChangeSlot();
                        return;
                    }

                    HB.slots[i].Item = g.GetComponent<Item>();
                    HB.slots[i].Item.count = count;
                    //print("set " + name + " at " + i);
                }
                else
                {
                    //HB.slots[i].Item.count += g.GetComponent<Item>().count;
                    HB.slots[i].Item.count += count;
                    Destroy(g);
                    //print("add " + name + " at " + i);
                }
            }
            else if (INV.FirstFreeSlot() != -1)
            {
                int i = -1;
                //GameObject g = Instantiate(Resources.Load<GameObject>("items/" + item.Name), INV.slots[i = INV.FirstFreeSlot(item.Name)].transform);
                GameObject g = Instantiate(Resources.Load<GameObject>("items/" + item.Name), INV.slots[i = INV.FirstFreeSlot(item.Name)].transform);
                if(INV.slots[i].Item == null)
                {
                    INV.slots[i].Item = g.GetComponent<Item>();
                    INV.slots[i].Item.count = count;
                }
                else
                {
                    //INV.slots[i].Item.count += g.GetComponent<Item>().count;
                    INV.slots[i].Item.count += count;
                    Destroy(g);
                }
                //GetInventory().slots[GetInventory().FirstFreeSlot()].Item = i;
            }
            else
            {

            }
        }
        ChangeSlot();
    }
    //public Inventory GetInventory()
    //{
    //    return GameObject.Find("Inventory").GetComponent<Inventory>();

    //}
    //public Inventory GetHotbar()
    //{
    //    return GameObject.Find("Hotbar").GetComponent<Inventory>();
    //}
}
