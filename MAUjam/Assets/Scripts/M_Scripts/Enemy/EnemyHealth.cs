using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour,IDamageable
{
    [SerializeField] private float maxEnemyHealth=50f;
    private float enemyCurrentHealth;
    private Animator _animator;
    [SerializeField] private BoxCollider2D normalCollider;
    [SerializeField] private BoxCollider2D deathCollider;
    public bool damageTaken = false;

    private void Awake()
    {
        enemyCurrentHealth = maxEnemyHealth;
        _animator = GetComponent<Animator>();
        normalCollider.enabled = true;
        deathCollider.enabled = false;
    }

    private void Update()
    {
        if (enemyCurrentHealth <= 0)
        {
            StartCoroutine(Die());
        }
    }

    public void TakeDamage(float damageAmount)
    {
        damageTaken = true;
        _animator.SetBool("takeDamage",true);
        enemyCurrentHealth -= damageAmount;
        Invoke(nameof(ResetDamageAnim),0.3f);
        Debug.Log("Current health is:" + enemyCurrentHealth);
    }

    void ResetDamageAnim()
    {
        _animator.SetBool("takeDamage",false);
        damageTaken = false;
    }

   
    IEnumerator Die()
    {
        yield return new WaitForSeconds(0.25f);
        normalCollider.enabled = false;
        deathCollider.enabled = true;
        _animator.SetBool("IsDead",true);
        //more vfx maybe?
        yield return new WaitForSeconds(0.6f);
        Destroy(gameObject);
        
    }
    
}
