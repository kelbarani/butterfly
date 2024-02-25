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
    public bool isDead = false;
    private GameObject _virtualCamera;
    private B_CamShake _camShake;

    private void Awake()
    {
        enemyCurrentHealth = maxEnemyHealth;
        _animator = GetComponent<Animator>();
        normalCollider.enabled = true;
        deathCollider.enabled = false;
        _virtualCamera = GameObject.FindWithTag("VirtualCamera");
        _camShake = _virtualCamera.GetComponent<B_CamShake>();
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
        _camShake?.CamShake();
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
        isDead = true;
        yield return new WaitForSeconds(0.25f);
        normalCollider.enabled = false;
        deathCollider.enabled = true;
        _animator.SetBool("IsDead",isDead);
        //more vfx maybe?
        yield return new WaitForSeconds(0.6f);
        Destroy(gameObject);
        
    }
    
}
