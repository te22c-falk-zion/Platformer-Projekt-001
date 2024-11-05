using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float jumpForce = 10;
    [SerializeField]
    float speed = 10;
    [SerializeField]
    Transform groundCheccker;
    [SerializeField]
    LayerMask groundLayer;

    bool mayJump = true;

    void Update()
    {
        float xMove = Input.GetAxisRaw("Horizontal");
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = new(
            xMove * speed,
            rb.velocity.y
        );


        if (Input.GetAxisRaw("Jump") > 0 && mayJump == true)
        {
            rb.AddForce(Vector2.up * jumpForce);
            
            mayJump = false;
        }
        if (rb.velocity.x > 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        if (rb.velocity.x < 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }

        if (Input.GetAxisRaw("Jump") == 0 && mayJump == false)
        {
            mayJump = true;
        }

        if(isGrounded() == false)
        {
            mayJump = false;
        }
    }

    private bool isGrounded()
    {
        if (Physics2D.OverlapCircle(groundCheccker.position, .2f, groundLayer))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
