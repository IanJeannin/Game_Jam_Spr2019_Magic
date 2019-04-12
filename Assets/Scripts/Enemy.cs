using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float health=10;

    public void TakeDamage(float damage)
    {
        health -= damage;
        Debug.Log($"Enemy Health: { health}");
        gameObject.GetComponent<IanTestEnemyMovement>().StunEnemyBriefly();
    }

    private void Update()
    {
        if(health<=0)
        {
            Destroy(gameObject);
        }
    }
}
