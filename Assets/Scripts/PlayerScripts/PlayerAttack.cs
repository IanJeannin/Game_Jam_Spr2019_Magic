using System;
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

    [Header("Arrow Variables")]
    [SerializeField]
    private GameObject arrowPrefab;
    [SerializeField]
    private float arrowSpeed;
    [SerializeField]
    private float targetArrowLifetime, nonTargetArrowLifetime;
    [SerializeField]
    private ParticleSystem hurtParticles;
    [SerializeField]
    private CameraShake cameraScript;

    [Header("Sound Stuff")]
    [SerializeField]
    private AudioClip shootSound, enemyHurtSound;
    [SerializeField]
    private float shootVolume, enemyHurtVolume;
    [SerializeField]
    private float timeBeforeExplosionForFinalAttack;
    [SerializeField]
    private AudioClip startOfFinalAttack;
    [SerializeField]
    private AudioClip finalAttackExplosion;

    private Animator anim;
    private Player playerScript;
    private MovePlayer movePlayerScript;
    private GameObject arrowInstance;//gives me a ref to each arrow shot so I can pass in variables.
    private GameObject target;
    private MoveArrow moveArrow;
    private AudioSource audio;

    private bool ableToUnleashFinal = false;
    private float timeBetweenAttacks;

    private void Start()
    {
        anim = GetComponent<Animator>();
        playerScript = GetComponent<Player>();
        movePlayerScript = GetComponent<MovePlayer>();
        audio = GetComponent<AudioSource>();
    }

    private void Update()
    {
        //If the attack cd is 0
        if(timeBetweenAttacks <= 0)
        {
            if (Input.GetButtonDown("BowAttack"))
            {
                //AUDIO SHOOTING SOUND
                anim.SetTrigger("ShootBow");//get the animator doing that attack
                audio.PlayOneShot(shootSound, shootVolume);

                timeBetweenAttacks = originalTimeBetweenAttacks;//reset time
                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(punchAttackPosition.position, punchAttackRange, allEnemies);

                //I'm going to have the arrow go for the closest enemy so that I can actually move the arrow object towards them
                try//try to shoot it at a target by getting the closest nasty boi
                {
                    enemiesToDamage[0].GetComponent<Enemy>().TakeDamage(punchDamage);

                    target = enemiesToDamage[0].gameObject;

                    if (!target.GetComponent<Enemy>().deathCoroutineStarted)//if the enemy isn't already dying
                        Instantiate(hurtParticles, target.transform.position, Quaternion.identity);//put some blood particles on the enemy
                    audio.PlayOneShot(enemyHurtSound, enemyHurtVolume);

                    playerScript.AddEnergy(chargePerAttack);

                    arrowInstance = Instantiate(arrowPrefab, this.gameObject.transform.position, Quaternion.identity);//make the arrow appear
                    moveArrow = arrowInstance.GetComponent<MoveArrow>();
                    //SET ARROW VALUES
                    moveArrow.arrowSpeed = arrowSpeed;//pass in variables to specific arrows
                    moveArrow.isFacingRight = movePlayerScript.isFacingRight;//pass in variables to specific arrows
                    moveArrow.target = target;
                    moveArrow.targetArrowLifetime = targetArrowLifetime;
                    moveArrow.damage = punchDamage;//so the arrow knows how much damage
                }
                catch (IndexOutOfRangeException)//if there's nothing at that index, just shoot straight
                {
                    arrowInstance = Instantiate(arrowPrefab, this.gameObject.transform.position, Quaternion.identity);//make the arrow appear
                    moveArrow = arrowInstance.GetComponent<MoveArrow>();
                    //SET ARROW VALUES
                    moveArrow.arrowSpeed = arrowSpeed;//pass in variables to specific arrows
                    moveArrow.isFacingRight = movePlayerScript.isFacingRight;//pass in variables to specific arrows
                    moveArrow.nonTargetArrowLifetime = nonTargetArrowLifetime;
                }

            }
            else if (Input.GetButtonDown("FinalAttack"))
            {
                if (ableToUnleashFinal)
                {
                    //AUDIO do final attack sound
                    audio.Play(); //Final attack sound attached directly to audio source so it can be stopped.
                    StartCoroutine(FinalAttackSoundDelay());
                    timeBetweenAttacks = originalTimeBetweenAttacks;//sreset time
                    Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(finalAttackPosition.position, finalAttackRange, allEnemies);

                    for (int i = 0; i < enemiesToDamage.Length; i++)
                    {
                        enemiesToDamage[i].GetComponent<Enemy>().TakeDamage(finalDamage);
                        playerScript.AddEnergy(chargePerAttack);
                    }

                    StartCoroutine(cameraScript.DoCameraShake(.2f));//HACk magic number

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
    
    public IEnumerator FinalAttackSoundDelay()
    {
        yield return new WaitForSeconds(timeBeforeExplosionForFinalAttack);
        audio.Stop(); //Stops playing the star sounds.
        audio.PlayOneShot(finalAttackExplosion);
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
