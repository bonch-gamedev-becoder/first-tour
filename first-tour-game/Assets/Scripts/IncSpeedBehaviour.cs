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

        if (collision.TryGetComponent(out MoveWASD movewasd))
            movewasd.speed = 7.5f;

        if (collision.TryGetComponent(out MoveArrows movearrows))
            movearrows.speed = 7.5f;

        yield return new WaitForSeconds(3f);

        if (movewasd != null)
            movewasd.speed = 5f;
        if (movearrows != null)
            movearrows.speed = 5f;
    }
}
