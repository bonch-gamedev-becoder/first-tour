using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    [SerializeField] int damage;
    private Rigidbody2D rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            return;
        }
//        Debug.Log("Collistion for bullit is " + collision.transform.name);

        if (gameObject.CompareTag("Bullet") && collision.gameObject.CompareTag("blockingLayer"))
        {
            Vector2 wallNormal = collision.contacts[0].normal;
            Vector2 dir = Vector2.Reflect(rb.velocity, wallNormal).normalized;
            rb.velocity = dir * 10f;
            StartCoroutine(DestroyAfterTime());
            return;
        }

        //player hit enemies
        if (gameObject.tag == "Bullet" && (collision.collider.tag == "Enemy" || collision.collider.tag == "ArtilleryEnemy"))
        {
            collision.gameObject.GetComponent<EnemyCombat>().TakeDamage(damage);
        }

        //enemies hit base
        if (collision.gameObject.tag == "Base" && tag != "Bullet")
        {
//            Debug.Log("Enemy hit base!");
            GameManager.instance.currentBase.TakeDamage(damage);
        }

        if (collision.gameObject.tag == "blockingLayerBreakable")
        {
            Destroy(collision.gameObject);
        }

        Destroy(gameObject);
    }

    IEnumerator DestroyAfterTime()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
