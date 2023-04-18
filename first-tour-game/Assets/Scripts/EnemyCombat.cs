using JetBrains.Annotations;
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
    public int bulletDamage = 10;

    [SerializeField] int maxHealth;
    private int currentHealth;
    private bool canShoot;

    [SerializeField] GameObject thanosBonus;
    [SerializeField] GameObject shieldBonus;
    [SerializeField] GameObject invisibilityBonus;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        canShoot = true;
        deathEffect = deathEffectPermanent;
        animator = GetComponent<Animator>();
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

        if (gameObject.tag == "ArtilleryEnemy")
        {
            animator.SetTrigger("attack");
        }
        
        SoundsManager.instance.PlaySound("Shoot", true);
        
        yield return new WaitForSeconds(1f);
        canShoot = true;
    }

    private void SpawnAndImpulseBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bullet.AddComponent<BulletBehavior>();
        bullet.GetComponent<BulletBehavior>().executor = gameObject;
        bullet.GetComponent<BulletBehavior>().damage = bulletDamage;
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
    }

    public void TakeDamage(int damage, GameObject shooter)
    {
//        Debug.Log("Enemy taking damage. its hp is " + currentHealth);
        currentHealth -= damage;
        SoundsManager.instance.PlaySound("Hit", true);
        if (currentHealth < 0)
        {
            Death();
            GameManager.instance.AddPointsToTheShooter(shooter);
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
        if (randNumber >= 0 && randNumber <= 4)
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