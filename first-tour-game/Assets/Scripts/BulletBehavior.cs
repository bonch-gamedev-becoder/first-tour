using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    [SerializeField] int damage;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collistion for bullit is " + collision.transform.name);

        if (collision.collider.tag == "Enemy" || collision.collider.tag == "ArtilleryEnemy")
        {
            collision.gameObject.GetComponent<EnemyCombat>().TakeDamage(damage);
        }

        if (collision.gameObject.tag == "Base" && tag != "Bullet")
        {
            Debug.Log("Enemy hit base!");
            GameManager.instance.currentBase.TakeDamage(damage);
        }

        if (collision.transform.tag == "blockingLayer" && gameObject.tag == "BulletArtillery")
        {
            gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
            DestroyArtilleryBullet();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    IEnumerator DestroyArtilleryBullet()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
