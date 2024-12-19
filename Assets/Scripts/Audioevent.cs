using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audioevent : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void playAudio(string audio)
    {
        if(Resources.Load("audio/" + audio) != null)
            Instantiate(Resources.Load<GameObject>("audio/" + audio), transform.position, Quaternion.identity);
    }
    public static void playAudio(string audio, Vector3 position)
    {
        if (Resources.Load("audio/" + audio) != null)
            Instantiate(Resources.Load<GameObject>("audio/" + audio), position, Quaternion.identity);
    }
}
