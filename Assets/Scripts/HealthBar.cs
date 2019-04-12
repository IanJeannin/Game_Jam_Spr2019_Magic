using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private Transform bar;

    private void Start()
    {
        bar.localScale = new Vector3(.4f, 1);
    }

    public void SetHealth(float sizePercent)
    {
        bar.localScale = new Vector3(sizePercent, 1);
    }
}
