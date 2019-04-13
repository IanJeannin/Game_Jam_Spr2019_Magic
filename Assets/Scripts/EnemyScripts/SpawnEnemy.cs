using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField]
    private Transform[] spawnPoints;

    [SerializeField]
    private float minTimeBetweenSpawn, maxTimeBetweenSpawn;

    [SerializeField]
    private GameObject enemyPrefab;

    private GameObject enemyInstance;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Spawn());
    }

    private Vector3 GetRandomPoint()//returns a random spawn point
    {
        return spawnPoints[Random.Range(0, 3)].position;
    }

    private IEnumerator Spawn()
    {
        for (; ; )
        {
            yield return new WaitForSeconds(randomTimeBetweenSpawn());//wait
            enemyInstance = Instantiate(enemyPrefab, GetRandomPoint(), Quaternion.identity);//spawn the enemy
        }
    }

    private float randomTimeBetweenSpawn()
    {
        return Random.Range(minTimeBetweenSpawn, maxTimeBetweenSpawn);
    }
}
