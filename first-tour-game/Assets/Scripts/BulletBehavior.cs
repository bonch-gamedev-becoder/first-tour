using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class BulletBehavior : MonoBehaviour
{
    public GameObject executor;
    public int damage;
    private Rigidbody2D rb;
    [SerializeField] GameObject deathEffect;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("Collistion for bullit is " + collision.transform.name);

        if (gameObject.CompareTag("Bullet") && collision.gameObject.CompareTag("Player"))
        {
            return;
        }

        //??????? ???? ?? ?????
        if (gameObject.CompareTag("Bullet") && collision.gameObject.CompareTag("blockingLayer"))
        {
            Vector2 wallNormal = collision.contacts[0].normal;
            Vector2 dir = Vector2.Reflect(rb.velocity, wallNormal).normalized;
            rb.velocity = dir * 10f;
            StartCoroutine(DestroyAfterTime());
            return;
        }

        //player hit enemies
        if (gameObject.tag == "Bullet" && collision.gameObject.layer == 9)
        {
            collision.gameObject.GetComponent<EnemyCombat>().TakeDamage(damage, executor);
        }

        if (gameObject.tag == "BulletHunter" && collision.collider.tag == "Player")
        {
            Random random = new Random();
            collision.gameObject.transform.position = new Vector3(random.Next(3, 7), random.Next(3, 7), 0);
            Destroy(executor);
        }

        //enemies hit base
        if (collision.gameObject.tag == "Base" && tag != "Bullet")
        {
            Debug.Log("Enemy hit base!");
            GameManager.instance.currentBase.TakeDamage(damage);
        }

        if (collision.gameObject.tag == "blockingLayerBreakable" && tag == "Bullet")
        {
            Destroy(collision.gameObject);
            Instantiate(deathEffect, collision.gameObject.transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }

    IEnumerator DestroyAfterTime()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
