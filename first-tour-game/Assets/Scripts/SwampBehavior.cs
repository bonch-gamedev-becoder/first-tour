using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwampBehavior : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            StartCoroutine(DecSpeed(collision));
        }
    }

    IEnumerator DecSpeed(Collider2D collision)
    {
        PlayerMovement script = collision.gameObject.GetComponent<PlayerMovement>();
        script.moveSpeed = 2.5f;
        yield return new WaitForSeconds(3f);
        script.moveSpeed = 5f;
    }
}
