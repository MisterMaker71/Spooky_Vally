using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarvestTIle : MonoBehaviour
{
    public Crop crop;
    // Start is called before the first frame update
    void Start()
    {
        if (crop == null)
            crop = GetComponentInChildren<Crop>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
