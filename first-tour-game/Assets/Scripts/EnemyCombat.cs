using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class EnemyCombat : MonoBehaviour
{
    public GameObject deathEffectPermanent;
    public GameObject deathEffect;

    [SerializeField] Transform firePoint;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float bulletForce = 20f;

    [SerializeField] int maxHealth;
    private int currentHealth;
    private bool canShoot;

    [SerializeField] GameObject thanosBonus;
    [SerializeField] GameObject shieldBonus;
    [SerializeField] GameObject invisibilityBonus;
    [SerializeField] GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        canShoot = true;
        deathEffect = deathEffectPermanent;
    }

    // Update is called once per frame
    void Update()
    {
        if (canShoot)
        {
            StartCoroutine(Shoot());
        }
    }

    IEnumerator Shoot()
    {
        canShoot = false;

        SpawnAndImpulseBullet();
        SoundsManager.instance.PlaySound("Shoot", true);

        yield return new WaitForSeconds(1f);
        canShoot = true;
    }

    private void SpawnAndImpulseBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
    }

    public void TakeDamage(int damage)
    {
        Debug.Log("Enemy taking damage. its hp is " + currentHealth);
        currentHealth -= damage;
        SoundsManager.instance.PlaySound("Hit", true);
        if (currentHealth < 0)
        {
            Death();
        }
    }

    public void Death()
    {
        GameManager.instance.AddPoints(1);
        if (deathEffect != null)
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity);
        }
        
        Destroy(gameObject);

        Random rand = new Random();
        int randNumber = rand.Next(0, 49);

        //Thanos bonus (probability chance 1/10)
        if (randNumber >= 0 && randNumber <= 25)
        {
            Instantiate(thanosBonus, transform.position, Quaternion.identity);
        }
        //Shield bonus (probability chance 1/5)
        else if (randNumber >= 5 && randNumber <= 14)
        {
            Instantiate(shieldBonus, transform.position, Quaternion.identity);
        }
        //Shield bonus (probability chance 4/25)
        else if (randNumber >= 15 && randNumber <= 22)
        {
            Instantiate(invisibilityBonus, transform.position, Quaternion.identity);
        }
    }
}