using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBloodstain : MonoBehaviour
{
    private SpriteRenderer spriteRend;
    [SerializeField]
    private Sprite[] bloodSplatters;
    // Start is called before the first frame update
    void Start()
    {
        spriteRend = GetComponent<SpriteRenderer>();
        int rand = Random.Range(0, bloodSplatters.Length);
        spriteRend.sprite = bloodSplatters[rand];
    }
}
