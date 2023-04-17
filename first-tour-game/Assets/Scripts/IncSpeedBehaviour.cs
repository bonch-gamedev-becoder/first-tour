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

        if (TryGetComponent<MoveWASD>(out MoveWASD movewasd))
        {
            movewasd.speed = 10000;
            Debug.Log("SPEED UP wasd!");
        }

        if (TryGetComponent<MoveArrows>(out MoveArrows movearrows))
        {
            movearrows.speed = 10000;
            Debug.Log("SPEED UP arrows!");
        }

        yield return new WaitForSeconds(3f);

        if (movewasd != null)
            movewasd.speed = 5000;
        if (movearrows != null)
            movearrows.speed = 5000;
    }
}
