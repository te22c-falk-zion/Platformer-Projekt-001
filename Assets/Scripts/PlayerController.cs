
using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

public class PlayerController : MonoBehaviour
{   
    private float xMove;
    [SerializeField] float jumpForce = 10;
    [SerializeField] float speed = 8f; 
    bool mayJump = true;
    bool isFacingRight;

    private bool isWallSliding;
    private float wallSlidingSpeed = 2f;

    private float dashingPower = 24f;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 1f;     
    private bool canDash = true;
    private bool isDashing;    


    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundChecker;
    [SerializeField] private Transform wallChecker;
    [SerializeField] private Transform wallCheckerRight;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;

    void Update()
    {

        if (isDashing)
        {
            return;
        }

        float xMove = Input.GetAxisRaw("Horizontal");
        rb.velocity = new ( xMove * speed, rb.velocity.y ); 

        if (Input.GetButtonDown("Jump") && isGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

        WallSlide();


        if(Input.GetKeyDown(KeyCode.Q) && canDash == true)
        {
            StartCoroutine(Dash());
        }

        Flip();
        
    }

    private void FixedUpdate() 
    {
        
    }

    private bool isGrounded()
    {
        if (Physics2D.OverlapCircle(groundChecker.position, 0.2f, groundLayer))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool isWalled()
    {
        return Physics2D.OverlapCircle(wallChecker.position, 0.2f, wallLayer);
    }

    private void WallSlide()
    {
        if (isWalled() && !isGrounded() || xMove != 0f)
        {
            isWallSliding = true;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue));
        }
        else
        {
            isWallSliding = false;
        }
    }






    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalgGavity = rb.gravityScale;
        rb.gravityScale = 0;
        if (rb.velocity.x > 0)
        {
            rb.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        }
        else if (rb.velocity.x < 0)
        {
            rb.velocity = new Vector2(transform.localScale.x * -dashingPower, 0f);
        }
        yield return new WaitForSeconds(dashingTime);
        rb.gravityScale = originalgGavity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    } 

    private void Flip()
    {
            if (rb.velocity.x > 0)
            {
                GetComponent<SpriteRenderer>().flipX = true;
                Vector3 pos = wallChecker.position;
                pos.x = -pos.x;
                wallChecker.position = pos;
                isFacingRight = true; 

            }
            if (rb.velocity.x < 0)
            {
                GetComponent<SpriteRenderer>().flipX = false;
                Vector3 pos = wallChecker.position;
                pos.x = -pos.x;
                wallChecker.position = pos;
                isFacingRight = false;
            }
    }


}