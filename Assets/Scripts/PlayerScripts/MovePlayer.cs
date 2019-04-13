using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private GameObject startingArrowAttackPosition;
    [HideInInspector]
    public bool isFacingRight;//tells you which way the player is facing to be referenced in attack and move stuff

    private Animator anim;
    private SpriteRenderer spriteRenderer;

    private string horizontalName, verticalName;
    private float horizontalValue, verticalValue;
    private float arrowAttackPosition;
    private float normalAttackPosition;

    private void Start()
    {
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        arrowAttackPosition = startingArrowAttackPosition.transform.position.x;
        normalAttackPosition = startingArrowAttackPosition.transform.position.x;
        horizontalName = "Horizontal";
        verticalName = "Vertical";
    }
    private void FixedUpdate()
    {
        Move();
    }

    void Update()
    {
        GetInput();
        SetAnimBasedOnInput();//tells the animator whether the player is moving or not
        UpdatePlayerDirection();
    }

    void SetAnimBasedOnInput()
    {
        if (Mathf.Abs(horizontalValue) > 0 || Mathf.Abs(verticalValue) > 0)
            anim.SetBool("isMoving", true);
        else anim.SetBool("isMoving", false);
    }

    void GetInput()
    {
        horizontalValue = Input.GetAxis(horizontalName);
        verticalValue = Input.GetAxis(verticalName);
    }

    void Move()
    {
        transform.Translate(horizontalValue * speed * Time.deltaTime, verticalValue * speed * Time.deltaTime, 0);
    }

    void UpdatePlayerDirection()//this is how u know which direction the player is facing.
    {
        if (horizontalValue > 0)
        {
            arrowAttackPosition = gameObject.transform.position.x-normalAttackPosition/2;
            spriteRenderer.flipX = false;
            isFacingRight = true;
        }
        if (horizontalValue < 0)
        {
            arrowAttackPosition = gameObject.transform.position.x+normalAttackPosition/2; //Flips the position of hitbox
            spriteRenderer.flipX = true;
            isFacingRight = false;
        }
        startingArrowAttackPosition.transform.position = new Vector3(arrowAttackPosition, startingArrowAttackPosition.transform.position.y);
    }
}
