using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPunch : MonoBehaviour
{
    private CircleCollider2D enemyPunchCollider;
    [SerializeField] private float enemyPunchDamage;
    private AudioSource _audioSource;
    public AudioClip punchBodySound;
    
    private void Awake()
    {
        enemyPunchCollider = GetComponent<CircleCollider2D>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();
        if (damageable != null)
        {
            _audioSource.PlayOneShot(punchBodySound);
            damageable.TakeDamage(enemyPunchDamage);
        }
    }
}
