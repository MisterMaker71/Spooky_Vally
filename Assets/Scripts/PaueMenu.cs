using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PaueMenu : MonoBehaviour
{
    public GameObject Menu;
    void Start()
    {
        Menu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !InventoryManager.MainInstance.InventoryIsVisibel)
        {
            Menu.SetActive(!Menu.activeSelf);
            
            if(Menu.activeSelf)
                Time.timeScale = 0;
            else
                Time.timeScale = 1;
        }
    }
    public void UnStuck()
    {
        
        PlayerMovement.PlayerInstance.Teleport(new Vector3(7, 0, 19.25f));
    }
    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void Quit()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
