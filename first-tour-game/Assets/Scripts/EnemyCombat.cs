using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class EnemyCombat : MonoBehaviour
{
    [SerializeField] GameObject deathEffect;

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

    private PlayerBonusBehavior playerBonusBehavour;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        canShoot = true;
        playerBonusBehavour = player.GetComponent<PlayerBonusBehavior>();
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

        yield return new WaitForSeconds(0.25f);
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
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
        playerBonusBehavour.totalResources++;
       // playerBonusBehavour.resourcesCounterText.text = "Resources: " + playerBonusBehavour.totalResources;

        Random rand = new Random();
        int randNumber = rand.Next(0, 9);

        if (randNumber >= 0 && randNumber <= 3)
        {
            Instantiate(thanosBonus, transform.position, Quaternion.identity);
        }
        else if (randNumber >= 8 && randNumber <= 9)
        {
            Instantiate(shieldBonus, transform.position, Quaternion.identity);
        }
        else if (randNumber >= 4 && randNumber <= 7)
        {
            Instantiate(invisibilityBonus, transform.position, Quaternion.identity);
        }
    }
}