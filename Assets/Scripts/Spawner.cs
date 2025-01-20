using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] gegnerPrefab;
    public Transform spawnPoint;
    public int numberOfEnemies = 3;
    bool PlayerInside = false;
    [SerializeField] float spawnRadius = 10;

    private List<GameObject> spawnedEnemies = new List<GameObject>();

    void Start()
    {
        //SpawnEnemies();
    }
   

    void SpawnEnemies()
    {
        for (int i = 0; i < numberOfEnemies - spawnedEnemies.Count ; i++)
        {
            GameObject g = gegnerPrefab[Random.Range(0, gegnerPrefab.Length)];
            GameObject newEnemy = Instantiate(g, spawnPoint.position + new Vector3(Random.Range(-5, 5), 0, Random.Range(-5, 5)), Quaternion.identity, transform);
            newEnemy.name = g.name;
            if(newEnemy.GetComponent<Gegner>() != null)
            {
                newEnemy.GetComponent<Gegner>().isVisebel = false;
                newEnemy.GetComponent<Gegner>().canMove = false;
            }
            spawnedEnemies.Add(newEnemy);
        }
    }

    void Update()
    {
        if(Vector3.Distance(PlayerMovement.PlayerInstance.damagePosition.position, transform.position) < spawnRadius)
        {
            if(!PlayerInside)
            {
                PlayerInside = true;
                SpawnEnemies();
            }
        }
        else
        {
            PlayerInside = false;
        }
    spawnedEnemies.RemoveAll(enemy => enemy == null);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, spawnRadius);
    }
}
