using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    private Transform[] spawnPoints;

    [SerializeField]
    private float timeBetweenEnemies;

    [SerializeField]
    private GameObject enemyPrefab;

    private GameObject enemyInstance;

    // Start is called before the first frame update
    void Start()
    {
        spawnPoints = GetComponentsInChildren<Transform>();
    }

    private Transform GetRandomPoint()//returns a random spawn point
    {
        return spawnPoints[Random.Range(0, 3)];
    }

    private IEnumerator Spawn()
    {
        enemyInstance = Instantiate(enemyPrefab, GetRandomPoint());//spawn the enemy
        yield return new WaitForSeconds(timeBetweenEnemies);//wait
    }
}
