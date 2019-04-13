using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    [SerializeField]
    private float speed;

    private Animator anim;

    private string horizontalName, verticalName;
    private float horizontalValue, verticalValue;

    private void Start()
    {
        anim = GetComponent<Animator>();

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
            transform.localScale = new Vector3(1f, 1f, 1f);//facing right
        }
        if (horizontalValue < 0)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);//facing left
        }
    }
}
