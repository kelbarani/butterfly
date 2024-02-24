using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour,IDamageable
{
    [SerializeField] private float maxEnemyHealth=50f;
    private float enemyCurrentHealth;
    

    private void Awake()
    {
        enemyCurrentHealth = maxEnemyHealth;
    }

    private void Update()
    {
        if (enemyCurrentHealth <= 0)
        {
            Die();
        }
    }

    public void TakeDamage(float damageAmount)
    {
        enemyCurrentHealth -= damageAmount;
        Debug.Log("Current health is:" + enemyCurrentHealth);
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
