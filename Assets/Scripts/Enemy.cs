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

    private void Start()
    {
        health = maxHealth;
        movementScript = GetComponent<IanTestEnemyMovement>();
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        Debug.Log($"Enemy Health: { health}");
        if (health <= enemyDazeThreshold)
        {
            movementScript.DazeEnemy();
        }
        else
        {
            movementScript.StunEnemyBriefly();
        }
        
    }

    private void Update()
    {
        if(health<=0)
        {
            Destroy(gameObject);
        }
    }
}
