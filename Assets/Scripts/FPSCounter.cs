using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FPSCounter : MonoBehaviour
{
    [HideInInspector]
    public float fps;
    [HideInInspector]
    float i = 1;
    [SerializeField]private float intervall = 0.1f;
    void Update()
    {
        i += Time.deltaTime;
        fps = 1 / Time.deltaTime;
        if (GetComponent<TMP_Text>() != null && i >= intervall)
        {
            i = 0;
            GetComponent<TMP_Text>().text = "FPS:" + Mathf.Round(fps);
        }
    }
}
