using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using System.IO;

public class SaveManager : MonoBehaviour
{
    public static bool loaded = false;
    public string saveName = "DEV";
    public Saver save;
    public UnityEvent OnLoad;
    public List<GameObject> enabeldObjects = new List<GameObject>();
    public GameObject LoadImage;
    [SerializeField] RenderTexture screenshot;
    [SerializeField] Camera screenshotCamera;

    void OnEnable()
    {
        //print(PlayerPrefs.GetString("saveName"));


        //if (!Directory.Exists(Application.dataPath + "/Saves"))
        //{
        //    Directory.CreateDirectory(Application.dataPath + "/Saves");
        //}

        if (FindFirstObjectByType<CropManager>() != null)
            FindFirstObjectByType<CropManager>().Init();

        saveName = PlayerPrefs.GetString("saveName", saveName);

        //print("Start Loading");
        StartCoroutine(st());
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.F1))
            Save();
        //if (Input.GetKeyDown(KeyCode.F2))
        //    Load();

        //if (Input.GetKeyDown(KeyCode.Delete))
        //    if (Directory.Exists(Application.dataPath + "/Saves"))
        //    {
        //        if (File.Exists(Application.dataPath + "/Saves/" + saveName + ".save"))
        //        {
        //            File.Delete(Application.dataPath + "/Saves/" + saveName + ".save");
        //        }
        //        save = new Saver(new Vector3(), new Vector2(), 0);
        //        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        //    }
    }

    public void Save()
    {
        //Gether Dater:

        save = new Saver(
            PlayerMovement.PlayerInstance.transform.position,
            PlayerMovement.PlayerInstance.GetCamRotation(),
            DayNightCical.timeOfDay,
            TimedEvents.timedEvent,
            PlayerMovement.PlayerInstance.health
            ); ;
        save.enabeldObjects.Clear();
        foreach (var item in enabeldObjects)
        {
            save.enabeldObjects.Add(item.activeSelf);
        }


        FarmManager fm = FindFirstObjectByType<FarmManager>();
        save.rainTime = fm.RainTime;
        save.rainLenth = fm.RainLenth;
        save.rainTestTime = fm.RainTestTime;
        save.raining = fm.Raining;


        save.placed.Clear();
        foreach (BuildingGrid grid in FindObjectsOfType<BuildingGrid>())
        {
            foreach (Buildebel obj in grid.objecsOnGrid)
            {
                save.placed.Add(new SavePlcebel(grid.name, obj.name, obj.transform.position + new Vector3(obj.placeOffset.x, 0, obj.placeOffset.y), obj.id));
            }
        }
        save.Items.Clear();
        foreach (Inventory inv in FindObjectsOfType<Inventory>())
        {
            foreach (InventorySlot slot in inv.slots)
            {
                if (slot.Item != null)
                {
                    SaveItem i = new SaveItem(inv.name, slot.index, slot.Item.Name, slot.Item.count);
                    save.Items.Add(i);
                }
            }
        }
        save.crops.Clear();
        for (int i = 0; i < CropManager.tiles.Count; i++)
        {
            if (CropManager.tiles[i].farmebel != null)
            {
                if (CropManager.tiles[i].farmebel.GetComponent<GrowCrop>() != null)
                    save.crops.Add(new SaveCrop(CropManager.tiles[i].farmebel.name, CropManager.tiles[i].farmebel.GetComponent<GrowCrop>().GrowTime, CropManager.tiles[i].farmebel.GetComponent<GrowCrop>().GrowState));
                else
                    save.crops.Add(new SaveCrop(CropManager.tiles[i].farmebel.name, 0, 0));
            }
            else
                save.crops.Add(new SaveCrop("", 0, 0));
        }
        //foreach (HarvestTile ht in CropManager.tiles)
        //{
        //    if (ht.farmebel != null)
        //    {
        //        if (ht.farmebel.GetComponent<GrowCrop>() != null)
        //            save.crops.Add(new SaveCrop(ht.farmebel.name, ht.farmebel.GetComponent<GrowCrop>().GrowTime, ht.farmebel.GetComponent<GrowCrop>().GrowState));
        //        else
        //            save.crops.Add(new SaveCrop(ht.farmebel.name, 0, 0));
        //    }
        //    else
        //        save.crops.Add(new SaveCrop("", 0, 0));
        //}
        save.Loaders.Clear();
        foreach (LoadAndDeload lad in FindObjectsOfType<LoadAndDeload>())
        {
            if (lad.loadedScenes.Count > 0)
                save.Loaders.Add(new SaveLevelLoader(lad.saveId, lad.loadedScenes));
            else
                save.Loaders.Add(new SaveLevelLoader(lad.saveId));
        }
        save.Invs.Clear();
        foreach (var invs in FindObjectsOfType<PlacebelIStoragent>())
        {
            SaveInventory si = new SaveInventory(invs.id);
            foreach (var siv in invs.items)
            {
                si.Items.Add(new SaveItem("", siv.index, siv.Name, siv.count));
            }
            save.Invs.Add(si);
        }
        save.placebelFarmland.Clear();
        foreach (var pfl in FindObjectsOfType<PlacebelFarmLand>())
        {
            SavePlacebelFarmland si = new SavePlacebelFarmland(pfl.id);
            foreach (var siv in pfl.harvestTiles)
            {
                if(siv.farmebel != null)
                    si.crops.Add(new SaveCrop(siv.farmebel.name, siv.farmebel.GetComponent<GrowCrop>().GrowTime, siv.farmebel.GetComponent<GrowCrop>().GrowState));
                else
                    si.crops.Add(null);
            }
            save.placebelFarmland.Add(si);
        }

        //Save:
        if (!Directory.Exists(Application.dataPath + "/Saves"))
        {
            Directory.CreateDirectory(Application.dataPath + "/Saves");
        }
        //create scene shoot
        //Application.dataPath + "/Saves/" + saveName + ".png"
        //StartCoroutine(TakeScreenShot());
        if (screenshot != null)
        {
            screenshotCamera.gameObject.SetActive(true);
            byte[] bites = BrightnessContrast(toTexture2D(screenshot)).EncodeToPNG();
            // byte[] bites = BrightnessContrast(toTexture2D(screenshot), 1.1f, 1.2f).EncodeToPNG();
            print(bites);
            File.WriteAllBytes(Application.dataPath + "/Saves/" + saveName + ".png", bites);
            screenshotCamera.gameObject.SetActive(false);
        }


        //write to file
        string j = JsonUtility.ToJson(save);
        File.WriteAllText(Application.dataPath + "/Saves/" + saveName + ".save", j);
    }


    public void Load()
    {
        loaded = false;
        Debug.Log("Loading: " + saveName + " ...");
        LoadImage.SetActive(true);
        OnLoad.Invoke();
        if (save == null)
        {
            save = new Saver(new Vector3(), new Vector2(), 0);
        }
        //Lode:
        if (Directory.Exists(Application.dataPath + "/Saves"))
        {
            if (File.Exists(Application.dataPath + "/Saves/" + saveName + ".save"))
            {
                string j = File.ReadAllText(Application.dataPath + "/Saves/" + saveName + ".save");
                save = JsonUtility.FromJson<Saver>(j);

                Debug.Log("Reading: " + new FileInfo(Application.dataPath + "/Saves/" + saveName + ".save").Length + " bites");

                //Aplly changes:


                if (enabeldObjects.Count != save.enabeldObjects.Count)
                {
                    save.enabeldObjects = new List<bool>(enabeldObjects.Count);
                }
                if (enabeldObjects.Count == save.enabeldObjects.Count)
                {
                    for (int i = 0; i < enabeldObjects.Count; i++)
                    {
                        enabeldObjects[i].SetActive(save.enabeldObjects[i]);
                    }
                }

                //if (File.Exists(Application.dataPath + "/Saves/" + saveName + ".save"))
                //{
                DayNightCical.timeOfDay = save.timeOfDay;

                if (PlayerMovement.PlayerInstance != null)
                {
                    PlayerMovement.PlayerInstance.Teleport(save.PlayerPos, save.PlayerRot);
                    PlayerMovement.PlayerInstance.health = save.PlayerHealth;
                }

                FarmManager fm = FindFirstObjectByType<FarmManager>();
                fm.RainTime = save.rainTime;
                fm.RainLenth = save.rainLenth;
                fm.RainTestTime = save.rainTestTime;
                fm.Raining = save.raining;

                foreach (BuildingGrid item in FindObjectsOfType<BuildingGrid>())//clear Objects
                {
                    foreach (Buildebel obj in item.objecsOnGrid)
                    {
                        Destroy(obj.gameObject);
                    }
                    item.objecsOnGrid.Clear();
                    item.ResetCoverd();
                }
                foreach (SavePlcebel plcebel in save.placed)//place Objects
                {
                    GetGidByBuildebelName(plcebel.gridName).Place(plcebel.buildebelName, plcebel.position, plcebel.id);
                }
                foreach (PlacebelIStoragent pinv in FindObjectsOfType<PlacebelIStoragent>())
                {
                    //print("Loading invetorys");
                    //pinv.items.Clear();
                    foreach (SaveInventory pi in save.Invs)
                    {
                        if (pi.id == pinv.id)
                        {
                            foreach (SaveItem pii in pi.Items)
                            {
                                pinv.Add(new PlacebelIStoragent.PlItem(pii.Name, pii.count, pii.index));
                            }
                        }
                    }
                }


                foreach (SaveLevelLoader loader in save.Loaders)
                {
                    LoadAndDeload l = GetLevelLoader(loader.id);
                    if (l != null)
                    {
                        l.LoadAll(loader.loadedScenes);
                    }
                }

                foreach (Inventory inv in FindObjectsOfType<Inventory>())
                {
                    foreach (InventorySlot slot in inv.slots)
                    {
                        //print(slot.index);
                        if (slot.Item != null)
                        {
                            //print("Remove " + slot.Item.Name);
                            Destroy(slot.Item.gameObject);
                        }
                        if (GetItemInInventory(inv.name, slot.index) != null)
                        {
                            SaveItem i = GetItemInInventory(inv.name, slot.index);

                            //print("added " + i.Name);


                            GameObject GI = Resources.Load<GameObject>("items/" + i.Name);
                            if (GI != null)
                            {
                                slot.Item = Instantiate(GI, slot.transform).GetComponent<Item>();
                                slot.Item.count = i.count;
                            }
                        }
                    }
                }

                foreach (var item in FindObjectsOfType<Farmebel>())
                {
                    Destroy(item.gameObject);
                }
                for (int i = 0; i < save.crops.Count; i++)
                {
                    if (i < CropManager.tiles.Count)
                    {
                        HarvestTile ht = CropManager.tiles[i];
                        if (ht != null)
                        {
                            if (FarmManager.instance.FindCrop(save.crops[i].Name) != null)
                            {
                                GameObject g = Instantiate(FarmManager.instance.FindCrop(save.crops[i].Name).gameObject, ht.transform);
                                g.name = FarmManager.instance.FindCrop(save.crops[i].Name).name;
                                g.GetComponent<GrowCrop>().GrowState = save.crops[i].state;
                                g.GetComponent<GrowCrop>().GrowTime = save.crops[i].growTime;
                                ht.farmebel = g.GetComponent<Farmebel>();
                            }
                        }
                    }
                }


                foreach (PlacebelFarmLand ps in FindObjectsOfType<PlacebelFarmLand>())
                {
                    foreach (SavePlacebelFarmland fl in save.placebelFarmland)
                    {
                        if (ps.id == fl.id)
                        {
                            for (int i = 0; i < fl.crops.Count; i++)
                            {
                                if (FarmManager.instance.FindCrop(fl.crops[i].Name) != null)
                                {
                                    GameObject ga = Instantiate(FarmManager.instance.FindCrop(fl.crops[i].Name).gameObject, ps.harvestTiles[i].transform);
                                    ga.name = FarmManager.instance.FindCrop(fl.crops[i].Name).name;
                                    ga.GetComponent<GrowCrop>().GrowState = fl.crops[i].state;
                                    ga.GetComponent<GrowCrop>().GrowTime = fl.crops[i].growTime;
                                    ps.harvestTiles[i].farmebel = ga.GetComponent<Farmebel>();
                                    //print(ps.harvestTiles[i].farmebel);
                                }
                                //else
                                //{
                                //    print(fl.crops[i].Name);
                                //}
                            }
                        }
                    }
                }

                //}
                //else
                //{
                //    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                //    Save();
                //}
                LoadImage.SetActive(false);
                Debug.Log("Loading finished");
            }
            else
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                Save();
                Debug.LogWarning("Missing File (trying to create new file)");
            }
        }
        else
        {
            Debug.LogError("Invalid Directory");
        }
        loaded = true;
    }

    public BuildingGrid GetGidByBuildebelName(string gridName)
    {
        foreach (BuildingGrid item in FindObjectsOfType<BuildingGrid>())
        {
            if (gridName == item.name)
            {
                return item;
            }
        }
        return null;
    }
    public LoadAndDeload GetLevelLoader(string id)
    {
        foreach (LoadAndDeload lad in FindObjectsOfType<LoadAndDeload>())
        {
            if (lad.saveId == id)
            {
                return lad;
            }
        }
        return null;
    }
    public Texture2D toTexture2D(RenderTexture rTex)
    {
        Texture2D tex = new Texture2D(rTex.width, rTex.height, TextureFormat.RGBAFloat, false);
        var old_rt = RenderTexture.active;
        RenderTexture.active = rTex;

        tex.ReadPixels(new Rect(0, 0, rTex.width, rTex.height), 0, 0);
        tex.Apply();
        RenderTexture.active = old_rt;
        return tex;
    }
    public SaveItem GetItemInInventory(string inventory, int index)
    {
        foreach (SaveItem item in save.Items)
        {
            if (item.inventoryName == inventory)
            {
                if (item.index == index)
                {
                    return item;
                }
            }
        }
        return null;
    }
    [System.Serializable]
    public class Saver
    {
        public List<bool> enabeldObjects = new List<bool>();
        public List<SaveCrop> crops = new List<SaveCrop>();
        public List<SaveLevelLoader> Loaders = new List<SaveLevelLoader>();
        public List<SaveItem> Items = new List<SaveItem>();
        public List<SavePlcebel> placed = new List<SavePlcebel>();
        public List<SaveInventory> Invs = new List<SaveInventory>();
        public List<SavePlacebelFarmland> placebelFarmland = new List<SavePlacebelFarmland>();

        public bool raining;

        public float rainTime;
        public float rainTestTime;
        public float rainLenth;

        public float timeOfDay;
        public TimedEvent timedEvent;
        public Vector3 PlayerPos;
        public Vector2 PlayerRot;
        public float PlayerHealth = 100;

        public Saver(Vector3 PPos, Vector2 PRot, float _timeOfDay)
        {
            timeOfDay = _timeOfDay;
            timedEvent = TimedEvent.NoEvent;
            PlayerPos = PPos;
            PlayerRot = PRot;
        }
        public Saver(Vector3 PPos, Vector2 PRot, float _timeOfDay, TimedEvent _timedEvent, float PHp)
        {
            timeOfDay = _timeOfDay;
            timedEvent = _timedEvent;
            PlayerPos = PPos;
            PlayerRot = PRot;
            PlayerHealth = PHp;
        }
    }

    [System.Serializable]
    public class SaveItem
    {
        public string inventoryName;
        public int index = 0;

        public string Name = "";
        public int count = 1;

        public SaveItem(string _inventoryName, int _index, string _name, int _count)
        {
            inventoryName = _inventoryName;
            index = _index;
            Name = _name;
            count = _count;
        }
    }
    [System.Serializable]
    public class SavePlcebel
    {
        public string id;
        public string gridName;
        public string buildebelName;
        public Vector3 position;

        public SavePlcebel(string _gridName, string _buildebelName, Vector3 _position, string _id)
        {
            id = _id;
            gridName = _gridName;
            buildebelName = _buildebelName;
            position = _position;
        }
    }
    [System.Serializable]
    public class SaveInventory
    {
        public string id;
        public List<SaveItem> Items = new List<SaveItem>();

        public SaveInventory(string _id)
        {
            id = _id;
        }
        public SaveInventory(string _id, List<SaveItem> _Items)
        {
            id = _id;
            Items = _Items;
        }
    }
    [System.Serializable]
    public class SavePlacebelFarmland
    {
        public string id;
        public List<SaveCrop> crops = new List<SaveCrop>();

        public SavePlacebelFarmland(string _id)
        {
            id = _id;
        }
    }
    [System.Serializable]
    public class SaveCrop
    {
        public string Name = "";
        public float growTime;
        public int state;
        public SaveCrop(string _name, float _growTime, int _state)
        {
            growTime = _growTime;
            state = _state;
            Name = _name;
        }
    }
    [System.Serializable]
    public class SaveLevelLoader
    {
        public string id;
        public List<string> loadedScenes = new List<string>();
        public SaveLevelLoader(string _id)
        {
            id = _id;
        }
        public SaveLevelLoader(string _id, List<string> _loadedScenes)
        {
            id = _id;
            loadedScenes = _loadedScenes;
        }
    }
    IEnumerator st()
    {
        Time.timeScale = 1;
        LoadImage.SetActive(true);
        //print("befor wait wait");
        yield return new WaitForSeconds(0.2f);
        //print("after wait");
        Load();
    }
    //IEnumerator TakeScreenShot()
    //{
    //    yield return new WaitForEndOfFrame();
    //    //screenshot = ScreenCapture.CaptureScreenshotAsTexture();
    //    //screenshot.Resize();
    //    if (screenshot != null)
    //    {
    //        screenshot.name = saveName;
    //        screenshot.Apply();
    //        screenshot = new Texture2D(1000, 1000, screenshot.format, false);
    //        //if (screenshot.Reinitialize(100, 100))
    //        //{
    //        //byte[] bites = screenshot.EncodeToPNG();
    //        //File.WriteAllBytes(Application.dataPath + "/Saves/" + saveName + ".png", bites);
    //        //}
    //    }
    //}


    public static float AdjustChannel(float colour,
           float brightness, float contrast, float gamma)
    {
        return Mathf.Pow(colour, gamma) * contrast + brightness;
    }

    public static Texture2D BrightnessContrast(Texture2D tex,
               float brightness = 1f, float contrast = 1f, float gamma = 1f)
    {
        float adjustedBrightness = brightness - 1.0f;

        Color[] pixels = tex.GetPixels();

        for (int i = 0; i < pixels.Length; i++)
        {
            var p = pixels[i];
            p.r = AdjustChannel(p.r, adjustedBrightness, contrast, gamma);
            p.g = AdjustChannel(p.g, adjustedBrightness, contrast, gamma);
            p.b = AdjustChannel(p.b, adjustedBrightness, contrast, gamma);
            pixels[i] = p;
        }

        tex.SetPixels(pixels);
        tex.Apply();

        return tex;
    }


    private void OnApplicationQuit()
    {
        PaueMenu pm = FindFirstObjectByType<PaueMenu>();
        if (pm != null)
        {
            pm.showMenu();
            if (!pm.canQuit)
                Application.CancelQuit();
        }
    }


}