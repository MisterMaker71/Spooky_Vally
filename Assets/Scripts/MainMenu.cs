using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.IO;

public class MainMenu : MonoBehaviour
{
    public TMP_Dropdown dropdown;
    public Button lodeButton;
    public TMP_InputField lodeNameField;
    string saveName = "DEV";

    private void Start()
    {
        lodeNameField.text = PlayerPrefs.GetString("saveName", "");
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
#if UNITY_EDITOR
            if (File.Exists(Application.dataPath + "/Saves/" + PlayerPrefs.GetString("saveName", "") + ".save.meta"))
            {
                File.Delete(Application.dataPath + "/Saves/" + PlayerPrefs.GetString("saveName", "") + ".save.meta");
            }
            if (File.Exists(Application.dataPath + "/Saves/" + PlayerPrefs.GetString("saveName", "") + ".png.meta"))
            {
                File.Delete(Application.dataPath + "/Saves/" + PlayerPrefs.GetString("saveName", "") + ".png.meta");
            }
#endif
            if (FindFirstObjectByType<MapListDropdown>() != null)
                FindFirstObjectByType<MapListDropdown>().Refresh();
        }
    }
    private void Update()
    {
        if (lodeNameField.text == "")
            lodeButton.interactable = false;
        else
            lodeButton.interactable = true;

        Cursor.visible = false;
        if (Input.GetKeyDown(KeyCode.Escape))
            Quit();
        //print(PlayerPrefs.GetString("saveName"));
    }
    public void ChangeSaveName(string name)
    {
        saveName = name;
    }
    public void LoadSave()
    {
        print("Lode: " + PlayerPrefs.GetString("saveName", ""));
        if(PlayerPrefs.GetString("saveName", "") != "")
        {
            SceneManager.LoadScene("SampleScene");
        }
    }
    public void NewSave()
    {
        print("Lode: " + saveName);
        if(saveName != "")
        {
            PlayerPrefs.SetString("saveName", saveName);
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
