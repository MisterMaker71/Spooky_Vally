using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;

public class MapListDropdown : MonoBehaviour
{
    [SerializeField] TMP_Dropdown dropdown;
    bool open = false;
    public string playerPrefs = "LoadLevel";
    void Start()
    {
        Refresh();
        SelectLastMap();
    }
    void Update()
    {
        if (dropdown.IsExpanded)
        {
            if (!open)
            {
                open = true;
                Refresh();
            }
        }
        else if (open)
        {
            open = false;
        }
    }
    public void SelectLastMap()
    {
        int i = GetOptionIndex(PlayerPrefs.GetString(playerPrefs, ""));
        if (i > 0)
            dropdown.value = i;
    }
    public int GetOptionIndex(string Name)
    {
        for (int i = 0; i < dropdown.options.Count; i++)
        {
            if (dropdown.options[i].text == Name)
            {
                return i;
            }
        }
        return -1;
    }
    public void ChangeEditorMap(int i)
    {
        print("select:" + i);
        if (dropdown.options.Count > 0)
        {
            if (i < dropdown.options.Count)
            {
                //print("selected: " + dropdown.options[i].text);
                PlayerPrefs.SetString(playerPrefs, dropdown.options[i].text);
            }
            else
            {
                //print("_selected: " + dropdown.options[dropdown.options.Count - 1].text);
                PlayerPrefs.SetString(playerPrefs, dropdown.options[dropdown.options.Count - 1].text);
            }
        }
    }
    public void Refresh()
    {
        string last = "";
        if (dropdown.value > 0)
            last = dropdown.options[dropdown.value].text;
        dropdown.ClearOptions();
        if (Directory.Exists(Application.dataPath + "/Saves"))
        {
            List<TMP_Dropdown.OptionData> options = new List<TMP_Dropdown.OptionData>();
            foreach (var item in Directory.GetFiles(Application.dataPath + "/Saves/", "*.save"))
            {
                //print(Path.GetFileNameWithoutExtension(item));
                options.Add(new TMP_Dropdown.OptionData(Path.GetFileNameWithoutExtension(item)));
            }
            
            dropdown.AddOptions(options);
        }
        int i = GetOptionIndex(last);
        if (i > 0)
            dropdown.value = i;
        else
        {
            dropdown.value = 0;
            ChangeEditorMap(0);
        }
    }
}
