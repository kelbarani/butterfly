using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPunch : MonoBehaviour
{
    private CircleCollider2D enemyPunchCollider;
    [SerializeField] private float enemyPunchDamage;
    
    private void Awake()
    {
        enemyPunchCollider = GetComponent<CircleCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.TakeDamage(enemyPunchDamage);
        }
    }
}
