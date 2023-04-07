using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class BaseBehavior : MonoBehaviour
{
    [SerializeField] int maxHealth;
    [SerializeField] int currentHealth;

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
        //TODO: ?????? ??????? ?? ?????? ???? ? ?????????? ?????
    }

    public void DisableShield()
    {
        shieldActive = false;
        //TODO: ?????? ??????? ?? ?????? ???? ? ??????????? ?????
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
