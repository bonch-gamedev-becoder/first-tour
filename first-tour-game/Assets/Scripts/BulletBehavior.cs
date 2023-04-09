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

        //bullet of artillery hit blockingLayer
        if (collision.transform.tag == "blockingLayer" && gameObject.tag == "BulletArtillery")
        {
            StartCoroutine(TurnOffCollider(collision));
            StartCoroutine(DestroyBulletAfterTime());
        }
        else
        {
            Destroy(gameObject);
        }
    }

    IEnumerator TurnOffCollider(Collision2D collision)
    {
        Collider2D test = GetComponent<BoxCollider2D>();

        test.isTrigger = true;
        yield return new WaitForSeconds(0.01f);
        test.isTrigger = false;
    }

    IEnumerator DestroyBulletAfterTime()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
