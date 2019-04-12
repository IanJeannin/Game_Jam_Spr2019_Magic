using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    [SerializeField]
    private float speed;

    private string horizontalName, verticalName;
    private float horizontalValue, verticalValue;

    private void Start()
    {
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
}
