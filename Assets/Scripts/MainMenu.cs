using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.IO;

public class MainMenu : MonoBehaviour
{
    public TMP_Dropdown dropdown;
    //public string saveName = "DEV";

    private void Start()
    {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Confined;
    }
    public void Delet()
    {
        print(PlayerPrefs.GetString("saveName", ""));
        if (Directory.Exists(Application.dataPath + "/Saves"))
        {
            if (File.Exists(Application.dataPath + "/Saves/" + PlayerPrefs.GetString("saveName", "") + ".save"))
            {
                File.Delete(Application.dataPath + "/Saves/" + PlayerPrefs.GetString("saveName", "") + ".save");
            }
            if (File.Exists(Application.dataPath + "/Saves/" + PlayerPrefs.GetString("saveName", "") + ".png"))
            {
                File.Delete(Application.dataPath + "/Saves/" + PlayerPrefs.GetString("saveName", "") + ".png");
            }
            if (FindFirstObjectByType<MapListDropdown>() != null)
                FindFirstObjectByType<MapListDropdown>().Refresh();
        }
    }
    private void Update()
    {
        Cursor.visible = false;
        if (Input.GetKeyDown(KeyCode.Escape))
            Quit();
        //print(PlayerPrefs.GetString("saveName"));
    }
    public void ChangeSaveName(string name)
    {
        PlayerPrefs.SetString("saveName", name);
    }
    public void LoadSave()
    {
        if(PlayerPrefs.GetString("saveName", "") != "")
        {
            SceneManager.LoadScene("SampleScene");
        }
    }
    public void Quit()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
