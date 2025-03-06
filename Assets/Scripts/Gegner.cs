using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Gegner : MonoBehaviour
{
    public Animator animator;
    NavMeshAgent agent;
    Transform player;
    public bool isVisebel = true;
    public bool canMove = true;

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
        agent = GetComponent<NavMeshAgent>();
        player = PlayerMovement.PlayerInstance.transform;
    }
    void Update()
    {
        if(!isVisebel)
        {
            if(Camera.main.WorldToViewportPoint(transform.position).x >  1.01 ||
               Camera.main.WorldToViewportPoint(transform.position).x < -0.01 ||
               Camera.main.WorldToViewportPoint(transform.position).y >  1.05 ||
               Camera.main.WorldToViewportPoint(transform.position).y < -0.05)
            {
                isVisebel = true;
                canMove = true;
            }
        }

        if (player != null && canMove)
        {
            //transform.LookAt(player.transform.position);

            float distanceToplayer = Vector3.Distance(transform.position, player.position);
            if (distanceToplayer < followRange && distanceToplayer > 0.85f && !isAtacking)
            {
                if (blend < 1)
                    blend += Time.deltaTime * 5;
                if (blend > 1)
                    blend = 1;
                timeSinceoutofrange = 0.0f;
                agent.SetDestination(player.position);
                //transform.position = Vector3.MoveTowards(transform.position, player.position, Time.deltaTime * speed);

                //NEWENEWNNENEW
                if (distanceToplayer <= attackRange && !isAtacking)
                {
                    AttackPlayer();
                    agent.isStopped = true;
                }
                if(!isAtacking && agent.isStopped)
                {
                    agent.isStopped = false;
                }
                
            }
            else if (distanceToplayer > disepearRange)
            {
                agent.isStopped = true;
                blend = 0;
                timeSinceoutofrange += Time.deltaTime;
            }
            else
            {
                agent.isStopped = true;
                if (blend > 0)
                    blend -= Time.deltaTime * 5;
                if (blend < 0)
                    blend = 0;
                timeSinceoutofrange = 0;
            }
            if (animator != null)
                animator.SetFloat("walkingBlend", blend);
        }
        else
        {
            if (blend > 0)
                blend -= Time.deltaTime * 5;
            if (blend < 0)
                blend = 0;
            timeSinceoutofrange = 0;
            if (animator != null)
                animator.SetFloat("walkingBlend", blend);
        }

        if (timeSinceoutofrange >= maxOutofrangetime)
        {
            Die();
        }

        //for (int i = 0; i < transform.childCount; i++)
        //{
        //    transform.GetChild(i).gameObject.SetActive(isVisebel); ;
        //}
        foreach (var item in GetComponentsInChildren<Renderer>())
        {
            item.enabled = isVisebel;
        }
        foreach (var item in GetComponentsInChildren<Collider>())
        {
            item.enabled = isVisebel;
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
        GameObject g = Instantiate(Resources.Load<GameObject>(name + "-Ragdoll"), transform.position, transform.rotation, transform.parent);
        if (g.GetComponentInChildren<Ragdoll>() != null)
            g.GetComponentInChildren<Ragdoll>().CoppyHumanoid(transform);
        Destroy(gameObject);
    }
}
