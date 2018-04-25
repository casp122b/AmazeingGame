using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour {

    public float MovementSpeed = 1f;

    private float speed = 0f;
    private Vector2 movementDirection;
    private Rigidbody2D rbody;
    private Animator animator;
    private SpriteRenderer _renderer;

    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        _renderer = GetComponent<SpriteRenderer>();
        speed = MovementSpeed;
    }

    private void FixedUpdate()
    {
        rbody.velocity = movementDirection;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            animator.SetTrigger("Die");
        }
        else if (Input.GetKeyDown(KeyCode.Z))
        {
            animator.SetTrigger("Attack");
            movementDirection = Vector2.zero;
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetTrigger("Jump");
        }
        else
        {
            Move();
        }
    }

    private void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        if (x < 0f)
            _renderer.flipX = true;
        else if (x > 0f)
            _renderer.flipX = false;

        Vector2 dir = new Vector2(x, y);
        speed = 0f;
        if (dir.magnitude > 0f)
        {
            speed = MovementSpeed;
        }
        movementDirection = dir.normalized * speed;
        animator.SetFloat("Speed", speed);
    }
}
