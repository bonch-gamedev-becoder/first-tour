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
        //TODO: ������ ������� �� ������ ���� � ���������� �����
    }

    public void disableShield()
    {
        shieldActive = false;
        //TODO: ������ ������� �� ������ ���� � ����������� �����
    }

    private void TakeDamage(int damage)
    {
        if (!shieldActive)
        {
            currentHealth -= damage;
        }
    }

    
}
