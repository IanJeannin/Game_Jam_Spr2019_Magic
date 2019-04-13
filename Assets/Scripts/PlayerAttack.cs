using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField]
    private float originalTimeBetweenAttacks;
    [SerializeField]
    private Transform punchAttackPosition;
    [SerializeField]
    private float punchAttackRange;
    [SerializeField]
    private LayerMask allEnemies;
    [SerializeField]
    private float punchDamage;
    [SerializeField]
    private float chargePerAttack;
    [SerializeField]
    private Transform finalAttackPosition;
    [SerializeField]
    private float finalAttackRange;
    [SerializeField]
    private float finalDamage;

    private bool ableToUnleashFinal = false;

    private float timeBetweenAttacks;

    private void Update()
    {
        //If the attack cd is 0
        if(timeBetweenAttacks<=0)
        {
            if(Input.GetButton("BowAttack"))
            {
                timeBetweenAttacks = originalTimeBetweenAttacks;
                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(punchAttackPosition.position,punchAttackRange, allEnemies);
                for (int i = 0; i < enemiesToDamage.Length; i++)
                {
                    enemiesToDamage[i].GetComponent<Enemy>().TakeDamage(punchDamage);
                    gameObject.GetComponent<Player>().AddEnergy(chargePerAttack);
                }
            }
            else if(Input.GetButton("FinalAttack"))
            {
                if (ableToUnleashFinal)
                {
                    timeBetweenAttacks = originalTimeBetweenAttacks;
                    Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(finalAttackPosition.position, finalAttackRange, allEnemies);
                    for (int i = 0; i < enemiesToDamage.Length; i++)
                    {
                        enemiesToDamage[i].GetComponent<Enemy>().TakeDamage(finalDamage);
                        gameObject.GetComponent<Player>().AddEnergy(chargePerAttack);
                    }
                }
            }
        }
        else
        {
            timeBetweenAttacks -= Time.deltaTime;
        }
    }

    public void UnlockFinalAttack()
    {
        ableToUnleashFinal = true;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(punchAttackPosition.position, punchAttackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(finalAttackPosition.position, finalAttackRange);
    }
}
