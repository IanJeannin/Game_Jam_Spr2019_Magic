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
    private GameObject resourceBar;

    
    private float currentHealth;
    private static float currentCharge;
    

    private void Awake()
    {
        currentHealth = maxHealth;
        currentCharge = 0;
        resourceBar.GetComponent<ResourceBars>().SetEnergy(currentCharge);
    }

    public void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;
        resourceBar.GetComponent<ResourceBars>().SetHealth(currentHealth / 100);
        Debug.Log(currentHealth);
    }

    public void AddEnergy(float chargeAdded)
    {
        currentCharge += chargeAdded;
        //Prevents player going over max charge
        if (currentCharge>=100)
        {
            currentCharge = 100;
            gameObject.GetComponent<PlayerAttack>().UnlockFinalAttack();
        }
        resourceBar.GetComponent<ResourceBars>().SetEnergy(currentCharge/100);
    }


    private void Update()
    {
        if(currentHealth<=0)
        {
            SceneManager.LoadScene("DefeatScene");
        }
    }

}
