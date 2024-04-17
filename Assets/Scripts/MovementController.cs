using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    Rigidbody2D rb;
    public Animator animator;
    public float runSpeed = 2f;
    float horizontalMove = 0f;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        animator.SetFloat("Walking", Mathf.Abs(horizontalMove));

        flip();
    }
    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontalMove * runSpeed, rb.velocity.y);
    }


    void flip()
    {
        if (horizontalMove < -0.1f) transform.localScale = new Vector3(-1, 1, 1);
        if (horizontalMove> 0.1f) transform.localScale = new Vector3(1, 1, 1);
    }
}
