using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class BaseBehavior : MonoBehaviour
{
    [SerializeField] int maxHealth;
    public int currentHealth;

    public bool shieldActive = false;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame

    public void ActivateShield()
    {
        shieldActive = true;
        animator.SetBool("hasShield", true);
    }

    public void DisableShield()
    {
        shieldActive = false;
        animator.SetBool("hasShield", false);
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
        BaseHpTracker.instance.ChangeText();
    }

    private void Death()
    {
        Destroy(gameObject);
        //TODO game over restart level
    }
}
