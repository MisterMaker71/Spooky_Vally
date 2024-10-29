using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowCrop : MonoBehaviour
{
    public List<Material> states = new List<Material>();
    [Tooltip("time it takes the crop to grows in secents")]
    public float growSpeed = 4;
    [Tooltip("Random variations in grow per update"), Min(0)]
    public Vector2 growNoise = new Vector2(0, 1);

    public float GrowMultyplyerByRain = 2;

    //[HideInInspector]
    public float GrowTime;
    //[HideInInspector]
    public int GrowState;
    // Start is called before the first frame update
    void Start()
    {
        GrowTime = Random.Range(-growSpeed * 1.5f, 0f);
        GetComponent<MeshRenderer>().material = states[GrowState];
    }

    // Update is called once per frame
    void Update()
    {
        if (GrowState < states.Count-1 && DayNightCical.time == DayTime.Day)
        {
            if(GetComponentInParent<HarvestTile>() != null)
            {
                if (GetComponentInParent<HarvestTile>().isWhett && Random.Range(0, 2) == 0)
                    GrowTime += (Time.deltaTime + Random.Range(growNoise.x, growNoise.y)) / 20 * GrowMultyplyerByRain;
                else
                    GrowTime += (Time.deltaTime + Random.Range(growNoise.x, growNoise.y)) / 20 * Random.Range(0.75f, 1);
            }
            else
                GrowTime += (Time.deltaTime + Random.Range(growNoise.x, growNoise.y)) / 20 * Random.Range(0.75f, 1);

            if (GrowTime >= growSpeed)
            {
                GrowState++;
                GrowTime = 0;
                GetComponent<MeshRenderer>().material = states[GrowState];
            }
        }
    }
    public bool IsFullyGrowen()
    {
        return GrowState >= states.Count - 1;
    }
}
