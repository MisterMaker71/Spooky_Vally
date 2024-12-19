using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gegner : MonoBehaviour
{
    public Animator animator;
    Transform player;
    [SerializeField] float blend;
    public float speed = 2.0f;
    public float followRange = 20.0f;
    public float disepearRange = 30.0f;
    public float maxHealth = 100;
    public float health = 100;
    private float timeSinceoutofrange = 0.0f;
    public float maxOutofrangetime = 10.0f;

    public float attackRange = 2.0f;
    public float attackDmg = 10.0f;
    public float attackCd = 2.0f;
    public bool isAtacking = false;
    public int waffenType = 1;

    

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
            if (distanceToplayer < followRange && distanceToplayer > 0.85f && !isAtacking)
            {
                if (blend < 1)
                    blend += Time.deltaTime * 5;
                if (blend > 1)
                    blend = 1;
                timeSinceoutofrange = 0.0f;
                transform.position = Vector3.MoveTowards(transform.position, player.position, Time.deltaTime * speed);
            
                //NEWENEWNNENEW
                if (distanceToplayer <= attackRange && !isAtacking)
                {
                    AttackPlayer();
                }
                
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

    void AttackPlayer()
    {
        if (animator != null)
            animator.SetInteger("schlagen", waffenType);
            //animator.SetInteger("schlagen", Random.Range(1, 3+1));
        isAtacking = true;
    }

    public void takedmg(float damage)
    {
        Partical.Create("blod_hit", transform.position + Vector3.up);
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
