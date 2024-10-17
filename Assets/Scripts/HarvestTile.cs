using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarvestTile : MonoBehaviour
{
    public Crop crop;
    // Start is called before the first frame update
    void Start()
    {
        if (crop == null)
            crop = GetComponentInChildren<Crop>();
    }

    public Crop colectCrop()
    {
        if (crop != null)
            return crop.Destroy();
        else
            return null;
    }
}
