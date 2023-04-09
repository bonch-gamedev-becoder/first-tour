using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportBehavior : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.transform.position = new Vector3(GameManager.instance.currentBase.transform.position.x, GameManager.instance.currentBase.transform.position.y, 0);
            Destroy(gameObject);
        }
    }
}
