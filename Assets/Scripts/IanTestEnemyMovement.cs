using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IanTestEnemyMovement : MonoBehaviour
{
    private GameObject player;

    [SerializeField]
    private float MinDistanceNeededToSeePlayer;
    [SerializeField]
    private float enemySpeed;
    [SerializeField]
    private float enemyStoppingDistance;
    [SerializeField]
    private float enemyDamage;
    [SerializeField]
    private float stunnedTime;


    //Will be false when the enemy is in the middle of an attack.
    private bool enemyCanMove = true;
    //Placeholder variable to stop enemy from moving if their attack animation is playing
    private float enemyAttackAnimationLength=1;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (enemyCanMove)
        {
            if (DistanceBetweenEnemyAndPlayer() <= enemyStoppingDistance)
            {
                //Stop moving and attack
                StartCoroutine(EnemyAttackPause());
            }
            else if (DistanceBetweenEnemyAndPlayer() < MinDistanceNeededToSeePlayer)//if the enemy can see the player
            {
                transform.position = Vector3.MoveTowards(this.transform.position, player.transform.position, enemySpeed * Time.deltaTime);//move directly towards the player
                Debug.Log("I SEE YOU");
            }
            else//default
            {
                transform.Translate(Vector3.left * enemySpeed * Time.deltaTime);//move left
                Debug.Log("I MOVE LEFT");
            }
        }
    }

    float DistanceBetweenEnemyAndPlayer()
    {
        return Vector3.Distance(player.transform.position, this.transform.position);
    }


    public void StunEnemyBriefly()
    {
        StartCoroutine(StunTimer());
    }

    /// <summary>
    /// Counts the time that the enemy takes to attack, to prevent that enemy from moving during that time. 
    /// </summary>
    /// <returns></returns>
    private IEnumerator EnemyAttackPause()
    {
        enemyCanMove = false;
        yield return new WaitForSeconds(enemyAttackAnimationLength);
        enemyCanMove = true;
        Debug.Log("I ATTACK");
        player.GetComponent<Player>().TakeDamage(enemyDamage);
    }


    public IEnumerator StunTimer()
    {
        enemyCanMove = false;
        yield return new WaitForSeconds(stunnedTime);
        enemyCanMove = true;
    }
}
