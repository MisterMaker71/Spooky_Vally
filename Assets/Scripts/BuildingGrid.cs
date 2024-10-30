using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingGrid : MonoBehaviour
{
    public LayerMask Mask;
    public Material m;
    float f = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 10, Mask))
        {
            m.SetVector("_Position", hit.point);
            if (f > 0)
                f -= Time.deltaTime * 10;
            else f = 0;
        }
        else
        {
            if(f < 1)
                f += Time.deltaTime * 10;
            else f = 1;
        }
        m.SetFloat("_camera_dependent", f);
    }
}
