using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float maxHealth;
    [SerializeField]
    private float maxCharge;

    
    private float currentHealth;
    private static float currentCharge;
    

    private void Awake()
    {
        currentHealth = maxHealth;
        currentCharge = 0;
    }

    public void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;
        Debug.Log(currentHealth);
    }

    private void Update()
    {
        if(currentHealth<=0)
        {
            SceneManager.LoadScene("DefeatScene");
        }
    }

}
