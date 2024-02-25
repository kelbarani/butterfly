using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kick : MonoBehaviour
{
    [SerializeField] private float kickDamage=20f;
    private CircleCollider2D kickCollider;
    private AudioSource audioSource;
    public AudioClip punchBodySound;
    void Start()
    {
        kickCollider = GetComponent<CircleCollider2D>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        IDamageable damageable = other.gameObject.GetComponent<IDamageable>();
        if (damageable != null)
        {
            audioSource.PlayOneShot(punchBodySound);
            damageable.TakeDamage(kickDamage);
            
        }
    }
}
