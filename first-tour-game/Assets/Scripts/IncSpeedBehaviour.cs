using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncSpeedBehaviour : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            StartCoroutine(IncSpeed(collision));
        }
    }

    IEnumerator IncSpeed(Collider2D collision)
    {
        collision.gameObject.GetComponent<PlayerMovement>().moveSpeed = 7.5f;
        yield return new WaitForSeconds(3f);
        collision.gameObject.GetComponent<PlayerMovement>().moveSpeed = 5f;
    }
}
