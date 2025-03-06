using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Item : MonoBehaviour
{
    TMP_Text countText = null;
    public string Name = "item_name";
    public string displayName = "Item Name";
    [Multiline(5)]
    public string Description = "This is a Item!";
    public int count = 1;
    public int maxCount = 64;
    [Space(3)]
    public bool CreateItemCount = true;
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }
    /// <summary>
    /// Creates Item Counter
    /// </summary>
    public void Init()
    {
        if (CreateItemCount)
        { 
            countText = GetComponentInChildren<TMP_Text>();
            if (countText == null)
            {
                GameObject g = Instantiate(Resources.Load<GameObject>("ItemCount"), transform);
                if (g != null)
                    countText = g.GetComponent<TMP_Text>();
            }
        }
    }
    /// <summary>
    /// removes 1 item and delets it when reaching 0;
    /// </summary>
    /// <returns>number of leftover items</returns>
    public int RemoveItem()
    {
        return RemoveItem(1);
    }
    /// <summary>
    /// removes X item and delets it when reaching 0;
    /// </summary>
    /// <returns>number of leftover items</returns>
    public int RemoveItem(int _count)
    {
        count -= _count;
        if (count <= 0)
        {
            GetComponentInParent<InventorySlot>().Item = null;
            Destroy(gameObject);
            return 0;
        }
        return count;
    }

    // Update is called once per frame
    void Update()
    {
        if (countText != null && CreateItemCount)
        {
            if (count > 1)
            {
                countText.text = count.ToString();
                countText.gameObject.SetActive(true);
            }
            else
            {
                countText.gameObject.SetActive(false);
            }
        }
    }
}
