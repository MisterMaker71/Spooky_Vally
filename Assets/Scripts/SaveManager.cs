using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class SaveManager : MonoBehaviour
{
    public string saveName = "DEV";
    public Saver save;
    // Start is called before the first frame update
    void Start()
    {
        //if (!Directory.Exists(Application.dataPath + "/Saves"))
        //{
        //    Directory.CreateDirectory(Application.dataPath + "/Saves");
        //}

        saveName = PlayerPrefs.GetString("saveName", saveName);

        StartCoroutine(st());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
            Save();
        if (Input.GetKeyDown(KeyCode.F2))
            Load();

        if (Input.GetKeyDown(KeyCode.Delete))
            if (Directory.Exists(Application.dataPath + "/Saves"))
            {
                if (File.Exists(Application.dataPath + "/Saves/" + saveName + ".save"))
                {
                    File.Delete(Application.dataPath + "/Saves/" + saveName + ".save");
                }
                save = new Saver(new Vector3(), new Vector2(), 0);
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
    }

    public void Save()
    {
        //Gether Dater:

        save = new Saver(
            PlayerMovement.PlayerInstance.transform.position,
            PlayerMovement.PlayerInstance.GetCamRotation(),
            DayNightCical.timeOfDay
            );
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
        foreach (HarvestTile ht in FindObjectsOfType<HarvestTile>())
        {
            if(ht.farmebel != null)
            {
                if (ht.farmebel.GetComponent<GrowCrop>() != null)
                    save.crops.Add(new SaveCrop(ht.farmebel.name, ht.farmebel.GetComponent<GrowCrop>().GrowTime, ht.farmebel.GetComponent<GrowCrop>().GrowState));
                else
                    save.crops.Add(new SaveCrop(ht.farmebel.name, 0, 0));
            }
            else
                save.crops.Add(new SaveCrop("", 0, 0));
        }

        //Save:
        if (!Directory.Exists(Application.dataPath+"/Saves"))
        {
            Directory.CreateDirectory(Application.dataPath + "/Saves");
        }
        string j = JsonUtility.ToJson(save);
        File.WriteAllText(Application.dataPath + "/Saves/" + saveName + ".save", j);
    }
    public void Load()
    {
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
            }
        }
        //Aplay changes:

        if (File.Exists(Application.dataPath + "/Saves/" + saveName + ".save"))
        {
            DayNightCical.timeOfDay = save.timeOfDay;

            if (PlayerMovement.PlayerInstance != null)
                PlayerMovement.PlayerInstance.Teleport(save.PlayerPos, save.PlayerRot);

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
                HarvestTile ht = FindObjectsOfType<HarvestTile>()[i];
                if (ht != null)
                {
                    if(FarmManager.instance.FindCrop(save.crops[i].Name) != null)
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
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            Save();
        }
    }
    public SaveItem GetItemInInventory(string inventory, int index)
    {
        foreach (SaveItem item in save.Items)
        {
            if(item.inventoryName == inventory)
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
        public List<SaveCrop> crops = new List<SaveCrop>();
        public List<SaveItem> Items = new List<SaveItem>();
        public float timeOfDay;
        public Vector3 PlayerPos;
        public Vector2 PlayerRot;

        public Saver(Vector3 PPos, Vector2 PRot, float _timeOfDay)
        {
            timeOfDay = _timeOfDay;
            PlayerPos = PPos;
            PlayerRot = PRot;
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
    IEnumerator st()
    {
        yield return new WaitForSeconds(0.2f);
        Load();
    }
}