using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour,IDamageable
{
    [SerializeField] private float maxHealth = 100;
    private float currentHealth;
    private Transform respawnPoint;
    
    
    private void Awake()
    {
        currentHealth = maxHealth;
    }

    private void Update()
    {
        if (currentHealth <= 0)
        {
            Die();
        }
    }


    public void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;
    }

    void Die()
    {
        //play death animation
        //play death sfx
        
    }
}
