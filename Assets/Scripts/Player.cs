using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthAndCharge : MonoBehaviour
{
    [SerializeField]
    private float maxHealth;
    [SerializeField]
    private float maxCharge;

    private float currentHealth;
    private float currentCharge;

    private void Awake()
    {
        currentHealth = maxHealth;
        currentCharge = 0;
    }
}
