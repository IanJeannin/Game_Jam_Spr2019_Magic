using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveArrow : MonoBehaviour
{
    [HideInInspector]
    public float arrowSpeed;//we need to know how hard to shoot the arrow, so we pass in a ref to the arrow

    [HideInInspector]
    public GameObject target;

    [HideInInspector]
    public float targetArrowLifetime, nonTargetArrowLifetime;

    [HideInInspector]
    public bool isFacingRight;

    [HideInInspector]
    public float damage;
    private float elapsedTime;
    [SerializeField]
    private LayerMask allEnemies;
    [SerializeField]
    private GameObject player;

    private SpriteRenderer renderer;
        
    private void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        elapsedTime = 0;
    }


    private void Update()
    {
        UpdateMove();//move the arrow accordingly
        CheckTimerForDestroy();//check to see if you should destroy the arrow based on the arrow type

        elapsedTime += Time.deltaTime;
    }

    void CheckTimerForDestroy()
    {
        if (target != null)
        {
            if (elapsedTime >= targetArrowLifetime)//when the timer for a target arrow is up
            {
                StartCoroutine(FadeArrowOut());
                elapsedTime = 0;
            }
        }
        else
        {
            if (elapsedTime >= nonTargetArrowLifetime)//when the timer for a normal arrow is up...
            {
                StartCoroutine(FadeArrowOut());
                elapsedTime = 0;//reset the timer
            }
        }
    }

    void UpdateMove()
    {
        if (target != null)//if we have a target, move towards the target
            transform.position = Vector3.MoveTowards(this.transform.position, target.transform.position, arrowSpeed * Time.deltaTime);//move directly towards the player
        else
        {
            if (isFacingRight)//else just go right if we're facing right
            {
                transform.Translate(Vector3.right * arrowSpeed * Time.deltaTime);

            }
            else if (!isFacingRight)//or if we're facing left, go left
                transform.Translate(Vector3.left * arrowSpeed * Time.deltaTime);

        }
            Collider2D[] enemy = new Collider2D[10];
            ContactFilter2D contactFilter = new ContactFilter2D();
            contactFilter.SetLayerMask(allEnemies);
            gameObject.GetComponent<BoxCollider2D>().OverlapCollider(contactFilter, enemy);
            if (enemy[0] != null)
            {
                Destroy(gameObject);
                enemy[0].gameObject.GetComponent<Enemy>().TakeDamage(player.GetComponent<PlayerAttack>().GetArrowDamage());
                Instantiate(player.GetComponent<PlayerAttack>().GetDamageParticles(), enemy[0].transform.position, Quaternion.identity);//put some blood particles on the enemy
            }
    }

    private IEnumerator FadeArrowOut()
    {
        yield return new WaitForSeconds(.5f);//wait for the arrow to hit it

        for (float f = 1f; f > 0; f -= .1f)
        {
            renderer.color = new Color(1, 1, 1, f);
            yield return new WaitForSeconds(.1f);
        }

        Destroy(this.gameObject);
    }
}


