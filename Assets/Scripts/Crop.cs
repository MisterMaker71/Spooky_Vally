using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crop : Item
{
    public string cropName = "crop";
    public Crop Destroy()
    {
        Destroy(gameObject);
        return this;
    }
}
