using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour
{
    public GameObject crash;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Bullet")
        {
            Instantiate(crash, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
  
}
