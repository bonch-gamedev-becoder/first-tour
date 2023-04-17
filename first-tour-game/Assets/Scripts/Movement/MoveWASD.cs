using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWASD : MonoBehaviour
{
    public float speed = 5;

    Vector2 movement;
    float animSpeed;
    Rigidbody2D rgd;
    Animator animator;

    private void Start()
    {
        rgd = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        movement.x = 0;
        movement.y = 0;

    }

    private void FixedUpdate()
    {
        movement = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
            movement.y = 1;

        if (Input.GetKey(KeyCode.S))
            movement.y = -1;

        if (Input.GetKey(KeyCode.A))
            movement.x = -1;

        if (Input.GetKey(KeyCode.D))
            movement.x = 1;

        if (movement.x != 0)
            animator.SetFloat("Horizontal", movement.x);

        if (movement.y != 0)
            animator.SetFloat("Vertical", movement.y);

        rgd.MovePosition(rgd.position + movement.normalized * speed * Time.fixedDeltaTime);

        animSpeed = new Vector2(movement.x, movement.y).sqrMagnitude;

        animator.SetFloat("Speed", animSpeed);

    }
}
