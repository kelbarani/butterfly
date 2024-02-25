using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour,IDamageable
{
    public float maxHealth = 100;
    private float currentHealth;
    [SerializeField] Transform respawnPoint;
    private PlayerController _movement;
    private Animator animator;
    public bool isDead = false;
    [SerializeField] private BoxCollider2D normalCollider;
    [SerializeField] private BoxCollider2D deathCollider;
    [SerializeField] private float damageCooldown = 1.0f;
    private bool canTakeDamage = true;
    private GameObject _virtualCamera;
    private B_CamShake _camShake;
    private Image currentHeadState;
    public Image fullHealthHead; 
    public Image halfHealthHead;
    public Image criticalHealthHead;
    public Image healthBar;
    public AudioClip appleBite;
    private AudioSource _audioSource;
    private void Awake()
    {
        currentHealth = maxHealth;
        _movement = GetComponent<PlayerController>();
        animator = GetComponent<Animator>();
        normalCollider.enabled = true;
        deathCollider.enabled = false;
        _virtualCamera = GameObject.FindWithTag("VirtualCamera");
        _camShake = _virtualCamera.GetComponent<B_CamShake>();
        halfHealthHead.gameObject.SetActive(false);
        criticalHealthHead.gameObject.SetActive(false);
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (currentHealth <= 0)
        {
            Die();
        }

        if (currentHealth > 66f)
        {
            ChangeHead(fullHealthHead);
            
        }
        else if (currentHealth>33f)
        {
            ChangeHead(halfHealthHead);
        }
        else
        {
            ChangeHead(criticalHealthHead);
        }
        
        
        if (Input.GetKeyDown(KeyCode.E))
        {
            TakeDamage(20);
        }
        //
        // if (Input.GetKeyDown(KeyCode.R))
        // {
        //     Heal(10);
        // }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Apple"))
        {
            Destroy(collision.gameObject);
            //apple collect sound.
            _audioSource.PlayOneShot(appleBite);
            Heal(40);
        }
    }
    

    public void ChangeHead(Image newHeadState)
    {
        if(currentHeadState!=newHeadState)
        {
            if(currentHeadState!=null)
            {
                currentHeadState.gameObject.SetActive(false);
            }
            newHeadState.gameObject.SetActive(true);
            currentHeadState = newHeadState;

        }
            
    }


    public void TakeDamage(float damageAmount)
    {
        if (!canTakeDamage)
        {
            return;
        }
        
        _movement.enabled = false;
        animator.SetBool("takeDamage",true);
        _camShake?.CamShake();
        currentHealth -= damageAmount;
        healthBar.fillAmount = currentHealth / maxHealth;
        Invoke(nameof(ResetDamage),0.1f);
        Debug.Log("Current health is:" + currentHealth);
        canTakeDamage = false;
        Invoke(nameof(ResetDamageCooldown), damageCooldown);
    }

    private void ResetDamage()
    {
        animator.SetBool("takeDamage",false);
    }
    private void ResetDamageCooldown()
    {
        canTakeDamage = true;
        _movement.enabled = true;
    }
    void Die()
    {
        isDead = true;
        normalCollider.enabled = false;
        deathCollider.enabled = true;
        animator.SetBool("Death",true);
        //play death sfx
        _movement.enabled = false;
        Invoke(nameof(Respawn),1f);
        Debug.Log("Dead");
    }

    void Respawn()
    {
        isDead = false;
        //this.gameObject.SetActive(true);
        normalCollider.enabled = true;
        deathCollider.enabled = false;
        animator.SetBool("Death",false);
        _movement.enabled = true;
        transform.position = respawnPoint.position;
        currentHealth = maxHealth;
        healthBar.fillAmount = 1.0f;
        Debug.Log("Respawned");
        
    }

    public void Heal(float HealAmount)
    {
        currentHealth += HealAmount;
        Debug.Log(HealAmount+"kadar iyileşildi.");
        currentHealth = Mathf.Clamp(currentHealth, 0, 100);
        healthBar.fillAmount = currentHealth / maxHealth;
        
    }
}
