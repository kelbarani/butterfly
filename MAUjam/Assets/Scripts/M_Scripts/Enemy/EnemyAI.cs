using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private float patrolSpeed=2.5f;
    [SerializeField] private float chaseSpeed=5f;
    [SerializeField] private float attackRange=1.5f; 
    [SerializeField] private int enemyDamage=10;
    public Transform[] patrolWaypoints;
    
    private int currentWaypointIndex = 0;
    private Transform player;
    private Animator animator;
    
    
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
        Flip(target.x - transform.position.x);
        if (Vector2.Distance(transform.position, target) < 0.1f)
        {
            
            currentWaypointIndex = (currentWaypointIndex + 1) % patrolWaypoints.Length;
        }
       
    }

    void CheckPlayerDistance()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        if (distanceToPlayer < attackRange)
        {
            
            Attack();
        }
        else
        {
            
            animator.SetBool("IsAttacking", false);
        }
    }
    void Attack()
    {
        animator.SetBool("IsAttacking", true);
        
        player.GetComponent<IDamageable>().TakeDamage(enemyDamage);
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

