using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gegner : MonoBehaviour
{
    [SerializeField] Animator animator;
    Transform player;
    [SerializeField] float blend;
    public float speed = 2.0f;
    public float followRange = 20.0f;
    public float disepearRange = 30.0f;
    public float health = 100;
    private float timeSinceoutofrange = 0.0f;
    public float maxOutofrangetime = 10.0f;

    //takedmg 
    //eigenes script für den spawner
    //spawner als likste 3gegener 

    // Update is called once per frame
    void Start()
    {
        player = PlayerMovement.PlayerInstance.transform;
    }
    void Update()
    {
        if (player != null)
        {
            transform.LookAt(player.transform.position);

            float distanceToplayer = Vector3.Distance(transform.position, player.position);
            if (distanceToplayer < followRange)
            {
                if (blend < 1)
                    blend += Time.deltaTime * 5;
                if (blend > 1)
                    blend = 1;
                timeSinceoutofrange = 0.0f;
                transform.position = Vector3.MoveTowards(transform.position, player.position, Time.deltaTime * speed);
            }
            else if (distanceToplayer > disepearRange)
            {
                blend = 0;
                timeSinceoutofrange += Time.deltaTime;
            }
            else
            {
                if (blend > 0)
                    blend -= Time.deltaTime * 5;
                if (blend < 0)
                    blend = 0;
                timeSinceoutofrange = 0;
            }
            if (animator != null)
                animator.SetFloat("walkingBlend", blend);
        }

        if (timeSinceoutofrange >= maxOutofrangetime)
        {
            Die();
        }
    }

    public void takedmg(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
