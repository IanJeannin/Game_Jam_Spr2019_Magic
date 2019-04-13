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
    [Tooltip("The amount of time an enemy will pause after being hit.")]
    [SerializeField]
    private float stunnedTime;
    [Tooltip("The amount of time an enemy will freeze after falling below the enemyDazeThreshold.")]
    [SerializeField]
    private float dazedTime;


    //Will be false when the enemy is in the middle of an attack.
    private bool enemyCanMove = true;
    //Placeholder variable to stop enemy from moving if their attack animation is playing
    private float enemyAttackAnimationLength=1;

    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (enemyCanMove)
        {
            if (DistanceBetweenEnemyAndPlayer() <= enemyStoppingDistance)//stop moving and attack if within melee of player
            {
                StartCoroutine(EnemyAttackPause());
                anim.SetBool("isMoving", false);//let the animator know the enemy has stopped moving

            }
            else if (DistanceBetweenEnemyAndPlayer() < MinDistanceNeededToSeePlayer)//if the enemy can see the player
            {
                transform.position = Vector3.MoveTowards(this.transform.position, player.transform.position, enemySpeed * Time.deltaTime);//move directly towards the player
                anim.SetBool("isMoving", true);//let the animator know the enemy has started moving
            }
            else//default
            {
                transform.Translate(Vector3.left * enemySpeed * Time.deltaTime);//move left
                anim.SetBool("isMoving", true);//let the animator know the enemy has started moving
            }
        }
    }

    float DistanceBetweenEnemyAndPlayer()
    {
        return Vector3.Distance(player.transform.position, this.transform.position);
    }

    public void DazeEnemy()
    {
        Debug.Log("Daze called");
        StartCoroutine(DazeTimer());
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
        anim.SetTrigger("Attack");//let the animator know the enemy is attacking
        yield return new WaitForSeconds(enemyAttackAnimationLength);
        enemyCanMove = true;
        player.GetComponent<Player>().TakeDamage(enemyDamage);
    }

    private IEnumerator DazeTimer()
    {
        Debug.Log("Daze Timer Called");
        enemyCanMove = false;
        yield return new WaitForSeconds(dazedTime);
        enemyCanMove = true;
    }

    private IEnumerator StunTimer()
    {
        Debug.Log("Stun Timer Called");
        enemyCanMove = false;
        yield return new WaitForSeconds(stunnedTime);
        enemyCanMove = true;
    }
}
