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
    [SerializeField]
    private GameObject healthBar;

    
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
        healthBar.GetComponent<HealthBar>().SetHealth(currentHealth / 100);
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
