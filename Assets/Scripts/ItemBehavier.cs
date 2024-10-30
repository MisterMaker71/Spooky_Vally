using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBehavier : MonoBehaviour
{
    public GameObject ItemModel;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void HideItem()
    {
        Destroy(ItemModel);
    }
    public void ShowItem(Item item)
    {
        Destroy(ItemModel);
        if(item != null)
        {
            GameObject g = Resources.Load<GameObject>("ItemModels/" + item.Name);
            if (g != null)
            {
                ItemModel = Instantiate(g, transform.position, Quaternion.identity, transform);
                if (ItemModel != null)
                {
                    //print("[" + item.Name + "]: Me is Visibel!");
                    
                    ItemModel.transform.localPosition = Vector3.zero;
                }
                else
                {
                    //print(":(");
                }
            }
        }
    }
}
