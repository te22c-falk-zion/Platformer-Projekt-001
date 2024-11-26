
using System.Collections;
using System.Collections.Generic;
// using System.Numerics;
using System.Reflection.Emit;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{   
    private int health = 100;
    [SerializeField] Slider hpBar;
    private float xMove;
    [SerializeField] float jumpForce = 10;
    [SerializeField] float speed = 8f; 
    public bool directionalKey;

    bool isFacingRight = false;
    private float wallCheckerPosition;
    Vector3 pos;

    private bool isWallSliding;
    private float wallSlidingSpeed = 2f;
    private int wallJumpCounter;
    private Vector2 wallJumpingPower = new Vector2(12f, 24f);
    bool mayDJump = true;

    private float dashingPower = 24f;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 1f;     
    private bool canDash = true;
    private bool isDashing;    


    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundChecker;
    [SerializeField] private Transform wallChecker;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;


    void Start() 
    {
        health = 100;
        hpBar.maxValue = health;
    }
    void Update()
    {
        hpBar.value = health;

        if (isDashing)
        {
            return;
        }

        float xMove = Input.GetAxisRaw("Horizontal");

        rb.velocity = new Vector2(xMove * speed, rb.velocity.y); 

        if (Input.GetButtonDown("Jump") && isGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

        if(xMove != 0f)
        {
            directionalKey = true;
        }

        WallSlide();
        DoubleJump();
        WallJump();


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
            mayDJump = true;
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool isWalled()
    {

        if (Physics2D.OverlapCircle(wallChecker.position, 0.2f, wallLayer))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void WallSlide()
    {
        if (isWalled() && !isGrounded() && directionalKey == true)
        {
            isWallSliding = true;
            mayDJump = true;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue));
        }
        else
        {
            isWallSliding = false;
        }
    }

    private void WallJump()
    {
        if(Input.GetButtonDown("Jump") && wallJumpCounter <= 5 && !isGrounded() && isWalled())
        {
            rb.velocity = new Vector2(-wallJumpingPower.x, wallJumpingPower.y);
            wallJumpCounter ++;
        }
        if(isGrounded())
        {
            wallJumpCounter = 0;
        }
    }



    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalgGavity = rb.gravityScale;
        rb.gravityScale = 0;

        if (rb.velocity.x < 0)
        {
            rb.velocity = new Vector2(transform.localScale.x * -dashingPower, 0f);
        }
        else if (rb.velocity.x > 0)
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
        if (isFacingRight && rb.velocity.x < 0f || !isFacingRight && rb.velocity.x > 0f)
        {
            Vector3 localScale = transform.localScale;
            isFacingRight = !isFacingRight;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    private void DoubleJump()
    {
        if (mayDJump == true && Input.GetButtonDown("Jump") && !isGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            mayDJump = false;
        }
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        Debug.Log(health);
        if (health <= 0)
        {
            SceneManager.LoadScene(1);
        }
    }


}