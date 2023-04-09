using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    [SerializeField] int damage;

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

        if (collision.gameObject.tag == "blockingLayerBreakable")
        {
            Destroy(collision.gameObject);
        }

        Destroy(gameObject);
    }
}
