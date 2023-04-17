using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWASD : MonoBehaviour
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

        if (Input.GetKeyDown(KeyCode.W))
            movementY = 1;

        if (Input.GetKeyDown(KeyCode.S))
            movementY = -1;


        if (Input.GetKeyDown(KeyCode.A))
            movementX = -1;

        if (Input.GetKeyDown(KeyCode.D))
            movementX = 1;

        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S))
            movementY = 0;

        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
            movementX = 0;

        if (movementX != 0)
            animator.SetFloat("Horizontal", movementX);

        if (movementY != 0)
            animator.SetFloat("Vertical", movementY);

        animSpeed = new Vector2(movementX, movementY).sqrMagnitude;

        animator.SetFloat("Speed", animSpeed);

    }
}
