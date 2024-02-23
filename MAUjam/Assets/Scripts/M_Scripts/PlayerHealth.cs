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
    private Animator animator;
    private bool isDead = false;
    [SerializeField] private BoxCollider2D normalCollider;
    [SerializeField] private BoxCollider2D deathCollider;
    private void Awake()
    {
        currentHealth = maxHealth;
        _movement = GetComponent<PlayerMovement>();
        animator = GetComponent<Animator>();
        normalCollider.enabled = true;
        deathCollider.enabled = false;
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
        normalCollider.enabled = false;
        deathCollider.enabled = true;
        animator.SetBool("Death",true);
        //play death sfx
        _movement.enabled = false;
        Invoke(nameof(Respawn),2f);
        Debug.Log("Dead");
        
        
    }

    void Respawn()
    {
        normalCollider.enabled = true;
        deathCollider.enabled = false;
        animator.SetBool("Death",false);
        _movement.enabled = true;
        transform.position = respawnPoint.position;
        currentHealth = maxHealth;
        Debug.Log("Respawned");
        
    }
}
