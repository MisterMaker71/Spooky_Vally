using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gegner : MonoBehaviour
{
    public Transform player;
    public float speed = 2.0f;
    public float followRange = 20.0f;
    public int health= 100;
    private float timeSinceoutofrange = 0.0f;
    public float maxOutofrangetime = 10.0f;
    
    //takedmg 
    //eigenes script für den spawner
    //spawner als likste 3gegener 

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            float distanceToplayer = Vector3.Distance(transform.position, player.position);
            if (distanceToplayer < followRange)
        {
            timeSinceoutofrange = 0.0f;
            transform.position = Vector3.MoveTowards(transform.position, player.position, Time.deltaTime * speed);
        }
        else
        {
            timeSinceoutofrange += Time.deltaTime;
        }
    }

        if (timeSinceoutofrange >= maxOutofrangetime)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
