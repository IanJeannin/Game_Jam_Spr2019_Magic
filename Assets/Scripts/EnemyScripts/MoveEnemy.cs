using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEnemy : MonoBehaviour
{
    private GameObject player;

    [SerializeField]
    private float MinDistanceNeededToSeePlayer;

    [SerializeField]
    private float enemySpeed;

    [SerializeField]
    private float enemyStoppingDistance;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if(DistanceBetweenEnemyAndPlayer() <= enemyStoppingDistance)
        {
            //Stop moving and attack
            Debug.Log("I ATTACK");
        }
        else if (DistanceBetweenEnemyAndPlayer() < MinDistanceNeededToSeePlayer)//if the enemy can see the player
        {
            transform.Translate(Vector3.MoveTowards(this.transform.position, player.transform.position, enemySpeed * Time.deltaTime));//move directly towards the player
            Debug.Log("I SEE YOU");
        }
        else//default
        {
            transform.Translate(Vector3.left * enemySpeed * Time.deltaTime);//move left
            Debug.Log("I MOVE LEFT");
        }      
    }

    float DistanceBetweenEnemyAndPlayer()
    {
        return Vector3.Distance(player.transform.position, this.transform.position);
    }
}
