using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    private Rigidbody2D rb;
    private Animator animator;
    public bool isGrounded;
    private bool isFacingRight = false;
    public LayerMask groundLayer;
    private float groundCheckDistance = 0.2f;
    public Transform groundCheck;
    private float horizontal;
    private bool isJumping;
    private float coyoteTime = 0.2f;
    private float coyoteTimeCounter;             
    private float jumpBufferTime = 0.2f;
    private float jumpBufferCounter;
    private bool isAttacking = false;
    private float animationAccelerator;
    private AudioSource audioSource;
    public AudioClip punchAirSound;
    
    private enum MovementState { idle, jumping,running,falling,airKicking};
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        HandleInput();
        Flip();
        UpdateAnimations();
        CoyoteTime();
        JumpBuffer();
        
    }

    private void JumpBuffer()
    {
        if (Input.GetButtonDown("Jump"))
        {
            jumpBufferCounter = jumpBufferTime;
        }
        else
        {
            jumpBufferCounter -= Time.deltaTime;
        }
    }

    void CoyoteTime()
    {
        if (IsGrounded())
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }
    }

    void FixedUpdate()
    {
        isGrounded=IsGrounded();
    }

    void HandleInput()
    {
        if (isAttacking)
        {
            rb.velocity = new Vector2(0f, rb.velocity.y);
            return;  
        }
        horizontal = Input.GetAxis("Horizontal");
        Vector2 movement = new Vector2(horizontal, 0f);
        rb.velocity = new Vector2(movement.x * moveSpeed, rb.velocity.y);
        
        #region Jumping
        if (coyoteTimeCounter > 0f && jumpBufferCounter > 0f && !isJumping)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            jumpBufferCounter = 0f;
            StartCoroutine(JumpCooldown());
        }
        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
            coyoteTimeCounter = 0f;
        }
        #endregion
        #region Combat
        
        
        if (Input.GetButtonDown("Fire1")&& IsGrounded())
        {
            // First Punch
            animationAccelerator = 0.4f;
            StartCoroutine(PerformAttack("Punch"));
                
        }
      
        
        else if (Input.GetButtonDown("Fire2")&& IsGrounded())
        {
            // Kick
            animationAccelerator = 0.5f;
            StartCoroutine(PerformAttack("Kick"));
        }
        else if (Input.GetButtonDown("Fire2") && isJumping)
        {
            // Air Kick
            animationAccelerator = 0.6f;
            StartCoroutine(PerformAttack("AirKick"));
        }
        #endregion
       
    }

   

    IEnumerator PerformAttack(string animationTrigger)
    {
        if (isAttacking)
        {
            yield break;
        }
        
        animator.SetTrigger(animationTrigger);
        audioSource.PlayOneShot(punchAirSound);
        isAttacking = true;
        animator.SetBool("IsAttacking",isAttacking);
        
        yield return new WaitForSeconds(animator.GetCurrentAnimatorClipInfo(0).Length*animationAccelerator);
        //reset state
        
        isAttacking = false;
        animator.SetBool("IsAttacking",isAttacking);
        // if (animationTrigger == "SecondPunch")
        // {
        //     punchCount = 0;
        // }
    }
    
    

    void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;


        }
    }
    

    bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, groundCheckDistance, groundLayer);
    }

    private IEnumerator JumpCooldown()
    {
        isJumping = true;
        yield return new WaitForSeconds(0.4f);
        isJumping = false;
    }
    void UpdateAnimations()
    {
        MovementState state;

        if (horizontal != 0)
        {
            state = MovementState.running;
        }
        else
        {
            state = MovementState.idle;
        }

        if (rb.velocity.y > .1f)
        {
            state = MovementState.jumping;
            if (Input.GetKeyDown(KeyCode.Mouse2))
            {
                state = MovementState.airKicking;
            }
        }

        if (rb.velocity.y < -.1f)
        {
            state = MovementState.falling;
        }
        
        animator.SetInteger("state", (int) state);
    }
  
}
