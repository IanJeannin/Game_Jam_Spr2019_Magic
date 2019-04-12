using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepObjectOnScreen : MonoBehaviour
{
    [SerializeField]
    private float yMaxPos;

    private BoxCollider2D myColl;
    private Vector3 onScreenPos;
    private Vector3 bottomLeftWorldCoordinates;
    private Vector3 topRightWorldCoordinates;
    private Vector3 movementRangeMin;
    private Vector3 movementRangeMax;

    private void Start()
    {
        myColl = GetComponent<BoxCollider2D>();

        bottomLeftWorldCoordinates = Camera.main.ViewportToWorldPoint(Vector3.zero);//this is bottom left
        topRightWorldCoordinates = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 0));//this is top right

        movementRangeMin = bottomLeftWorldCoordinates + myColl.bounds.extents;//this is our minimum 
        movementRangeMax = topRightWorldCoordinates - myColl.bounds.extents;//this is our maximum
    }
    void Update()
    {
        onScreenPos = transform.position;
        onScreenPos.x = Mathf.Clamp(transform.position.x, movementRangeMin.x, movementRangeMax.x);
        onScreenPos.y = Mathf.Clamp(transform.position.y, movementRangeMin.y, yMaxPos);
        transform.Translate(onScreenPos - transform.position);
    }
}

