using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveArrows : MonoBehaviour
{
    public float speed;

    float movementX;
    float movementY;
    float animSpeed;
    Rigidbody2D rgd;
    Animator animator;

    private void Start()
    {
        rgd = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        movementX = 0;
        movementY = 0;
        speed = 10000 / 2f;

    }

    private void Update()
    {
        rgd.velocity = new Vector2(movementX * speed * Time.deltaTime, movementY * speed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.UpArrow))
            movementY = 1;

        if (Input.GetKeyDown(KeyCode.DownArrow))
            movementY = -1;


        if (Input.GetKeyDown(KeyCode.LeftArrow))
            movementX = -1;

        if (Input.GetKeyDown(KeyCode.RightArrow))
            movementX = 1;

        if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.DownArrow))
            movementY = 0;

        if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow))
            movementX = 0;

        if (movementX != 0)
            animator.SetFloat("Horizontal", movementX);

        if (movementY != 0)
            animator.SetFloat("Vertical", movementY);

        animSpeed = new Vector2(movementX, movementY).sqrMagnitude;

        animator.SetFloat("Speed", animSpeed);

    }
}
