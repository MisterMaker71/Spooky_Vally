using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject gegnerPrefab;
    public Transform spawnPoint;
    public int numberOfEnemies = 3;

    private List<GameObject> spawnedEnemies = new List<GameObject>();

    void Start()
    {
        SpawnEnemies();
    }
   

    void SpawnEnemies()
    {
        for (int i = 0; i < numberOfEnemies - spawnedEnemies.Count ; i++)
        {
            GameObject newEnemy = Instantiate(gegnerPrefab, spawnPoint.position, Quaternion.identity);
            spawnedEnemies.Add(newEnemy);
        }
    }

    void Update()
    {
        if(Vector3.Distance(PlayerMovement.PlayerInstance.damagePosition.position, transform.position) > 10f)
        {
            SpawnEnemies();
        }
    spawnedEnemies.RemoveAll(enemy => enemy == null);
    }
}
