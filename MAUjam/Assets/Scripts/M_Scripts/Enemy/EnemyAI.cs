using System.Collections;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float patrolSpeed = 2f;
    public float chaseRange = 5f;
    public float attackRange = 1.5f;
    public int damageAmount = 10;
    private Transform player;
    private Animator animator;
    private bool isAttacking = false;
    private Rigidbody2D enemyRb;
    private bool canMove = true;
    private EnemyHealth _enemyHealth;
    public LayerMask groundLayer;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
        enemyRb = GetComponent<Rigidbody2D>();
        _enemyHealth = GetComponent<EnemyHealth>();
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

                if (IsFacingRight())
                {
                    enemyRb.velocity = new Vector2(patrolSpeed, 0f);
                }
                else
                {
                    enemyRb.velocity = new Vector2(-patrolSpeed, 0f);
                }
            }

            if (_enemyHealth.isDead)
            {
                canMove = false;
                enemyRb.velocity = Vector2.zero;
                this.enabled = false;
            }
        }
        if(canMove)ChasePlayer(); 
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
        canMove = true;
        animator.SetBool("isRunning", canMove);
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);


        if (distanceToPlayer <= chaseRange)
        {
            Vector2 targetPos = new Vector2(player.position.x, transform.position.y);
            
            RaycastHit2D
                hit = Physics2D.Raycast(transform.position, transform.right, 2.0f,
                    groundLayer); 
            if (hit.collider == null)
            {
                
                Flip(targetPos.x - transform.position.x);
                float chase = patrolSpeed * Time.deltaTime;
                transform.position = Vector2.MoveTowards(transform.position, targetPos, chase);
            }
            else
            {
                
                canMove = false;
                animator.SetBool("isRunning", canMove);
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

            animator.SetTrigger(animationTrigger);
            canMove = false;
            animator.SetBool("isRunning", canMove);
            isAttacking = true;
            animator.SetBool("IsAttacking", isAttacking);

            yield return new WaitForSeconds(animator.GetCurrentAnimatorClipInfo(0).Length * 0.6f);

            isAttacking = false;
            animator.SetBool("IsAttacking", isAttacking);
            canMove = true;
            animator.SetBool("isRunning", canMove);
            // Deal damage to the player
            player.GetComponent<IDamageable>().TakeDamage(damageAmount);
        }







   
}


