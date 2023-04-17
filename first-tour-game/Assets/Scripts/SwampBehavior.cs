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
        if (collision.TryGetComponent(out MoveWASD movewasd))
        {
            movewasd.speed = 2500f;
            Debug.Log("wasd dec");
        }

        if (collision.TryGetComponent(out MoveArrows movearrows))
        {
            movearrows.speed = 2500f;
            Debug.Log("arrows dec");
        }

        yield return new WaitForSeconds(3f);
        if (movewasd != null)
        movewasd.speed = 5000f;
        if (movearrows != null)
        movearrows.speed = 5000f;
    }
}
