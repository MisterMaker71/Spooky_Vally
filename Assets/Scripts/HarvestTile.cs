using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarvestTile : MonoBehaviour
{
    public Farmebel farmebel;
    // Start is called before the first frame update
    void Start()
    {
        if (farmebel == null)
            farmebel = GetComponentInChildren<Farmebel>();
    }

    public Farmebel colectCrop()
    {
        if (farmebel != null)
        {
            return farmebel.Destroy();
        }
        else
            return null;
    }
}
