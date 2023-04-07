using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] Transform firePoint;
    [SerializeField] GameObject bulletPrefab;

    [SerializeField] float bulletForce = 20f;
    private bool canShoot;
    void Start()
    {
        canShoot = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && canShoot)
        {
             Shoot();
        }
    }

    void Shoot()
    {
        //canShoot = false;

        SpawnAndImpulseBullet();
        SoundsManager.instance.PlaySound("Shoot", true);

        //yield return new WaitForSeconds(0.25f);
        //canShoot = true;
    }

    private void SpawnAndImpulseBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
    }
}
