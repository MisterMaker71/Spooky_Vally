using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarvestTile : MonoBehaviour
{
    public bool isWhett = false;
    [HideInInspector]
    public float whettTime = 0;
    public Farmebel farmebel;
    // Start is called before the first frame update
    void Start()
    {
        if (farmebel == null)
            farmebel = GetComponentInChildren<Farmebel>();
    }
    private void Update()
    {
        if(FarmManager.instance.Raining != isWhett)
        {
            isWhett = true;
            whettTime = Random.Range(2, 7);
        }
        else
        {
            if(whettTime > 0)
                whettTime -= Time.deltaTime;
            else
                isWhett = false;
        }
    }

    public Farmebel colectCrop()
    {
        Partical.Create("dirt_pickup", transform.position);
        if (farmebel != null)
        {
            return farmebel.Destroy();
        }
        else
            return null;
    }
}
