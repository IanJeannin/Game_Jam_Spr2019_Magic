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

    private Animator anim;
    private Player playerScript;

    private bool ableToUnleashFinal = false;
    private float timeBetweenAttacks;

    private void Start()
    {
        anim = GetComponent<Animator>();
        playerScript = GetComponent<Player>();
    }

    private void Update()
    {
        //If the attack cd is 0
        if(timeBetweenAttacks <= 0)
        {
            if(Input.GetButton("BowAttack"))
            {
                anim.SetTrigger("ShootBow");//get the animator doing that attack

                timeBetweenAttacks = originalTimeBetweenAttacks;//reset time
                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(punchAttackPosition.position,punchAttackRange, allEnemies);

                for (int i = 0; i < enemiesToDamage.Length; i++)
                {
                    enemiesToDamage[i].GetComponent<Enemy>().TakeDamage(punchDamage);
                    playerScript.AddEnergy(chargePerAttack);
                }
            }
            else if(Input.GetButton("FinalAttack"))
            {
                if (ableToUnleashFinal)
                {
                    timeBetweenAttacks = originalTimeBetweenAttacks;//sreset time
                    Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(finalAttackPosition.position, finalAttackRange, allEnemies);

                    for (int i = 0; i < enemiesToDamage.Length; i++)
                    {
                        enemiesToDamage[i].GetComponent<Enemy>().TakeDamage(finalDamage);
                        playerScript.AddEnergy(chargePerAttack);
                    }

                    ableToUnleashFinal = false;//after you do the attack, you can't do it again right away
                    playerScript.AddEnergy(-100); //Sets bar back to 0 energy, figure out how to make this not a magic number
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
