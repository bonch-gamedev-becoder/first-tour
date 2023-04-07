using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{

    [SerializeField] int damage;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collistion for bullit is " + collision.transform.name);

        if (collision.collider.tag == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyCombat>().TakeDamage(damage);
            Destroy(gameObject);
            //collision.gameObject.GetComponent<EnemyCombat>().TakeDamage(damage);
        }

        if (collision.transform.tag == "blockingLayer")
        {
            Destroy(gameObject);
        }

        if (collision.transform.tag == "Bullet" || collision.transform.tag == "BulletEnemy")
        {
            Destroy(gameObject);
        }


        if (collision.gameObject.tag == "Base" && gameObject.tag == "BulletEnemy")
        {
            Debug.Log("Enemy hit base!");
            GameManager.instance.currentBase.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
