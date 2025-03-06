using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interactor : MonoBehaviour
{
    public Transform playerBoddy;
    public Transform outline;
    public Transform point;
    public LayerMask interactionLayer;
    public Image cursor;
    public static string selectedCrop = "Wheat";
    public Animator cursorAnimations;
    public static float interactionDistance = 10;
    public Shader placeShader;
    public GameObject buildPrev;
    public string lastItem;
    [SerializeField]Vector3 posOffset = new Vector3();
    public static Vector3 prepos = new Vector3();
    [SerializeField]Vector3 rotOffset = new Vector3();
    [SerializeField] Vector3 prevDir = new Vector3();
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //cursor.color = new Color(0.6886792f, 0.6886792f, 0.6886792f, 0.7882353f);//default
        if (!InventoryManager.MainInstance.InventoryIsVisibel)
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, interactionDistance, interactionLayer) && Time.timeScale == 1)
            {
                if(buildPrev != null)
                {
                    Vector2 v = new Vector2(Mathf.Round(transform.forward.x * 2), Mathf.Round(transform.forward.z * 2));

                    //buildPrev.transform.position = BuildManager.grids[BuildManager.selectedGrid].nearestTile(hit.point, 0.5f).position + (buildPrev.transform.right * posOffset.x + buildPrev.transform.up * posOffset.y + buildPrev.transform.forward * posOffset.z);
                    buildPrev.transform.position = BuildManager.grids[BuildManager.selectedGrid].nearestTile(hit.point + buildPrev.transform.right * posOffset.x + buildPrev.transform.forward * posOffset.z, 0.5f).position + buildPrev.transform.right * -posOffset.x + buildPrev.transform.forward * -posOffset.z;
                    if (v.x != 0)
                        prevDir = new Vector3(v.x, 0, 0);
                    else
                        prevDir = new Vector3(0, 0, v.y);


                    //buildPrev.transform.position = hit.point;
                    buildPrev.transform.forward = prevDir;
                    buildPrev.transform.Rotate(rotOffset);
                    //buildPrev.transform.position -= buildPrev.transform.right * posOffset.x + buildPrev.transform.forward * posOffset.z;
                    //Debug.DrawLine(buildPrev.transform.position, -new Vector3(transform.forward.x, 1, transform.forward.z).normalized, Color.blue);
                    prepos = buildPrev.transform.position;
                }

                //print(hit.transform);
                //print(hit.transform.gameObject);
                //sets color of cursor
                if (hit.transform.GetComponent<Interactebel>() != null && Vector3.Distance(hit.point, PlayerMovement.PlayerInstance.transform.position) <= interactionDistance / 2)
                {
                    //print("can inter act with interactebel");
                    cursorAnimations.SetInteger("cursor", 2);
                    //cursor.color = new Color(0.5f, 0.5f, 0.5f, 0.7882353f);//gray
                }
                else if (InventoryManager.selectedItem != null && hit.transform.GetComponent<BuildingGrid>() != null)
                {
                    if (InventoryManager.selectedItem.GetType() == typeof(BuildebelItem))
                        cursorAnimations.SetInteger("cursor", 1);
                    else
                        cursorAnimations.SetInteger("cursor", 0);
                }
                else if (hit.transform.GetComponent<HarvestTile>() != null)
                {
                    //print(InventoryManager.selectedItem.GetType());
                    //print("can inter act with farmland");
                    if (hit.transform.GetComponent<HarvestTile>().farmebel != null)
                    {
                        if (InventoryManager.selectedItem != null)
                        {
                            //print(InventoryManager.selectedItem.GetType());
                            if (InventoryManager.selectedItem.GetType() != typeof(Sword) &&
                                InventoryManager.selectedItem.GetType() != typeof(Tool) &&
                                InventoryManager.selectedItem.GetType() != typeof(Seed) &&
                                InventoryManager.selectedItem.GetType() != typeof(Crop) 
                                /*|| hit.transform.GetComponent<HarvestTile>().farmebel.timeSincplaced <= 0*/)
                            {
                                cursorAnimations.SetInteger("cursor", 3);
                            }
                            else
                            {
                                if (hit.transform.GetComponent<HarvestTile>().farmebel.timeSincplaced <= 0 /*&&
                                InventoryManager.selectedItem.GetType() != typeof(Seed)*/)
                                {
                                    cursorAnimations.SetInteger("cursor", 3);//0
                                }
                                else
                                {
                                    cursorAnimations.SetInteger("cursor", 0);
                                }
                                //if (InventoryManager.selectedItem.GetType() == typeof(Seed))
                                //{
                                //    cursorAnimations.SetInteger("cursor", 1);
                                //}
                            }
                        }
                        else
                        {
                            cursorAnimations.SetInteger("cursor", 3);
                        }
                    }
                    else if (InventoryManager.selectedItem != null)
                    {
                        if (InventoryManager.selectedItem.GetType() == typeof(Seed))
                            cursorAnimations.SetInteger("cursor", 1);
                        else
                            cursorAnimations.SetInteger("cursor", 0);
                    }
                    else
                    {
                        cursorAnimations.SetInteger("cursor", 0);
                    }
                    //cursor.color = new Color(0.3030562f, 0.4716981f, 0.2865788f, 0.7882353f);//grean
                }
                else
                {
                    cursorAnimations.SetInteger("cursor", 0);
                }


                if (point != null)
                    point.position = Vector3.MoveTowards(point.position, hit.point, Time.deltaTime * 10);
                if (Vector3.Distance(new Vector3(hit.point.x, 0, hit.point.z), playerBoddy.position) < 3.5f)
                {
                    //print(hit.transform.tag);
                    if (hit.transform.tag == "FarmLand")
                    {
                        outline.gameObject.SetActive(true);
                        outline.position = new Vector3(Mathf.Floor(hit.point.x) + 0.5f, 0, Mathf.Floor(hit.point.z) + 0.5f);
                    }
                    else
                        outline.gameObject.SetActive(false);
                    //Debug.DrawRay(new Vector3(Mathf.Floor(hit.point.x) + 0.5f, 0, Mathf.Floor(hit.point.z) + 0.5f), Vector3.up, Color.red, 1);

                    if ((Input.GetKeyDown(KeyCode.E) || Input.GetMouseButtonDown(0)) && !InventoryManager.MainInstance.InventoryIsVisibel)
                    {
                        if (hit.transform.GetComponent<HarvestTile>() != null)
                        {
                            Item holdingItem = null;
                            if (InventoryManager.MainInstance.HB.slots[InventoryManager.MainInstance.selected].Item != null)
                            {
                                holdingItem = InventoryManager.MainInstance.HB.slots[InventoryManager.MainInstance.selected].Item;//do not ask me why I only used this here
                                if (hit.transform.GetComponent<HarvestTile>().farmebel != null)
                                {
                                    //Define Harvist Item Blacklist
                                    bool holdingBlackList1 = holdingItem.GetType() != typeof(Tool) && holdingItem.GetType() != typeof(Wapon) && holdingItem.GetType() != typeof(Sword) && holdingItem.GetType() != typeof(Crop);
                                    bool holdingBlackList2 = holdingItem.GetType() != typeof(Seed);
                                    if (holdingBlackList1 || Input.GetKeyDown(KeyCode.E))
                                    {
                                        if (holdingBlackList2 || Input.GetKeyDown(KeyCode.E) || hit.transform.GetComponent<HarvestTile>().farmebel.timeSincplaced <= 0)
                                        {
                                            //if(hit.transform.GetComponent<HarvestTile>().farmebel.canColect)
                                            //{
                                            hit.transform.GetComponent<HarvestTile>().colectCrop();
                                            //}
                                        }
                                        else if (hit.transform.GetComponent<HarvestTile>().farmebel.canColect)
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
                        if (inter != null && Vector3.Distance(hit.point, PlayerMovement.PlayerInstance.transform.position) <= interactionDistance / 2)
                        {
                            inter.Interact();
                        }
                    }
                    if (Input.GetMouseButtonDown(0))
                    {
                        if (InventoryManager.MainInstance.HB.slots[InventoryManager.MainInstance.selected].Item != null)
                        {
                            //Wapon w = InventoryManager.MainInstance.HB.slots[InventoryManager.MainInstance.selected].Item.GetComponent<Wapon>() as Wapon;

                            if (hit.transform.GetComponent<HarvestTile>() != null)
                            {
                                if (InventoryManager.MainInstance.HB.slots[InventoryManager.MainInstance.selected].Item.GetType() == typeof(Seed))
                                {
                                    if (hit.transform.GetComponent<HarvestTile>().farmebel == null)
                                    {
                                        //GameObject g = Instantiate(Resources.Load<GameObject>("items/" + name), hit.transform);
                                        Seed s = (Seed)InventoryManager.MainInstance.HB.slots[InventoryManager.MainInstance.selected].Item;
                                        //print(s.crop.name);
                                        GameObject g = Instantiate(FarmManager.instance.FindCrop(s.crop.name).gameObject, hit.transform);
                                        g.name = FarmManager.instance.FindCrop(s.crop.name).gameObject.name;
                                        hit.transform.GetComponent<HarvestTile>().farmebel = g.GetComponent<Farmebel>();

                                        InventoryManager.MainInstance.HB.slots[InventoryManager.MainInstance.selected].Item.RemoveItem(1);

                                        //if (InventoryManager.MainInstance.HB.slots[InventoryManager.MainInstance.selected].Item.count <= 0)
                                        //{
                                        //    Destroy(InventoryManager.MainInstance.HB.slots[InventoryManager.MainInstance.selected].Item.gameObject);
                                        //}
                                    }
                                }
                            }

                            if (InventoryManager.MainInstance.HB.slots[InventoryManager.MainInstance.selected].Item.GetType() == typeof(BuildebelItem))//Build
                            {
                                if (BuildManager.isGridSelected)
                                {
                                    Buildebel bu = BuildManager.grids[BuildManager.selectedGrid].Place(((BuildebelItem)(InventoryManager.MainInstance.HB.slots[InventoryManager.MainInstance.selected].Item)).buildebel, hit.point, prevDir);
                                    if (bu != null)
                                    {
                                        InventoryManager.MainInstance.HB.slots[InventoryManager.MainInstance.selected].Item.RemoveItem(1);
                                    }
                                }
                            }
                        }
                    }
                    else if (Input.GetMouseButtonDown(1))
                    {
                        if (InventoryManager.MainInstance.HB.slots[InventoryManager.MainInstance.selected].Item != null)
                        {
                            if (InventoryManager.MainInstance.HB.slots[InventoryManager.MainInstance.selected].Item.GetType() == typeof(BuildebelItem))//Build
                            {
                                if (BuildManager.isGridSelected)
                                {
                                    Buildebel b = BuildManager.grids[BuildManager.selectedGrid].Place(((BuildebelItem)(InventoryManager.MainInstance.HB.slots[InventoryManager.MainInstance.selected].Item)).buildebel, Vector3.zero, hit.point);
                                    if(b != null)
                                        InventoryManager.MainInstance.HB.slots[InventoryManager.MainInstance.selected].Item.RemoveItem();
                                }
                            }
                            else if (InventoryManager.MainInstance.HB.slots[InventoryManager.MainInstance.selected].Item.GetType() == typeof(Seed) && hit.transform.GetComponent<HarvestTile>() != null)
                            {
                                if(hit.transform.GetComponent<HarvestTile>().farmebel != null)
                                {
                                    if (hit.transform.GetComponent<HarvestTile>().farmebel == null)
                                    {
                                        //GameObject g = Instantiate(Resources.Load<GameObject>("items/" + name), hit.transform);
                                        Seed s = (Seed)InventoryManager.MainInstance.HB.slots[InventoryManager.MainInstance.selected].Item;
                                        //print(s.crop.name);
                                        GameObject g = Instantiate(FarmManager.instance.FindCrop(s.crop.name).gameObject, hit.transform);
                                        g.name = FarmManager.instance.FindCrop(s.crop.name).gameObject.name;
                                        hit.transform.GetComponent<HarvestTile>().farmebel = g.GetComponent<Farmebel>();

                                        InventoryManager.MainInstance.HB.slots[InventoryManager.MainInstance.selected].Item.RemoveItem(1);
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    outline.gameObject.SetActive(false);
                    cursorAnimations.SetInteger("cursor", 0);
                }
            }
            else
            {
                cursorAnimations.SetInteger("cursor", 0);
                outline.gameObject.SetActive(false);
            }


            // Build Preview
            if(InventoryManager.MainInstance.HB.slots[InventoryManager.MainInstance.selected].Item != null)
            {
                if (InventoryManager.MainInstance.HB.slots[InventoryManager.MainInstance.selected].Item.GetType() == typeof(BuildebelItem))//Build
                {
                    //print(BuildManager.isGridSelected);
                    if (BuildManager.isGridSelected & (buildPrev == null || lastItem != InventoryManager.MainInstance.HB.slots[InventoryManager.MainInstance.selected].Item.Name))
                    {
                        if (buildPrev != null)
                        {
                            Destroy(buildPrev);
                        }
                        //print(((BuildebelItem)(InventoryManager.MainInstance.HB.slots[InventoryManager.MainInstance.selected].Item)).buildebel);
                        GameObject g = Resources.Load<GameObject>("Buildebels/" + ((BuildebelItem)(InventoryManager.MainInstance.HB.slots[InventoryManager.MainInstance.selected].Item)).buildebel);
                        if (g != null)
                        {
                            print(InventoryManager.MainInstance.HB.slots[InventoryManager.MainInstance.selected].Item.Name);
                            buildPrev = Instantiate(g, hit.point, Quaternion.identity);
                            if (buildPrev != null)
                            {
                                rotOffset = g.GetComponent<Buildebel>().placeRotationOffset;
                                posOffset = g.GetComponent<Buildebel>().placeOffset;
                                lastItem = InventoryManager.MainInstance.HB.slots[InventoryManager.MainInstance.selected].Item.Name;
                                foreach (MeshRenderer item in buildPrev.GetComponentsInChildren<MeshRenderer>())
                                {
                                    foreach (Material m in item.materials)
                                    {
                                        if (m != null)
                                        {
                                            m.shader = placeShader;
                                        }
                                    }
                                }
                                foreach (Collider c in buildPrev.GetComponentsInChildren<Collider>())
                                {
                                    c.enabled = false;
                                }
                            }
                        }
                    }
                }
            }
            else if (buildPrev != null)
            {
                lastItem = "";
                Destroy(buildPrev);
            }
            if (InventoryManager.MainInstance.HB.slots[InventoryManager.MainInstance.selected].Item == null && lastItem != "")
            {
                lastItem = "";
            }



            //Use Wapon
            if (InventoryManager.MainInstance.HB.slots[InventoryManager.MainInstance.selected].Item != null && (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1)) && Time.timeScale > 0)
            {
                Wapon w = InventoryManager.MainInstance.HB.slots[InventoryManager.MainInstance.selected].Item.GetComponent<Wapon>() as Wapon;
                //Tool w = InventoryManager.MainInstance.HB.slots[InventoryManager.MainInstance.selected].Item.GetComponent<Tool>();
                //Tool w = (Tool)(InventoryManager.MainInstance.HB.slots[InventoryManager.MainInstance.selected].Item);
                if (w != null)// Use Tool
                {
                    if(Input.GetMouseButtonDown(0))
                        w.Use();
                    if(Input.GetMouseButtonDown(1))
                        w.UseSec();
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
        else
        {
            cursorAnimations.SetInteger("cursor", 0);
            outline.gameObject.SetActive(false);
        }
    }
}
