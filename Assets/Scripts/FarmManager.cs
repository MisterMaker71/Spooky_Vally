using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmManager : MonoBehaviour
{
    public static FarmManager instance;
    public bool Raining = false;
    public ParticleSystem rain;
    public ParticleSystem clouds;
    [Tooltip("procent 0-100"), Min(0)]
    public int rainPosebility = 5;
    [Tooltip("Time intervall to test to start raining"), Min(0)]
    public float rainColldown = 15;
    public Vector2 rainTime = new Vector2(10, 50);
    float rC = 0;
    float rTM = 0;
    float rT = -1;
    public List<Farmebel> rCrops = new List<Farmebel>();

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        rTM = Random.Range(rainTime.x, rainTime.y);
    }

    // Update is called once per frame
    void Update()
    {
        if(Raining)
        {
            rT += Time.deltaTime;
            if (rT >= rTM)
            {
                rT = -1;
                Raining = false;
            }
        }

        if(!Raining)
        {
            rC += Time.deltaTime;
            if (rC >= rainColldown)
            {
                rC = 0;
                int rf = Random.Range(0, 101);
                //print(rf);
                if (rf <= rainPosebility)
                {
                    rT = 0;
                    rTM = Random.Range(rainTime.x, rainTime.y);
                    Raining = true;
                }
            }
        }


        if (rain != null)
        {
            if (Raining && rain.isStopped)
            {
                rain.Play();
                clouds.Play();
            }
            if (!Raining && rain.isPlaying)
            {
                rain.Stop();
                clouds.Stop();
            }
        }
    }
    public Farmebel FindCrop(string name)
    {
        foreach (Farmebel item in rCrops)
        {
            if(item.gameObject.name == name)
            {
                return item;
            }
        }
        return null;
    }
}
