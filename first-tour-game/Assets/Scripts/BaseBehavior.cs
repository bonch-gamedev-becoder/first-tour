using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BaseBehavior : MonoBehaviour
{
    int maxHealth;
    int currentHealth;

    bool shieldActive = false;


    // Start is called before the first frame update
    void Start()
    {
        maxHealth = 500;
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void activateShield()
    {
        shieldActive = true; 
        //TODO: замена спрайта на спрайт базы с включенным щитом
    }

    public void disableShield()
    {
        shieldActive = false;
        //TODO: замена спрайта на спрайт базы с выключенным щитом
    }

    private void TakeDamage(int damage)
    {
        if (!shieldActive)
        {
            currentHealth -= damage;
        }
    }

    
}
