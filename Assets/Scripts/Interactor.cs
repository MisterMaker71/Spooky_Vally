using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    public Transform playerBoddy;
    public Transform outline;
    public LayerMask interactionLayer;
    public static string selectedCrop = "Wheat";
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 10, interactionLayer))
        {
            if (Vector3.Distance(new Vector3(hit.point.x, 0, hit.point.z), playerBoddy.position) < 3.5f)
            {
                //print(hit.transform.tag);
                if(hit.transform.tag == "FarmLand")
                    outline.gameObject.SetActive(true);
                outline.position = new Vector3(Mathf.Floor(hit.point.x) + 0.5f, 0, Mathf.Floor(hit.point.z) + 0.5f);
                //Debug.DrawRay(new Vector3(Mathf.Floor(hit.point.x) + 0.5f, 0, Mathf.Floor(hit.point.z) + 0.5f), Vector3.up, Color.red, 1);

                if(Input.GetKeyDown(KeyCode.E) && !InventoryManager.MainInstance.InventoryIsVisibel)
                {
                    if(hit.transform.GetComponent<HarvestTile>() != null)
                    {
                        if (hit.transform.GetComponent<HarvestTile>().farmebel != null)
                        {
                            //if(hit.transform.GetComponent<HarvestTile>().farmebel.canColect)
                            //{
                                hit.transform.GetComponent<HarvestTile>().colectCrop();
                            //}
                        }
                    }
                    Interactebel inter = hit.transform.GetComponent<Interactebel>();
                    if (inter != null)
                    {
                        inter.Interact();
                    }
                }
                if (Input.GetMouseButtonDown(0))
                {
                    if (hit.transform.GetComponent<HarvestTile>() != null)
                    {
                        if(hit.transform.GetComponent<HarvestTile>().farmebel == null)
                        {
                            //GameObject g = Instantiate(Resources.Load<GameObject>("items/" + name), hit.transform);
                            if(InventoryManager.MainInstance.HB.slots[InventoryManager.MainInstance.selected].Item != null)
                            {
                                if(InventoryManager.MainInstance.HB.slots[InventoryManager.MainInstance.selected].Item.GetType() == typeof(Seed))
                                {
                                    Seed s = (Seed)InventoryManager.MainInstance.HB.slots[InventoryManager.MainInstance.selected].Item;
                                    print(s.crop.name);
                                    GameObject g = Instantiate(FarmManager.instance.FindCrop(s.crop.name).gameObject, hit.transform);
                                    g.name = FarmManager.instance.FindCrop(s.crop.name).gameObject.name;
                                    hit.transform.GetComponent<HarvestTile>().farmebel = g.GetComponent<Farmebel>();

                                    InventoryManager.MainInstance.HB.slots[InventoryManager.MainInstance.selected].Item.count -= 1;

                                    if (InventoryManager.MainInstance.HB.slots[InventoryManager.MainInstance.selected].Item.count <= 0)
                                    {
                                        Destroy(InventoryManager.MainInstance.HB.slots[InventoryManager.MainInstance.selected].Item.gameObject);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else
                outline.gameObject.SetActive(false);
        }
        else
            outline.gameObject.SetActive(false);
    }
}
