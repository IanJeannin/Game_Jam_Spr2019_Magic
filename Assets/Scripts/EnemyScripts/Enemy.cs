using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float maxHealth=100;

    [Tooltip("If an enemy falls below this, they will be dazed.")]
    [SerializeField]
    private float enemyDazeThreshold;

    private float health;
    private IanTestEnemyMovement movementScript;
    private Animator anim;

    private void Start()
    {
        health = maxHealth;
        anim = GetComponent<Animator>();
        movementScript = GetComponent<IanTestEnemyMovement>();
    }

    public void TakeDamage(float damage)
    {
        //AUDIO enemy is hurt sound
        anim.SetTrigger("Hurt");//tell the animator to do the hurt anim

        health -= damage;
        Debug.Log($"Enemy Health: { health}");
        if (health <= enemyDazeThreshold)
        {
            movementScript.DazeEnemy();
        }
        else
        {
            movementScript.StunEnemyBriefly();//This is when enemy hurt
        }
    }

    private void Update()
    {
        if(health<=0)
        {
            //AUDIO ENEMY DEATH sound
            Destroy(gameObject);//maybe move this to a coroutine so we can play a death animation or hurt frame
        }
    }
}
