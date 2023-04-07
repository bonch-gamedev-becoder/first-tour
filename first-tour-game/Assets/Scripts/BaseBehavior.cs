using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BaseBehavior : MonoBehaviour
{
    [SerializeField] int maxHealth;
    int currentHealth;

    public bool shieldActive = false;


    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame

    public void ActivateShield()
    {
        shieldActive = true; 
        //TODO: замена спрайта на спрайт базы с включенным щитом
    }

    public void DisableShield()
    {
        shieldActive = false;
        //TODO: замена спрайта на спрайт базы с выключенным щитом
    }

    public void TakeDamage(int damage)
    {
        if (!shieldActive)
        {
            currentHealth -= damage;
        }
        if (currentHealth < 0)
        {
            Death();
        }
    }

    private void Death()
    {
        Destroy(gameObject);
        //TODO game over restart level
    }
}
