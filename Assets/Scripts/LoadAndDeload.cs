using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadAndDeload : MonoBehaviour
{
    public string saveId = "Loader1";
    public List<string> loadedScenes = new List<string>();
    public void LoadAll(List<string> scenes)
    {
        foreach (string scene in loadedScenes)
        {
            SceneManager.UnloadSceneAsync(scene);
        }
        loadedScenes.Clear();
        foreach (string scene in scenes)
        {
            SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive);
            loadedScenes.Add(scene);
        }
    }
    public void Load(string levelName)
    {
        SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Additive);
        loadedScenes.Add(levelName);
    }
    public void Unload(string levelName)
    {
        SceneManager.UnloadSceneAsync(levelName);
        loadedScenes.Remove(levelName);
    }
}
