using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustumCursor : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Cursor.lockState == CursorLockMode.Locked)
            transform.position = new Vector2(Camera.main.pixelWidth / 2, Camera.main.pixelHeight / 2);
        else
            transform.position = Input.mousePosition;
    }
}
