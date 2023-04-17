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

    #region KEYCODES

    public KeyCode left;
    public KeyCode right;
    public KeyCode up;
    public KeyCode down;
    
    #endregion

    private float x, y;
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

        if (Input.GetKey(up))
            movement.y = 1;

        if (Input.GetKey(down))
            movement.y = -1;

        if (Input.GetKey(left))
            movement.x = -1;

        if (Input.GetKey(right))
            movement.x = 1;

        if (movement.x != 0 || movement.y != 0)
        {
            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
        }

        rgd.MovePosition(rgd.position + movement.normalized * speed * Time.fixedDeltaTime);

        animSpeed = new Vector2(movement.x, movement.y).sqrMagnitude;

        animator.SetFloat("Speed", animSpeed);
    }
}
