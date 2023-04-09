using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    [SerializeField] int damage;
    private BoxCollider2D collider;
    private void Start()
    {
        collider = gameObject.GetComponent<BoxCollider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collistion for bullit is " + collision.transform.name);

        //player hit enemies
        if (gameObject.tag == "Bullet" && (collision.collider.tag == "Enemy" || collision.collider.tag == "ArtilleryEnemy"))
        {
            collision.gameObject.GetComponent<EnemyCombat>().TakeDamage(damage);
        }

        //enemies hit base
        if (collision.gameObject.tag == "Base" && tag != "Bullet")
        {
            Debug.Log("Enemy hit base!");
            GameManager.instance.currentBase.TakeDamage(damage);
        }

        Destroy(gameObject);
    }
}
