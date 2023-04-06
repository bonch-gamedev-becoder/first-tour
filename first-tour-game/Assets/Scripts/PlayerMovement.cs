using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement instance = null;
    [SerializeField] float moveSpeed = 5f;

    private Rigidbody2D rb;
    private Animator animator;
    [SerializeField] Text resourcesCounterText;

    Vector2 movement;

    int totalResources = 0;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    //????????? ?????
    void Update()
    {
        ProcessInputForMovement();
    }

    //????????? ????????
    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement.normalized * moveSpeed * Time.fixedDeltaTime);
    }

    private void ProcessInputForMovement()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Resource")
        {
            Destroy(collision.gameObject);
            totalResources++;
            resourcesCounterText.text = "Resources: " + totalResources;
        }
    }
}
