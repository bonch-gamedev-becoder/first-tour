using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] Transform firePoint;
    [SerializeField] GameObject bulletPrefab;

    [SerializeField] float bulletForce = 20f;
    public int bulletDamage = 10;
    private bool canShoot;

    public KeyCode shootButton;

    void Start()
    {
        canShoot = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(shootButton) && canShoot)
        {
             Shoot();
        }
    }

    void Shoot()
    {
        SpawnAndImpulseBullet();
        SoundsManager.instance.PlaySound("Shoot", true);
    }

    private void SpawnAndImpulseBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bullet.AddComponent<BulletBehavior>();
        bullet.GetComponent<BulletBehavior>().executor = gameObject;
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);

    }
}
