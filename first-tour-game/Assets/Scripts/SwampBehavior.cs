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
            movewasd.speed = 2.5f;

        if (collision.TryGetComponent(out MoveArrows movearrows))
            movearrows.speed = 2.5f;

        yield return new WaitForSeconds(3f);
        if (movewasd != null)
        movewasd.speed = 5f;
        if (movearrows != null)
        movearrows.speed = 5f;
    }
}
