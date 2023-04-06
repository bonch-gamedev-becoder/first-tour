using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    [SerializeField] int damage;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Enemy")
        {
            Destroy(gameObject);
            collision.collider.gameObject.GetComponent<EnemyCombat>().TakeDamage(damage);
        }

        if (collision.transform.tag == "blockingLayer")
        {
            Destroy(gameObject);
        }
    }
}
