using System.Collections;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float patrolSpeed = 2f;
    public float chaseSpeed = 5f;
    public float attackRange = 1.5f;
    public int damageAmount = 10;
    public Transform[] patrolWaypoints;

    private int currentWaypointIndex = 0;
    private Transform player;
    private Animator animator;
    private bool isAttacking = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        Patrol();
        CheckPlayerDistance();
    }

    void Patrol()
    {
        Vector2 target = patrolWaypoints[currentWaypointIndex].position;
        float step = patrolSpeed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, target, step);

        // Flip the enemy based on movement direction
        Flip(target.x - transform.position.x);

        // Set animator parameters
        animator.SetFloat("Speed", Mathf.Abs(patrolSpeed));

        // Check if the enemy reached the waypoint
        if (Vector2.Distance(transform.position, target) < 0.1f)
        {
            // Reached the waypoint, switch to the next one
            currentWaypointIndex = (currentWaypointIndex + 1) % patrolWaypoints.Length;
        }
    }

    void CheckPlayerDistance()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer < attackRange)
        {
            StartCoroutine((PerformAttack("Punch")));
        }
        else
        {
            isAttacking = false;
            animator.SetBool("IsAttacking", isAttacking);
        }
    }

    IEnumerator PerformAttack(string animationTrigger)
    {
        if (isAttacking)
        {
            yield break;
        }
        animator.SetTrigger(animationTrigger);
        isAttacking = true;
        animator.SetBool("IsAttacking", isAttacking);

        yield return new WaitForSeconds(animator.GetCurrentAnimatorClipInfo(0).Length * 0.6f);

        isAttacking = false;

        // Deal damage to the player
        player.GetComponent<IDamageable>().TakeDamage(damageAmount);
    }

    void Flip(float direction)
    {
        if (direction > 0)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f); // Face right
        }
        else if (direction < 0)
        {
            transform.localScale = new Vector3(1f, 1f, 1f); // Face left
        }
    }
    

   

   
}


