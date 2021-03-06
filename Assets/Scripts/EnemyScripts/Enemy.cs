﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float maxHealth=100;

    [Tooltip("If an enemy falls below this, they will be dazed.")]
    [SerializeField]
    private float enemyDazeThreshold;

    [HideInInspector]
    public bool deathCoroutineStarted;//this tells us whether the enemy is dying

    private IanTestEnemyMovement movementScript;
    private Animator anim;
    private SpriteRenderer renderer;

    private float health;


    private void Start()
    {
        health = maxHealth;
        anim = GetComponent<Animator>();
        movementScript = GetComponent<IanTestEnemyMovement>();
        renderer = GetComponent<SpriteRenderer>();
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
        if (health <= 0)
        {
            //AUDIO ENEMY DEATH sound
            if (!deathCoroutineStarted)
                StartCoroutine(FadeOutDeath());
        }
    }

    private IEnumerator FadeOutDeath()
    {
        deathCoroutineStarted = true;

        yield return new WaitForSeconds(.2f);//wait for the arrow to hit it

        for (float f = 1f; f > 0; f -= .1f)
        {
            renderer.color = new Color(1, 1, 1, f);
            yield return new WaitForSeconds(.05f);
        }

        Destroy(this.gameObject);
    }
}
