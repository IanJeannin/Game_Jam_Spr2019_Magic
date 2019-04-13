using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveArrow : MonoBehaviour
{
    [HideInInspector]
    public float arrowSpeed;//we need to know how hard to shoot the arrow, so we pass in a ref to the arrow

    [HideInInspector]
    public GameObject player;//need to know the direction the player was facing, so we pass in a ref to the player

    [HideInInspector]
    public GameObject target;

    private bool isFacingRight;
    
    private void Start()
    {
        isFacingRight = player.GetComponent<MovePlayer>().isFacingRight;      
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(this.transform.position, target.transform.position, arrowSpeed * Time.deltaTime);//move directly towards the player
    }
}
