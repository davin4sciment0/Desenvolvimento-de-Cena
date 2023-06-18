using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed, drag, jumpForce;
    public Rigidbody rb;
    bool left, right, forward, backward;
    bool grounded, jump;
    public LayerMask ground;

    void Update()
    {
        HandleInput();
        HandleDrag();
        CheckGrounded();
    }

    void FixedUpdate()
    {
        if (left)
        {
            rb.AddForce(Vector3.left * speed);
            left = false;
        }
        if (right)
        {
            rb.AddForce(Vector3.right * speed);
            right = false;
        }
        if (forward)
        {
            rb.AddForce(Vector3.forward * speed);
            forward = false;
        }
        if (backward)
        {
            rb.AddForce(Vector3.back * speed);
            backward = false;
        }
        if (jump && grounded)
        {
            transform.position += Vector3.up * .1f;
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.y);
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            jump = false;
        }

    }

    void HandleInput()
    {
        if (Input.GetKey(KeyCode.A))
        {
            right = true;
        }
        if (Input.GetKey(KeyCode.W))
        {
            backward = true;
        }
        if (Input.GetKey(KeyCode.S))
        {
            forward = true;
        }
        if (Input.GetKey(KeyCode.D))
        {
            left = true;
        }
        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            jump = true;
        }
    }

    void HandleDrag()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z) / (1 + drag / 100) + new Vector3(0, rb.velocity.y, 0);
    }

    void CheckGrounded()
    {
        grounded = Physics.Raycast(transform.position + Vector3.up * .1f, Vector3.down, .2f, ground);
    }
}
