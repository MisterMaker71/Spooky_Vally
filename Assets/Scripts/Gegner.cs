using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gegner : MonoBehaviour
{
    public Transform player;
    public float speed = 2.0f;

    public float followRange = 10.0f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    if (player != null)
    {
     

        if(Vector3.Distance(transform.position,player.position) <followRange)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position , Time.deltaTime * speed);
        }
    }
    }
}
