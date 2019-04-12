using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceBars : MonoBehaviour
{
    [SerializeField]
    private Transform healthBar;
    [SerializeField]
    private Transform energyBar;

    public void SetHealth(float sizePercent)
    {
        healthBar.localScale = new Vector3(sizePercent, 1);
    }

    public void SetEnergy(float sizePercent)
    {
        energyBar.localScale = new Vector3(sizePercent, 1);
    }
}
