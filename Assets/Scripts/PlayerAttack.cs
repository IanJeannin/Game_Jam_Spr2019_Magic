using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField]
    private float originalTimeBetweenAttacks;
    [SerializeField]
    private Transform attackPosition;
    [SerializeField]
    private float attackRange;
    [SerializeField]
    private LayerMask allEnemies;
    [SerializeField]
    private float bowDamage;

    private float timeBetweenAttacks;

    private void Update()
    {
        //If the attack cd is 0
        if(timeBetweenAttacks<=0)
        {
            if(Input.GetButton("BowAttack"))
            {
                timeBetweenAttacks = originalTimeBetweenAttacks;
                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPosition.position,attackRange, allEnemies);
                for (int i = 0; i < enemiesToDamage.Length; i++)
                {
                    enemiesToDamage[i].GetComponent<Enemy>().TakeDamage(bowDamage);
                }
            }
        }
        else
        {
            timeBetweenAttacks -= Time.deltaTime;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPosition.position, attackRange);
    }
}
