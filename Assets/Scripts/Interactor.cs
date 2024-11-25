using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    public Transform playerBoddy;
    public Transform outline;
    public Transform point;
    public LayerMask interactionLayer;
    public static string selectedCrop = "Wheat";
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 10, interactionLayer) && Time.timeScale == 1)
        {
            if (point != null)
                point.position = Vector3.MoveTowards(point.position, hit.point, Time.deltaTime * 10);
            if (Vector3.Distance(new Vector3(hit.point.x, 0, hit.point.z), playerBoddy.position) < 3.5f)
            {
                //print(hit.transform.tag);
                if (hit.transform.tag == "FarmLand")
                    outline.gameObject.SetActive(true);
                outline.position = new Vector3(Mathf.Floor(hit.point.x) + 0.5f, 0, Mathf.Floor(hit.point.z) + 0.5f);
                //Debug.DrawRay(new Vector3(Mathf.Floor(hit.point.x) + 0.5f, 0, Mathf.Floor(hit.point.z) + 0.5f), Vector3.up, Color.red, 1);

                if ((Input.GetKeyDown(KeyCode.E) || Input.GetMouseButtonDown(0)) && !InventoryManager.MainInstance.InventoryIsVisibel)
                {
                    if (hit.transform.GetComponent<HarvestTile>() != null)
                    {
                        Item holdingItem = null;
                        if (InventoryManager.MainInstance.HB.slots[InventoryManager.MainInstance.selected].Item != null)
                        {
                            holdingItem = InventoryManager.MainInstance.HB.slots[InventoryManager.MainInstance.selected].Item;//do not ask me why I only used this here
                            if(hit.transform.GetComponent<HarvestTile>().farmebel != null)
                            {
                                //Define Harvist Item Blacklist
                                bool holdingBlackList1 = holdingItem.GetType() != typeof(Tool) && holdingItem.GetType() != typeof(Wapon) && holdingItem.GetType() != typeof(Sword) && holdingItem.GetType() != typeof(Crop);
                                bool holdingBlackList2 = holdingItem.GetType() != typeof(Seed);
                                if (holdingBlackList1 || Input.GetKeyDown(KeyCode.E))
                                {
                                    if (holdingBlackList2 || Input.GetKeyDown(KeyCode.E))
                                    {
                                        //if(hit.transform.GetComponent<HarvestTile>().farmebel.canColect)
                                        //{
                                        hit.transform.GetComponent<HarvestTile>().colectCrop();
                                        //}
                                    }
                                    else if(hit.transform.GetComponent<HarvestTile>().farmebel.canColect)
                                    {
                                        hit.transform.GetComponent<HarvestTile>().colectCrop();
                                    }
                                }
                            }
                        }
                        else if (hit.transform.GetComponent<HarvestTile>().farmebel != null)
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
                    if (InventoryManager.MainInstance.HB.slots[InventoryManager.MainInstance.selected].Item != null)
                    {
                        //Wapon w = InventoryManager.MainInstance.HB.slots[InventoryManager.MainInstance.selected].Item.GetComponent<Wapon>() as Wapon;
                        if (InventoryManager.MainInstance.HB.slots[InventoryManager.MainInstance.selected].Item.GetType() == typeof(Seed))
                        {
                            if (hit.transform.GetComponent<HarvestTile>() != null)
                            {

                                if (hit.transform.GetComponent<HarvestTile>().farmebel == null)
                                {
                                    //GameObject g = Instantiate(Resources.Load<GameObject>("items/" + name), hit.transform);
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
                                else if (InventoryManager.MainInstance.HB.slots[InventoryManager.MainInstance.selected].Item.GetType() == typeof(Crop))
                                {
                                    if(((Crop)(InventoryManager.MainInstance.HB.slots[InventoryManager.MainInstance.selected].Item)).nutrien > 0)
                                    {
                                        //PlayerMovement.PlayerInstance.health += ((Crop)(InventoryManager.MainInstance.HB.slots[InventoryManager.MainInstance.selected].Item)).nutrien;
                                        InventoryManager.MainInstance.HB.slots[InventoryManager.MainInstance.selected].Item.count -= 1;

                                        //Heals player when eaten food
                                        PlayerMovement.PlayerInstance.Heal(((Crop)(InventoryManager.MainInstance.HB.slots[InventoryManager.MainInstance.selected].Item)).nutrien);

                                        if (InventoryManager.MainInstance.HB.slots[InventoryManager.MainInstance.selected].Item.count <= 0)
                                        {
                                            Destroy(InventoryManager.MainInstance.HB.slots[InventoryManager.MainInstance.selected].Item.gameObject);
                                        }
                                    }
                                }
                            }
                            else if (InventoryManager.MainInstance.HB.slots[InventoryManager.MainInstance.selected].Item.GetType() == typeof(Crop))//Eat Crop
                            {
                                if (((Crop)(InventoryManager.MainInstance.HB.slots[InventoryManager.MainInstance.selected].Item)).nutrien > 0)
                                {
                                    //PlayerMovement.PlayerInstance.health += ((Crop)(InventoryManager.MainInstance.HB.slots[InventoryManager.MainInstance.selected].Item)).nutrien;
                                    InventoryManager.MainInstance.HB.slots[InventoryManager.MainInstance.selected].Item.count -= 1;

                                    //Heals player when eaten food
                                    PlayerMovement.PlayerInstance.Heal(((Crop)(InventoryManager.MainInstance.HB.slots[InventoryManager.MainInstance.selected].Item)).nutrien);

                                    if (InventoryManager.MainInstance.HB.slots[InventoryManager.MainInstance.selected].Item.count <= 0)
                                    {
                                        Destroy(InventoryManager.MainInstance.HB.slots[InventoryManager.MainInstance.selected].Item.gameObject);
                                    }
                                }
                            }
                        }
                        else if (InventoryManager.MainInstance.HB.slots[InventoryManager.MainInstance.selected].Item.GetType() == typeof(BuildebelItem))//Build
                        {
                            if (BuildManager.isGridSelected)
                            {
                                BuildManager.grids[BuildManager.selectedGrid].Place(((BuildebelItem)(InventoryManager.MainInstance.HB.slots[InventoryManager.MainInstance.selected].Item)).buildebel, hit.point);
                            }
                        }
                    }
                }
            }
            else
                outline.gameObject.SetActive(false);
        }
        else
        {
            outline.gameObject.SetActive(false);
        }

        //Use Wapon
        if (InventoryManager.MainInstance.HB.slots[InventoryManager.MainInstance.selected].Item != null && Input.GetMouseButtonDown(0) && Time.timeScale > 0)
        {
            Wapon w = InventoryManager.MainInstance.HB.slots[InventoryManager.MainInstance.selected].Item.GetComponent<Wapon>() as Wapon;
            //Tool w = InventoryManager.MainInstance.HB.slots[InventoryManager.MainInstance.selected].Item.GetComponent<Tool>();
            //Tool w = (Tool)(InventoryManager.MainInstance.HB.slots[InventoryManager.MainInstance.selected].Item);
            if (w != null)// Use Tool
            {
                w.Use();
            }
        }


        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100, interactionLayer))
        {
            if (point != null)
                point.position = Vector3.MoveTowards(point.position, hit.point, Time.deltaTime * 10 * (Vector3.Distance(point.position, hit.point) / 2));
        }
        else
        {
            if (point != null)
                point.position = Vector3.MoveTowards(point.position, transform.position + transform.forward * 8, Time.deltaTime * 10 * (Vector3.Distance(point.position, hit.point) / 2));
        }
    }
}
