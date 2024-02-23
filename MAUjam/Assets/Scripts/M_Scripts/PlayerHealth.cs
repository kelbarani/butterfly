using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour,IDamageable
{
    [SerializeField] private float maxHealth = 100;
    private float currentHealth;
    [SerializeField] Transform respawnPoint;
    private PlayerMovement _movement;
    private void Awake()
    {
        currentHealth = maxHealth;
        _movement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (currentHealth <= 0)
        {
            Die();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            TakeDamage(20);
        }
    }


    public void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;
        Debug.Log("Current health is:" + currentHealth);
    }

    void Die()
    {
        //play death animation
        //play death sfx
        _movement.enabled = false;
        Invoke(nameof(Respawn),2f);
        Debug.Log("Dead");
        
        
    }

    void Respawn()
    {
        _movement.enabled = true;
        transform.position = respawnPoint.position;
        currentHealth = maxHealth;
        Debug.Log("Respawned");
        
    }
}
