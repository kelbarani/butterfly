using System.Collections;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float patrolSpeed = 2f;
    public float chaseRange = 5f;
    public float attackRange = 1.5f;
    private Transform player;
    private Animator animator;
    private bool isAttacking = false;
    public float attackCooldown = 0.2f;
    private Rigidbody2D enemyRb;
    private bool canMove = true;
    private EnemyHealth _enemyHealth;
    public LayerMask groundLayer;
    private PlayerController playerController;
    public float verticalMovementThreshold=1f;
    private PlayerHealth _playerHealth;

    public bool canPatrol;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        _playerHealth = player.GetComponent<PlayerHealth>();
        animator = GetComponent<Animator>();
        enemyRb = GetComponent<Rigidbody2D>();
        _enemyHealth = GetComponent<EnemyHealth>();
        playerController = player.gameObject.GetComponent<PlayerController>();
        
    }
    void Update()
    {
        if (canMove)
        {
            if (_enemyHealth.damageTaken)
            {
                canMove = false;
                animator.SetBool("isRunning", canMove);
                StartCoroutine(ResetMovement());
                return;
            }
            if (!_enemyHealth.damageTaken)
            {
                canMove = true;
                animator.SetBool("isRunning", canMove);
                Patrol();
            }
            if (_enemyHealth.isDead)
            {
                canMove = false;
                enemyRb.velocity = Vector2.zero;
                this.enabled = false;
            }
        }
        if(canMove && playerController.isGrounded) ChasePlayer(); 
    }
    
    private void Patrol()
    {
        if (canPatrol)
        {
            if (IsFacingRight())
            {
                enemyRb.velocity = new Vector2(patrolSpeed, 0f);
            }
            else
            {
                enemyRb.velocity = new Vector2(-patrolSpeed, 0f);
            }
        }
     
        else
        {
            animator.SetBool("isRunning", canPatrol);
        }
    }
    bool IsFacingRight()
    {
        return transform.localScale.x > Mathf.Epsilon;
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        transform.localScale = new Vector2(-Mathf.Sign(enemyRb.velocity.x), transform.localScale.y);
    }
    IEnumerator ResetMovement()
    {
        yield return new WaitForSeconds(0.3f);
        canMove = true;
        animator.SetBool("isRunning", canMove);
    }
    
    void ChasePlayer()
    {
        // canMove = true;
        // animator.SetBool("isRunning", canMove);
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        if (distanceToPlayer <= attackRange)
        {
            if (!_playerHealth.isDead)
            {
                StartCoroutine(PerformAttack("Punch"));
            }
            
        }
        else if (distanceToPlayer <= chaseRange) 
        {
            canMove = true;
            animator.SetBool("isRunning", canMove);
             Vector2 targetPos = new Vector2(player.position.x,transform.position.y);
             RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, 3.0f, groundLayer); 
             bool playerMovedVertically = Mathf.Abs(player.position.y - transform.position.y) > verticalMovementThreshold;
             if (hit.collider == null && !playerMovedVertically)
             {
                 Flip(targetPos.x - transform.position.x);
                 float chase = patrolSpeed * Time.deltaTime;
                 transform.position = Vector2.MoveTowards(transform.position, targetPos, chase);
             }
             else
             {
                 Patrol();
             }
        }
    }
    void Flip(float direction)
    {
        if (direction > 0)
        {
            transform.localScale = new Vector2(1f, 1f); 
        }
        else if (direction < 0)
        {
            transform.localScale = new Vector2(-1f, 1f); 
        }
    }
        IEnumerator PerformAttack(string animationTrigger)
        {
            if (isAttacking)
            {
                yield break;
            }
            
            canMove = false;
            animator.SetBool("isRunning", canMove);
            Flip(player.position.x - transform.position.x);
            yield return new WaitForSeconds(0.1f);
            animator.SetTrigger(animationTrigger);
            isAttacking = true;
            animator.SetBool("IsAttacking", isAttacking);
            yield return new WaitForSeconds(animator.GetCurrentAnimatorClipInfo(0).Length*0.5f);
            isAttacking = false;
            animator.SetBool("IsAttacking", isAttacking);
            //yield return new WaitForSeconds(0.1f);
            canMove = true;
            animator.SetBool("isRunning", canMove);
            
        }
}


